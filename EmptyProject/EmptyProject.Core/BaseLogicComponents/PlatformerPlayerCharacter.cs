using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D;
using MonoGame.Kernel2D.Animation;
using Debugger = MonoGame.Kernel2D.Helpers.DebugHelpers;
using XVector = Microsoft.Xna.Framework.Vector2;

#pragma warning disable
namespace EmptyProject.Core
{
    /// <summary>
    /// Part of the entity FSM to determine what it's currently "doing".
    /// </summary>
    [Flags]
    public enum PlayerState
    {
        None            = 0,
        Idle            = 1 << 0,  // 1
        Running         = 1 << 1,  // 2
        Dashing         = 1 << 2,  // 4
        Jumping         = 1 << 3,  // 8
        Falling         = 1 << 4,  // 16
        Landing         = 1 << 5,  // 32
        Shooting        = 1 << 6,  // 64
        Slashing        = 1 << 7,  // 128
        TakingDamage    = 1 << 8,  // 256
        Climbing        = 1 << 9,  // 512
        WallSliding     = 1 << 10, // 1024
        EnteringDoor    = 1 << 11, // 2048
    }

    /// <summary>
    /// A sidescrolling platformer player entity.
    /// </summary>
    public class PlatformerPlayerCharacter
    {
        #region afterimage stuff
        /// <summary>
        /// nifty stuff to make dashing look cooler
        /// </summary>
        private struct AfterImage
        {
            public Vector2 Position;
            public SpriteEffects Flip;
            public SpriteAnimation Animation;
            public float Timer;
        }

        private readonly List<AfterImage> AfterImages = [];
        private const float AfterImageInterval = 0.08f;
        private const float AfterImageLifetime = 0.2f;
        private float AfterImageTimer = 0f;
        private int AfterImageIndex = 0;
        private float AfterImageSeparation = 0f;
        #endregion


        #region internal resources
        private AnimationPlayer Animator = null;
        private Spritesheet SpriteSet = null;
        private Texture2D PlayerSpriteTexture = null;
        private readonly PhysicsValues _physics = PhysicsValues.Default();
        private SpriteBatch Batch = null;
        private SpriteFont _font;
        #endregion

        #region physics values
        private bool FacingRight = true;
        private XVector CurrentPosition;
        private float DashElapsedTime = 0f;
        private readonly float DashDuration;
        private float JumpElapsedTime = 0f;
        private readonly float JumpAscentDuration;
        private float VerticalVelocity = 0f;
        private const float MaxFallSpeed = 7f;
        private readonly float GroundLevel;
        private float deltaTime = 0f;
        private float AirborneSpeed =>
            HasState(PlayerState.Dashing) ? _physics.DashSpeed : _physics.RunSpeed;
        #endregion

        #region physics states
        private bool IsAirborne =>
            HasState(PlayerState.Jumping) || HasState(PlayerState.Falling);
        private bool IsGrounded = false;
        private bool JumpCut = false;
        #endregion

        #region state helpers
        /// <summary>
        /// Current state of the entity. Provides maneuverability for
        /// the entity FSM.
        /// </summary>
        private PlayerState CurrentState;
        /// <summary>
        /// Gets the current state of the entity.
        /// </summary>
        /// <returns>The current state of the entity.</returns>
        public PlayerState GetState() => this.CurrentState;
        /// <summary>
        /// Checks if a state is currently active in the FSM.
        /// </summary>
        /// <param name="state">The state to check for.</param>
        /// <returns>True if the state is active, False otherwise.</returns>
        private bool HasState(PlayerState state) => (CurrentState & state) != 0;
        /// <summary>
        /// Adds a state to the current state in the FSM.
        /// </summary>
        /// <param name="state">The state to add.</param>
        private void AddState(PlayerState state) => CurrentState |= state;
        /// <summary>
        /// Removes a state from the current state collection.
        /// </summary>
        /// <param name="state">The state to remove.</param>
        private void RemoveState(PlayerState state) => CurrentState &= ~state;
        /// <summary>
        /// Resets all states and sets a specific state.
        /// </summary>
        /// <param name="state">The state to set.</param>
        private void SetState(PlayerState state) => CurrentState = state;
        #endregion

