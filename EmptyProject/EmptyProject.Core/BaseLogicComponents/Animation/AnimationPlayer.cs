using Microsoft.Xna.Framework.Graphics;
using XnaVector = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework;

namespace EmptyProject.Core.BaseLogicComponents.Animation
{
    internal class AnimationPlayer
    {
        private SpriteAnimation _currentAnim;
        private int _currentAnimIndex;

        internal void Draw(SpriteBatch batch, Texture2D tex, XnaVector position)
        {
            if (_currentAnim == null) { return; }

            var frame = _currentAnim.Frames[_currentAnimIndex];
            batch.Draw(tex, position, frame.SourceRectangle, Color.White);
        }

        internal void Play(SpriteAnimation anim)
        {
            if (_currentAnim != anim)
            { 
                _currentAnim = anim;
                _currentAnimIndex = 0;
            }
        }
    }
}
