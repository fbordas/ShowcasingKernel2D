using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaVector = Microsoft.Xna.Framework.Vector2;

namespace MonoGame.Kernel2D.Animation
{
    public class AnimationPlayer
    {
        private SpriteAnimation? _currentAnim;
        private int _currentAnimIndex;
        private float _elapsedTime;
        public bool FacingRight = true;
        public string CurrentAnimationName = string.Empty;

        public bool HasFinishedPlaying =>
            _currentAnim != null &&
            !_currentAnim.Loop &&
            _currentAnimIndex >= _currentAnim.Frames.Count;

        public void Draw(SpriteBatch batch, Texture2D tex, XnaVector position, SpriteEffects fx)
        {
            if (_currentAnim == null || _currentAnimIndex >= _currentAnim.Frames.Count) { return; }
            var frame = _currentAnim.Frames[_currentAnimIndex];
            var origin = new XnaVector(frame.SourceRectangle.Width / 2f, frame.SourceRectangle.Height);
            batch.Draw(tex, position, frame.SourceRectangle, Color.White, 0f, origin, 2, fx, 0);
        }

        public void Draw(SpriteBatch batch, Texture2D tex, XnaVector position) =>
            Draw(batch, tex, position, SpriteEffects.None);

        public void Play(SpriteAnimation anim)
        {
            if (_currentAnim != anim)
            {
                _currentAnim = anim;
                _currentAnimIndex = 0;
            }
            CurrentAnimationName = anim.Name;
        }

        public void Update(GameTime gameTime)
        {
            if (_currentAnim == null) return;
            if (_currentAnimIndex >= _currentAnim.Frames.Count)
                return; // ✅ safety check before accessing any frame
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime >= _currentAnim.Frames[_currentAnimIndex].Duration)
            {
                _elapsedTime = 0f;
                _currentAnimIndex++;
                if (_currentAnimIndex >= _currentAnim.Frames.Count)
                {
                    if (_currentAnim.Loop)
                        _currentAnimIndex = 0;
                    else
                        _currentAnimIndex = _currentAnim
                            .Frames.Count - 1; // ✅ cap at final valid frame
                }
            }
        }

    }
}
