using System;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Spritesheet Sprites = null;
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

        #region input checks
        private bool JumpInterrupted = false;
        private bool JumpHeld = false;
        #endregion

        public PlatformerPlayerCharacter(XVector position, SpriteBatch batch, Spritesheet sprites, Texture2D texture, SpriteFont font)
        {
            IsGrounded = CurrentPosition.Y == GroundLevel;
            _font = font;
            CurrentPosition = position;
            GroundLevel = position.Y;
            Animator = new();
            Batch = batch;
            Sprites = sprites;
            PlayerSpriteTexture = texture;
            DashDuration = Sprites.Animations["dash"].Frames.Sum(f => f.Duration) / 1000f;
            JumpAscentDuration = Sprites.Animations["jumpascend"].Frames.Sum(f => f.Duration) / 1000f;
            Animator.Play(Sprites.Animations["idle"]);
        }

        public void HandleInput(PlatformerInputBridge _input)
        {
            if (CurrentState == PlayerState.Landing) return;

            // Ignore all input if in the middle of a dash
            // This is just initial behavior, will change later to allow composite actions
            if (CurrentState == PlayerState.Dashing) return;

            // IDLE
            if (_input.IsIdle() && CurrentPosition.Y == GroundLevel) // LAND, GODDAMMIT
            {
                if (CurrentState != PlayerState.Idle)
                {
                    CurrentState = PlayerState.Idle;
                    Animator.Play(Sprites.Animations["idle"]);
                }
                return;
            }

            // LEFT/RIGHT movement
            if (_input.MoveLeft()) FacingRight = false;
            else if (_input.MoveRight()) FacingRight = true;

            bool moving = _input.MoveLeft() || _input.MoveRight();
            if (moving && IsGrounded && !IsAirborne)
            {
                float speed = FacingRight ? _physics.RunSpeed : -_physics.RunSpeed;
                CurrentPosition = new(CurrentPosition.X + speed, CurrentPosition.Y);

                if (CurrentState != PlayerState.Running)
                {
                    CurrentState = PlayerState.Running;
                    Animator.Play(Sprites.Animations["run"]);
                }
            }

            // DASHING
            if (_input.InputPressed("dash"))
            {
                CurrentState = PlayerState.Dashing;
                DashElapsedTime = 0f;
                Animator.Play(Sprites.Animations["dash"]);
            }


            // JUMPING
            if (_input.InputHeld("jump") && CurrentState != PlayerState.Falling)
            {
                if (!JumpHeld)
                {
                    CurrentState = PlayerState.Jumping;
                    Animator.Play(Sprites.Animations["jumpascend"]);
                    JumpHeld = true;
                }
            }

            if (CurrentState == PlayerState.Jumping && !_input.InputHeld("jump")) JumpInterrupted = true;

            if (!_input.InputHeld("jump")) JumpHeld = false;
        }

        public void Update(GameTime gameTime, PlatformerInputBridge input)
        {
            Animator.Update(gameTime);
            IsGrounded = CurrentPosition.Y == GroundLevel;
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Debugger.DebugMessage($"Y: {CurrentPosition.Y} | State: {CurrentState}");
            if (CurrentState == PlayerState.Dashing)
            {
                DashElapsedTime += deltaTime;

                float speed = FacingRight ? _physics.DashSpeed : -_physics.DashSpeed;
                CurrentPosition.X += speed;

                if (DashElapsedTime >= DashDuration)
                {
                    CurrentState = PlayerState.Idle;
                    Animator.Play(Sprites.Animations["idle"]);
                }
            }

            if (CurrentState == PlayerState.Jumping)
            {
                JumpElapsedTime += deltaTime;
                VerticalVelocity = _physics.JumpVelocity * (JumpAscentDuration - JumpElapsedTime) / JumpAscentDuration;
                VerticalVelocity = Math.Max(VerticalVelocity, 0f);
                CurrentPosition.Y -= VerticalVelocity;
                if (JumpInterrupted || JumpElapsedTime >= JumpAscentDuration)
                {
                    CurrentState = PlayerState.Falling;
                    Animator.Play(Sprites.Animations["jumpdescend"]);
                    JumpElapsedTime = 0f;
                    JumpInterrupted = false;
                }
            }


            if (CurrentState == PlayerState.Falling)
            {
                VerticalVelocity += _physics.Gravity * deltaTime;
                VerticalVelocity = Math.Min(VerticalVelocity, MaxFallSpeed);
                CurrentPosition.Y += VerticalVelocity;
                if (CurrentPosition.Y >= GroundLevel)
                {
                    CurrentPosition.Y = GroundLevel;
                    VerticalVelocity = 0f;
                    //JumpHeld = false;
                    if (input.MoveLeft() || input.MoveRight())
                    {
                        CurrentState = PlayerState.Running;
                        Animator.Play(Sprites.Animations["jumplandrun"], () =>
                            {
                                CurrentState = PlayerState.Running;
                                Animator.Play(Sprites.Animations["run"]);
                            }
                        );
                    }
                    else
                    {
                        CurrentState = PlayerState.Idle;
                        Animator.Play(Sprites.Animations["jumplandidle"], () =>
                            {
                                CurrentState = PlayerState.Idle;
                                Animator.Play(Sprites.Animations["idle"]);
                            }
                        );
                    }
                }
            }

            if (CurrentState == PlayerState.Jumping || CurrentState == PlayerState.Falling)
            {
                float horizontal = 0f;
                if (input.MoveLeft()) { horizontal -= 1f; }
                if (input.MoveRight()) { horizontal += 1f; }
                CurrentPosition.X += horizontal * _physics.AirborneSpeed;// * deltaTime;
            }

            if (CurrentPosition.Y < GroundLevel && CurrentState != PlayerState.Jumping && CurrentState != PlayerState.Falling)
            {
                CurrentState = PlayerState.Falling;
                Animator.Play(Sprites.Animations["jumpdescend"]);
            }

            if (IsGrounded && !input.MoveLeft() && !input.MoveRight() && CurrentState != PlayerState.Landing)
            {
                CurrentState = PlayerState.Idle;
                Animator.Play(Sprites.Animations["idle"]);
            }
        }

        public void Draw(GameTime gameTime)
        {
            Animator.Draw(Batch, PlayerSpriteTexture, CurrentPosition, 
                FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }

        public void Play(SpriteAnimation anim) => Animator.Play(anim);

        private void DebugMsg(string msg) => System.Diagnostics.Debug.WriteLine(msg);

    }
}