using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Animation;
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
        #endregion

        public PlatformerPlayerCharacter(XVector position, SpriteBatch batch, Spritesheet sprites, Texture2D texture)
        {
            CurrentPosition = position;
            Animator = new();
            Batch = batch;
            Sprites = sprites;
            PlayerSpriteTexture = texture;
            DashDuration = Sprites.Animations["dash"].Frames.Sum(f => f.Duration);
            JumpAscentDuration = Sprites.Animations["jumpascend"].Frames.Sum(f => f.Duration);
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


            // TODO: Add jump management here
            // JUMPING
            //if (_input.InputHeld("jump"))
            //{
            //    CurrentState = PlayerState.JumpingAscent;
            //}
        }

        public void Update(GameTime gameTime, PlatformerInputBridge input)
        {
            Animator.Update(gameTime);
            if (CurrentState == PlayerState.Dashing)
            {
                DashElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                float speed = FacingRight ? _physics.DashSpeed : -_physics.DashSpeed;
                CurrentPosition.X += speed;

                if (DashElapsedTime >= DashDuration)
                {
                    CurrentState = PlayerState.Idle;
                    Animator.Play(Sprites.Animations["idle"]);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            Animator.Draw(Batch, PlayerSpriteTexture, CurrentPosition, 
                FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }

        public void Play(SpriteAnimation anim) => Animator.Play(anim);
    }
}