        #region init
        /// <summary>
        /// Creates a new instance of the player character entity.
        /// </summary>
        /// <param name="position">The position to spawn on.</param>
        /// <param name="batch">The <see cref="SpriteBatch"/> to draw
        /// the entity with.</param>
        /// <param name="sprites">The <see cref="Spritesheet"/> to use
        /// to draw sprites onscreen.</param>
        /// <param name="texture">The image resource to draw the entity
        /// with.</param>
        /// <param name="font">A font to use to draw text onscreen.</param>
        public PlatformerPlayerCharacter(XVector position, SpriteBatch batch,
            Spritesheet sprites, Texture2D texture, SpriteFont font)
        {
            SetState(PlayerState.Idle);
            _font = font;
            CurrentPosition = position;
            GroundLevel = position.Y;
            Animator = new();
            Batch = batch;
            SpriteSet = sprites;
            PlayerSpriteTexture = texture;
            DashDuration =
                SpriteSet.Animations["dash"].Frames.Sum(f => f.Duration) / 1000f;
            JumpAscentDuration =
                SpriteSet.Animations["jumpascend"].Frames.Sum(f => f.Duration) / 1000f;
            Play(SpriteSet.Animations["idle"]);
        }
        #endregion

        #region framework + helper functions
        /// <summary>
        /// Uses the built-in <see cref="AnimationPlayer"/> to draw the entity
        /// onscreen.
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
        public void Draw(GameTime gameTime)
        {
            foreach (var img in AfterImages)
            {
                float alpha = MathHelper.Clamp(img.Timer / AfterImageLifetime, 0f, 1f);
                Color tint = Color.DarkRed * alpha;
                Animator.Draw(Batch, PlayerSpriteTexture, img.Position, img.Flip, tint);
            }
            Animator.Draw(Batch, PlayerSpriteTexture, CurrentPosition,
                FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }

        /// <summary>
        /// Plays a given <see cref="SpriteAnimation"/> using the
        /// built-in <see cref="AnimationPlayer"/>.
        /// </summary>
        /// <param name="anim"></param>
        public void Play(SpriteAnimation anim) => Animator.Play(anim);

        /// <summary>
        /// Removes all grounded states from the FSM.
        /// </summary>
        private void ClearGroundedStates()
        {
            RemoveState(PlayerState.Idle);
            RemoveState(PlayerState.Running);
        }
        #endregion

        /// <summary>
        /// Processes the user commands sent to the entity object.
        /// </summary>
        /// <param name="input">The <see cref="PlatformerInputBridge"/>
        /// to process inputs from.</param>
        public void ProcessPlayerActions(PlatformerInputBridge input)
        {
            // if landing, ignore any other inputs
            if (HasState(PlayerState.Landing)) { return; }

            // if grounded and idle, forcefully clear all other states and play idle,
            // and don't process anything else
            if (input.IsIdle() && IsGrounded)
            {
                SetState(PlayerState.Idle);
                Play(SpriteSet.Animations["idle"]);
                return;
            }

            // set facing direction
            if (input.MoveLeft()) { FacingRight = false; }
            else if (input.MoveRight()) { FacingRight = true; }

            // run
            bool moving = input.MoveRight() || input.MoveLeft();
            if (moving && IsGrounded && !IsAirborne)
            {
                if (!HasState(PlayerState.Running))
                {
                    SetState(PlayerState.Running);
                    Play(SpriteSet.Animations["run"]);
                }
            }

            // dash (only if grounded and not already dashing)
            bool canDash = IsGrounded && !HasState(PlayerState.Dashing)
                && !HasState(PlayerState.Jumping) &&
                !HasState(PlayerState.Falling);
            if (input.GetInputState("dash") == InputState.Pressed && canDash)
            {
                AddState(PlayerState.Dashing);
                DashElapsedTime = 0f;
                Play(SpriteSet.Animations["dash"]);
            }

            // jump (even during dash)
            if (input.GetInputState("jump") == InputState.Pressed && IsGrounded)
            {
                ClearGroundedStates();
                AddState(PlayerState.Jumping);
                JumpCut = false;
                Play(SpriteSet.Animations["jumpascend"]);
            }
        }

        /// <summary>
        /// Updates the current state of the entity object.
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
        /// <param name="input">The <see cref="PlatformerInputBridge"/>
        /// to accept and process user inputs from.</param>
        public void Update(GameTime gameTime, PlatformerInputBridge input)
        {
            // update state
            Animator.Update(gameTime);

            // calculate delta time (in case rendering speed isn't stable)
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            IsGrounded = CurrentPosition.Y == GroundLevel;

            // output debug info
            Debugger.Write($"CurrentPosition.X: {CurrentPosition.X} | ");
            Debugger.Write($"CurrentPosition.Y: {CurrentPosition.Y} | ");
            Debugger.Write($"CurrentState: {CurrentState} | ");
            Debugger.WriteLine($"TotalGameTime.Milliseconds: " +
                $"{gameTime.TotalGameTime.TotalMilliseconds}");

            // execute actions based on input
            if (HasState(PlayerState.Dashing)) { ExecuteDash(input); }
            if (HasState(PlayerState.Jumping)) { ExecuteJump(input); }
            if (HasState(PlayerState.Falling)) { ExecuteFall(input); }

            // add some nifty aftereffects for dashing
            if (HasState(PlayerState.Dashing))
            {
                AfterImageTimer -= deltaTime;
                if (AfterImageTimer <= 0f)
                {
                    AfterImageTimer = AfterImageInterval;

                    // overwrite existing image, or expand list until limit
                    if (AfterImages.Count < 5) { AfterImages.Add(new AfterImage()); }
                    float trailOffset = AfterImageSeparation * AfterImages.Count;
                    float shadowX = CurrentPosition.X - 
                        (FacingRight ? trailOffset : -trailOffset);
                    AfterImages[AfterImageIndex] = new AfterImage
                    {
                        Position = new(shadowX, CurrentPosition.Y),
                        Flip = FacingRight ? 
                        SpriteEffects.None : SpriteEffects.FlipHorizontally,
                        Animation = Animator.GetCurrentAnimation(),
                        Timer = AfterImageLifetime
                    };
                    AfterImageIndex = (AfterImageIndex + 1) % AfterImages.Count;
                }
            }

            // check what to do if entity not "grounded"
            if (CurrentPosition.Y < GroundLevel &&
                !HasState(PlayerState.Jumping) &&
                !HasState(PlayerState.Falling))
            {
                ClearGroundedStates();
                AddState(PlayerState.Falling);
                Play(SpriteSet.Animations["jumpdescend"]);
            }

            // finish handling dashing aftereffect
            for (int i = 0; i < AfterImages.Count; i++)
            {
                AfterImage img = AfterImages[i];
                img.Timer -= deltaTime;
                AfterImages[i] = img; // struct reassignment (for value types)
            }

            // horizontal movement is managed independently
            ApplyHorizontalMovement(input);
        }

        #region action executors
        /// <summary>
        /// Executes a dash action.
        /// </summary>
        /// <param name="input">The <see cref="PlatformerInputBridge"/> to
        /// process the current input from.</param>
        private void ExecuteDash(PlatformerInputBridge input)
        {
            // by itself, dashing can only last a specific amount of time
            DashElapsedTime += deltaTime;

            // set player state and display corresponding animation
            // when dash is triggered
            bool dashExpired = DashElapsedTime >= DashDuration;
            bool dashReleased =
                input.GetInputState("dash") == InputState.Released;
            if ((dashExpired || dashReleased) && IsGrounded)
            {
                SetState(input.MoveLeft() || input.MoveRight() ?
                    PlayerState.Running : PlayerState.Idle);
                Play(SpriteSet.Animations
                    [HasState(PlayerState.Running) ? "run" : "idle"]);
            } // if airborne, preserve dashing state until grounded
        }

        /// <summary>
        /// Executes a jump action. Can overlap with dash action.
        /// </summary>
        /// <param name="input">The <see cref="PlatformerInputBridge"/> to
        /// process the current input from.</param>
        private void ExecuteJump(PlatformerInputBridge input)
        {
            // by itself, jumping can only last a specific amount of time
            JumpElapsedTime += deltaTime;

            // check if jump was interrupted before reaching max height
            if (!JumpCut && input.GetInputState("jump") == InputState.Released)
            { JumpCut = true; }
            if (!JumpCut) {
                VerticalVelocity = _physics.JumpVelocity *
                    (JumpAscentDuration - JumpElapsedTime) / JumpAscentDuration;
            }
            else { VerticalVelocity *= _physics.GravityDecay; }

            // calculate and apply vertical velocity
            VerticalVelocity = Math.Max(VerticalVelocity, 0f);
            CurrentPosition.Y -= VerticalVelocity;

            // set player state and display corresponding animation when
            // jump is triggered, and clear any related flags and counters
            if (JumpElapsedTime >= JumpAscentDuration || 
                input.GetInputState("jump") == InputState.Released)
            {
                RemoveState(PlayerState.Jumping);
                AddState(PlayerState.Falling);
                Play(SpriteSet.Animations["jumpdescend"]);
                JumpElapsedTime = 0f;
                JumpCut = false;
            }
        }

        /// <summary>
        /// Executes the falling action. Can overlap with dash action.
        /// </summary>
        /// <param name="input">The <see cref="PlatformerInputBridge"/> to
        /// process the current input from.</param>
        private void ExecuteFall(PlatformerInputBridge input)
        {
            // calculate applied gravity and vertical velocity
            VerticalVelocity += _physics.Gravity * deltaTime;
            VerticalVelocity = Math.Min(VerticalVelocity, MaxFallSpeed);
            CurrentPosition.Y += VerticalVelocity;

            // set sprite to grounded if Y position has reached ground level
            if (CurrentPosition.Y >= GroundLevel)
            {
                CurrentPosition.Y = GroundLevel;
                VerticalVelocity = 0f;
                SetState(PlayerState.None); // ensures fall artifacts are cleared

                // play corresponding animation if falling into idle or run states
                bool moving = input.MoveLeft() || input.MoveRight();
                if (moving)
                {
                    AddState(PlayerState.Running);
                    Animator.Play(SpriteSet.Animations["jumplandrun"], () =>
                    {
                        Play(SpriteSet.Animations["run"]);
                    });
                }
                else
                {
                    AddState(PlayerState.Idle);
                    Animator.Play(SpriteSet.Animations["jumplandidle"], () =>
                    {
                        Play(SpriteSet.Animations["idle"]);
                    });
                }
            }
        }
        #endregion

        /// <summary>
        /// Moves entity horizontally based on user input.
        /// </summary>
        /// <param name="input">The <see cref="PlatformerInputBridge"/> to
        /// process the current input from.</param>
        private void ApplyHorizontalMovement(PlatformerInputBridge input)
        {
            // calculate movement variance and direction
            float horizontal = 0f;
            if (input.MoveLeft()) { horizontal -= 1f; }
            if (input.MoveRight()) { horizontal += 1f; }

            // check if dashing action was triggered without movement;
            // it needs to work even if entity isn't moving
            if (horizontal == 0f)
            {
                // if dashing without directional input, dash anyway
                if (HasState(PlayerState.Dashing) && !IsAirborne)
                {
                    float inPlaceSpeed = FacingRight ?
                        _physics.DashSpeed : -_physics.DashSpeed;
                    CurrentPosition.X += inPlaceSpeed * deltaTime;
                }
                return;
            }

            // move entity according to state and direction
            float speed = HasState(PlayerState.Dashing)
                ? _physics.DashSpeed : _physics.RunSpeed;
            CurrentPosition.X += horizontal * speed * deltaTime;
        }
    }
}
