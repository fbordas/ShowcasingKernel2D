using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame.Kernel2D.Screens
{
    public abstract class MenuScreen
    {
        public virtual void LoadContent(ContentManager content) { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch batch) { }
    }
}