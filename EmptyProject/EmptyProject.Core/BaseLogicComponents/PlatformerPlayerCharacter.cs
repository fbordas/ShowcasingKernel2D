using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Animation;
using static System.Net.Mime.MediaTypeNames;
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
        JumpingAscent,
        Falling,
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
        private const float Gravity = 0.5f;
        private const float MaxFallSpeed = 3f;
        private float GroundLevel;
        private float deltaTime = 0f;
        private bool JumpInterrupted = false;
        private SpriteFont _font;
        #endregion

        public PlatformerPlayerCharacter(XVector position, SpriteBatch batch, Spritesheet sprites, Texture2D texture, SpriteFont font)
        {
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
            // Ignore all input if in the middle of a dash or jump
            // This is just initial behavior, will change later to allow composite actions
            if (CurrentState == PlayerState.Dashing || CurrentState == PlayerState.JumpingAscent)
                return;

            // IDLE
            if (_input.IsIdle())
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
            if (moving)
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
            if (_input.InputHeld("jump"))
            {
                CurrentState = PlayerState.JumpingAscent;
                Animator.Play(Sprites.Animations["jumpascend"]);
            }

            if (CurrentState == PlayerState.JumpingAscent && !_input.InputHeld("jump"))
                JumpInterrupted = true;
        }

        public void Update(GameTime gameTime, PlatformerInputBridge input)
        {
            Animator.Update(gameTime);
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
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

            if (CurrentState == PlayerState.JumpingAscent)
            {
                JumpElapsedTime += deltaTime;
                VerticalVelocity = _physics.JumpVelocity * (JumpAscentDuration - JumpElapsedTime) / JumpAscentDuration;
                VerticalVelocity = Math.Max(VerticalVelocity, 0f); // Prevent negative velocity
                CurrentPosition.Y -= VerticalVelocity;
                if (JumpElapsedTime >= JumpAscentDuration)
                {
                    CurrentState = PlayerState.Falling;
                    DrawDebugString($"Y: {CurrentPosition.Y} | State: {CurrentState}");

                    Animator.Play(Sprites.Animations["jumpdescend"]);
                    JumpElapsedTime = 0f;
                    JumpInterrupted = false; // Reset jump interruption state
                }

            }

            if (CurrentState == PlayerState.Falling)
            {
                //throw new Exception(); // KABOOM!
                VerticalVelocity += Gravity * deltaTime;
                VerticalVelocity = Math.Min(VerticalVelocity, MaxFallSpeed);
                CurrentPosition.Y += VerticalVelocity;
                if (CurrentPosition.Y >= GroundLevel)
                {
                    CurrentPosition.Y = GroundLevel;// Reset to ground level
                    CurrentState = PlayerState.Idle;
                    Animator.Play(Sprites.Animations["idle"]);
                    VerticalVelocity = 0f; // Reset vertical velocity
                }
            }

            if (CurrentPosition.Y < GroundLevel && CurrentState != PlayerState.JumpingAscent && CurrentState != PlayerState.Falling)
            {
                CurrentState = PlayerState.Falling;
                Animator.Play(Sprites.Animations["jumpdescend"]);
            }
        }

        public void Draw(GameTime gameTime)
        {
            Animator.Draw(Batch, PlayerSpriteTexture, CurrentPosition, 
                FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }

        public void Play(SpriteAnimation anim) => Animator.Play(anim);

        private void DrawDebugString(string str)
        {
            Batch.Begin();
            Batch.DrawString(_font, str, new XVector(10, 40), Color.Blue);
            Batch.End();

        }
    }
}