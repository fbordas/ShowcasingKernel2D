using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.Kernel2D.Screens
{
    public class ScreenManager
    {
        private GameScreen? _currentScreen = null;

        public void LoadScreen(GameScreen screen, ContentManager content)
        { 
            _currentScreen?.UnloadContent();
            _currentScreen = screen;
            _currentScreen.LoadContent(content);
        }

        public void Update(GameTime gameTime) => _currentScreen?.Update(gameTime);

        public void Draw(SpriteBatch batch) => _currentScreen?.Draw(batch);
    }
}
