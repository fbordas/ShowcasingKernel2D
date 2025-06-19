using EmptyProject.Core.BaseLogicComponents;
using XVector = Microsoft.Xna.Framework.Vector2;
using MonoGame.Kernel2D.Animation;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

#pragma warning disable
namespace EmptyProject.Core
{
    public class PlayerCharacter
    {
        private PlayerState CurrentState = PlayerState.Idle;
        public PlayerState GetState() => this.CurrentState;
        private bool FacingRight = true;
        private XVector CurrentPosition;
        private AnimationPlayer Animator = null;
        private Spritesheet Sprites = null;
        private Texture2D PlayerSpriteTexture = null;
        private readonly PhysicsValues _physics = PhysicsValues.Default();
        private SpriteBatch Batch = null;

        public PlayerCharacter(XVector position, SpriteBatch batch, Spritesheet sprites, Texture2D texture)
        {
            CurrentPosition = position;
            Animator = new();
            Batch = batch;
            Sprites = sprites;
            PlayerSpriteTexture = texture;
            Animator.Play(Sprites.Animations["idle"]);
        }

        public void HandleInput(PlatformerInputBridge _input)
        {
            if (_input.IsIdle())
            {
                CurrentState = PlayerState.Idle;
                Animator.Play(Sprites.Animations["idle"]);
                return;
            }

            if (_input.MoveLeft()) FacingRight = false;
            else if (_input.MoveRight()) FacingRight = true;

            bool moving = _input.MoveLeft() || _input.MoveRight();
            if (moving)
            {
                float speed = FacingRight ? _physics.RunSpeed : -_physics.RunSpeed;
                CurrentPosition = new(CurrentPosition.X + speed, CurrentPosition.Y);
            }

            if (CurrentState != PlayerState.Dashing && CurrentState != PlayerState.Jumping)
            {
                if (CurrentState != PlayerState.Running)
                    CurrentState = PlayerState.Running;

                if (Animator.CurrentAnimationName != "run")
                    Animator.Play(Sprites.Animations["run"]);
            }

            // TODO: all this shit about dashing and jumping
            if (_input.DashPressed())
            {
                CurrentState = PlayerState.Dashing;
            }

            if (_input.JumpPressed())
            {
                CurrentState = PlayerState.Jumping;
            }
        }

        public void Update(GameTime gameTime, PlatformerInputBridge input)
        {
            Animator.Update(gameTime);
            if (CurrentState == PlayerState.Dashing && Animator.HasFinishedPlaying)
            {
                Animator.Play(Sprites.Animations["idle"]);
                CurrentState = PlayerState.Idle;
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