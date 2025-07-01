using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D;
using MonoGame.Kernel2D.Animation;
using Debugger = MonoGame.Kernel2D.Helpers.DebugHelpers;
using XVector = Microsoft.Xna.Framework.Vector2;

#pragma warning disable
namespace EmptyProject.Core
{
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


    public class PlatformerPlayerCharacter
    {
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
        private bool IsGrounded = true;
        private bool IsLanding = false;
        private bool JumpCut = false;
        #endregion

        #region state helpers
        private PlayerState CurrentState;
        public PlayerState GetState() => this.CurrentState;
        private bool HasState(PlayerState state) => (CurrentState & state) != 0;
        private void AddState(PlayerState state) => CurrentState |= state;
        private void RemoveState(PlayerState state) => CurrentState &= ~state;
        private void SetState(PlayerState state) => CurrentState = state;
        #endregion

        #region init
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
        public void Draw(GameTime gameTime) =>
            Animator.Draw(Batch, PlayerSpriteTexture, CurrentPosition,
                FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);

        public void Play(SpriteAnimation anim) => Animator.Play(anim);

        private void ClearGroundedStates()
        {
            RemoveState(PlayerState.Idle);
            RemoveState(PlayerState.Running);
        }
        #endregion

        public void ProcessPlayerActions(PlatformerInputBridge input)
        {
            if (HasState(PlayerState.Landing)) { return; }

            if (input.IsIdle() && IsGrounded)
            {
                SetState(PlayerState.Idle);
                Play(SpriteSet.Animations["idle"]);
                return;
            }

            if (input.MoveLeft()) { FacingRight = false; }
            else if (input.MoveRight()) { FacingRight = true; }

            bool moving = input.MoveRight() || input.MoveLeft();
            if (moving && IsGrounded && !IsAirborne)
            {
                if (!HasState(PlayerState.Running))
                {
                    SetState(PlayerState.Running);
                    Play(SpriteSet.Animations["run"]);
                }
            }

            // Dash (only if grounded and not already dashing)
            bool canStartDash = IsGrounded && !HasState(PlayerState.Dashing)
                && !HasState(PlayerState.Jumping) && !HasState(PlayerState.Falling);
            if (input.GetInputState("dash") == InputState.Pressed && canStartDash)
            {
                AddState(PlayerState.Dashing);
                DashElapsedTime = 0f;
                Play(SpriteSet.Animations["dash"]);
            }

            // Jump (even during dash)
            if (input.GetInputState("jump") == InputState.Pressed && IsGrounded)
            {
                ClearGroundedStates();
                AddState(PlayerState.Jumping);
                JumpCut = false;
                Play(SpriteSet.Animations["jumpascend"]);
            }
        }

        public void Update(GameTime gameTime, PlatformerInputBridge input)
        {
            Animator.Update(gameTime);
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            IsGrounded = CurrentPosition.Y == GroundLevel;
            Debugger.DebugMessage($"CurrentPosition.X: {CurrentPosition.X} | CurrentPosition.Y: {CurrentPosition.Y}");
            Debugger.DebugMessage($"CurrentState: {CurrentState} | TotalGameTime.Milliseconds: {gameTime.TotalGameTime.TotalMilliseconds}");
            if (HasState(PlayerState.Dashing)) { ExecuteDash(input); }
            if (HasState(PlayerState.Jumping)) { ExecuteJump(input); }
            if (HasState(PlayerState.Falling)) { ExecuteFall(input); }

            if (CurrentPosition.Y < GroundLevel &&
                !HasState(PlayerState.Jumping) &&
                !HasState(PlayerState.Falling))
            {
                ClearGroundedStates();
                AddState(PlayerState.Falling);
                Play(SpriteSet.Animations["jumpdescend"]);
            }
            ApplyHorizontalMovement(input);
        }

        #region action executors
        private void ExecuteDash(PlatformerInputBridge input)
        {
            DashElapsedTime += deltaTime;

            bool dashExpired = DashElapsedTime >= DashDuration;
            bool dashReleased = input.GetInputState("dash") == InputState.Released;

            if ((dashExpired || dashReleased) && IsGrounded)
            {
                SetState(input.MoveLeft() || input.MoveRight() ? PlayerState.Running : PlayerState.Idle);
                Play(SpriteSet.Animations[HasState(PlayerState.Running) ? "run" : "idle"]);
            } // if airborne, preserve Dashing state until grounded
        }


        private void ExecuteJump(PlatformerInputBridge input)
        {
            JumpElapsedTime += deltaTime;
            if (!JumpCut && input.GetInputState("jump") == InputState.Released)
            { JumpCut = true; }
            if (!JumpCut) {
                VerticalVelocity = _physics.JumpVelocity *
                    (JumpAscentDuration - JumpElapsedTime) / JumpAscentDuration;
            }
            else { VerticalVelocity *= _physics.GravityDecay; }

            VerticalVelocity = Math.Max(VerticalVelocity, 0f);
            CurrentPosition.Y -= VerticalVelocity;

            if (JumpElapsedTime >= JumpAscentDuration || input.GetInputState("jump") == InputState.Released)
            {
                RemoveState(PlayerState.Jumping);
                AddState(PlayerState.Falling);
                Play(SpriteSet.Animations["jumpdescend"]);
                JumpElapsedTime = 0f;
                JumpCut = false;
            }
        }

        private void ExecuteFall(PlatformerInputBridge input)
        {
            VerticalVelocity += _physics.Gravity * deltaTime;
            VerticalVelocity = Math.Min(VerticalVelocity, MaxFallSpeed);
            CurrentPosition.Y += VerticalVelocity;

            if (CurrentPosition.Y >= GroundLevel)
            {
                CurrentPosition.Y = GroundLevel;
                VerticalVelocity = 0f;
                SetState(PlayerState.None); // ensures fall artifacts are cleared

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

        private void ApplyHorizontalMovement(PlatformerInputBridge input)
        {
            float horizontal = 0f;
            if (input.MoveLeft()) { horizontal -= 1f; }
            if (input.MoveRight()) { horizontal += 1f; }

            if (horizontal == 0f)
            {
                // if dashing without directional input
                if (HasState(PlayerState.Dashing) && !IsAirborne)
                {
                    float inPlaceSpeed = FacingRight ? _physics.DashSpeed : -_physics.DashSpeed;
                    CurrentPosition.X += inPlaceSpeed * deltaTime;
                }
                return;
            }

            float speed = HasState(PlayerState.Dashing)
                ? _physics.DashSpeed : _physics.RunSpeed;
            CurrentPosition.X += horizontal * speed * deltaTime;
        }
    }
}
