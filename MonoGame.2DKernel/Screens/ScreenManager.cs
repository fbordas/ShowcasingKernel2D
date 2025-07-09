using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Kernel2D.Drawing;

namespace MonoGame.Kernel2D.Screens
{
    public class ScreenManager
    {
        private readonly Dictionary<string, ScreenBase> _screens = [];
        private readonly Dictionary<string, ScreenTransition> _transitions = [];
        private ScreenBase? _currentScreen;
        private static ScreenManager? _instance = null;
        public ScreenBase? CurrentScreen => _currentScreen;

        public void ChangeScreen(string screenName, ContentManager content)
        {
            if (_screens.TryGetValue(screenName, out var nextScreen))
            {
                _currentScreen?.UnloadContent();
                _currentScreen = nextScreen;
                _currentScreen.LoadContent(content);
            }
            else { throw new KeyNotFoundException($"Screen '{screenName}' not found."); }
        }

        public void Update(GameTime gameTime) => _currentScreen?.Update(gameTime);

        public void Draw(DrawContext context) => _currentScreen?.Draw(context);

        public static ScreenManager Instance
        {
            get
            {
                _instance ??= new ScreenManager();
                return _instance;
            }
        }

        public void RegisterScreen(string name, ScreenBase screen)
        { _screens[name] = screen; }
    }
}