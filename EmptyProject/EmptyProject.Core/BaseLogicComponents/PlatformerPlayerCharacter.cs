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
        Idle,
        Dashing,
        Running,
        Jumping,
        Falling,
        Landing,
        Shooting,
        Slashing,
        TakingDamage,
        Climbing,
        WallSliding,
        EnteringDoor
    }

    public class PlatformerPlayerCharacter
    {
        #region internal resources
        private AnimationPlayer Animator = null;
        private Spritesheet SpriteSet = null;
        private Texture2D PlayerSpriteTexture = null;
        private readonly PhysicsValues _physics = PhysicsValues.Default();
        private SpriteBatch Batch = null;
        #endregion

        #region player values
        public PlayerState GetState() => this.CurrentState;
        private PlayerState CurrentState = PlayerState.Idle;
        private bool FacingRight = true;
        #endregion

        #region physics values
        private XVector CurrentPosition;
        private float DashElapsedTime = 0f;
        private readonly float DashDuration;
        private float JumpElapsedTime = 0f;
        private readonly float JumpAscentDuration;
        private float VerticalVelocity = 0f;
        private const float MaxFallSpeed = 20f;
        private readonly float GroundLevel;
        private float deltaTime = 0f;
        private SpriteFont _font;
        #endregion

        #region physics states
        private bool IsAirborne => CurrentState == PlayerState.Jumping || CurrentState == PlayerState.Falling;
        private bool IsGrounded = true;
        private bool IsLanding = false;
        #endregion

        #region basic functions
        public PlatformerPlayerCharacter(XVector position, SpriteBatch batch, Spritesheet sprites, Texture2D texture, SpriteFont font)
        {
            _font = font;
            CurrentPosition = position;
            GroundLevel = position.Y;
            IsGrounded = CurrentPosition.Y == GroundLevel;
            Animator = new();
            Batch = batch;
            SpriteSet = sprites;
            PlayerSpriteTexture = texture;
            DashDuration = SpriteSet.Animations["dash"].Frames.Sum(f => f.Duration) / 1000f;
            JumpAscentDuration = SpriteSet.Animations["jumpascend"].Frames.Sum(f => f.Duration) / 1000f;
            Animator.Play(SpriteSet.Animations["idle"]);
        }

        public void Draw(GameTime gameTime) =>
            Animator.Draw(Batch, PlayerSpriteTexture, CurrentPosition,
                FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        #endregion

        public void Play(SpriteAnimation anim) => Animator.Play(anim);

        public void ProcessPlayerActions(PlatformerInputBridge input)
        {
            if (input.IsIdle() && IsGrounded)
            {
                if (CurrentState != PlayerState.Idle)
                {
                    CurrentState = PlayerState.Idle;
                    Animator.Play(SpriteSet.Animations["idle"]);
                }
                return;
            }

            // Run
            if (input.MoveLeft()) FacingRight = false;
            else if (input.MoveRight()) FacingRight = true;

            if ((input.MoveLeft() || input.MoveRight()) && IsGrounded && !IsAirborne)
            {
                float speed = FacingRight ? _physics.RunSpeed : -_physics.RunSpeed;
                CurrentPosition.X += speed * deltaTime;

                if (CurrentState != PlayerState.Running)
                {
                    CurrentState = PlayerState.Running;
                    Animator.Play(SpriteSet.Animations["run"]);
                }
            }

            // Dash
            if (input.GetInputState("dash") == InputState.Pressed)
            {
                CurrentState = PlayerState.Dashing;
                DashElapsedTime = 0f;
                Animator.Play(SpriteSet.Animations["dash"]);
            }

            // Jump
            if (input.GetInputState("jump") == InputState.Pressed && IsGrounded)
            {
                CurrentState = PlayerState.Jumping;
                Animator.Play(SpriteSet.Animations["jumpascend"]);
            }
        }

        public void Update(GameTime gameTime, PlatformerInputBridge input)
        { 
            Animator.Update(gameTime);
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds / 1000f;
            // this will need rewriting once collisions start being considered
            IsGrounded = CurrentPosition.Y == GroundLevel;

            switch (CurrentState)
            {
                case PlayerState.Dashing:
                    ExecuteDash();
                    break;
                case PlayerState.Jumping:
                    ExecuteJump();
                    break;
                case PlayerState.Falling:
                    ExecuteFall(input);
                    break;
            }

            if (CurrentPosition.Y < GroundLevel &&
                CurrentState != PlayerState.Jumping &&
                CurrentState != PlayerState.Falling)
            {
                CurrentState = PlayerState.Falling;
                Animator.Play(SpriteSet.Animations["jumpdescend"]);
            }

            ApplyAirborneHorizontalMovement(input);
        }

        #region special actions
        private void ExecuteDash()
        {
            DashElapsedTime += deltaTime;
            float speed = FacingRight ? _physics.DashSpeed : -_physics.DashSpeed;
            CurrentPosition.X += speed * deltaTime;
            if (DashElapsedTime >= DashDuration)
            {
                CurrentState = PlayerState.Idle;
                Animator.Play(SpriteSet.Animations["idle"]);
            }
        }

        private void ExecuteJump()
        {
            JumpElapsedTime += deltaTime;
            VerticalVelocity = _physics.JumpVelocity *
                (JumpAscentDuration - JumpElapsedTime) / JumpAscentDuration;
            VerticalVelocity = Math.Max(VerticalVelocity, 0f);
            CurrentPosition.Y -= VerticalVelocity;
            if (JumpElapsedTime >= JumpAscentDuration)
            {
                CurrentState = PlayerState.Falling;
                Animator.Play(SpriteSet.Animations["jumpdescend"]);
                JumpElapsedTime = 0f;
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
                if (input.MoveLeft() || input.MoveRight())
                {
                    CurrentState = PlayerState.Running;
                    Animator.Play(SpriteSet.Animations["jumplandrun"], () =>
                    {
                        CurrentState = PlayerState.Running;
                        Animator.Play(SpriteSet.Animations["run"]);
                    }
                    );
                }
                else
                {
                    CurrentState = PlayerState.Idle;
                    Animator.Play(SpriteSet.Animations["jumplandidle"], () =>
                    {
                        CurrentState = PlayerState.Idle;
                        Animator.Play(SpriteSet.Animations["idle"]);
                    }
                    );
                }
            }
        }

        private void ApplyAirborneHorizontalMovement(PlatformerInputBridge input)
        {
            if (CurrentState != PlayerState.Jumping && CurrentState != PlayerState.Falling)
            { return; }

            float horizontal = 0f;
            if (input.MoveLeft()) { horizontal -= 1f; }
            if (input.MoveRight()) { horizontal += 1f; }
            CurrentPosition.X += horizontal * _physics.AirborneSpeed * deltaTime;
        }
        #endregion
    }
}