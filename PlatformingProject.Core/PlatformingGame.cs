using System;
using System.Linq;

using Kernel2D.Drawing;
using Kernel2D.Input;
using Kernel2D.Input.Bridges;
using Kernel2D.Input.Bridges.Menu;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using PlatformingProject.Core.Screens;

using Debugger = Kernel2D.Helpers.DebugHelpers;

namespace PlatformingProject.Core
{
    public class PlatformingGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private int BaseWidth = 1280;
        private int BaseHeight = 960;

        /// <summary>
        /// Indicates if the game is running on a mobile platform.
        /// </summary>
        public readonly static bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();

        /// <summary>
        /// Indicates if the game is running on a desktop platform.
        /// </summary>
        public readonly static bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

        public PlatformingGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Services.AddService(typeof(GraphicsDeviceManager), _graphics);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            _manager = ScreenManager.Instance;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = BaseWidth;
            _graphics.PreferredBackBufferHeight = BaseHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>(@"Fonts\BaseText");

            var transIn = new FadeTransition(1f, true, Color.White);

            _manager.RegisterScreen("OptionsSubMenu", 
                new OptionsSubScreen(Content, _font, InputBridgeContainer.Get<HIDMenuInputBridge>()));
            _manager.RegisterScreen("SplashScreen", new SplashScreen(_font));
            _manager.RegisterScreen("TitleScreen", 
                new TitleScreen(Content, _font, InputBridgeContainer.Get<HIDMenuInputBridge>()));
            _manager.RegisterScreen("OptionsScreen", 
                new OptionsScreen(Content, _font, InputBridgeContainer.Get<HIDMenuInputBridge>()));
            _manager.RegisterScreen("GameplayScreen", 
                new PlatformerGameTestScreen(Content, InputBridgeContainer.Get<PlatformerInputBridge>()));
            _manager.ChangeScreen("SplashScreen", Content, new ScreenTransitionPair(null, transIn));
        }

        protected override void Update(GameTime gameTime)
        {
            Debugger.WriteLine($"Game.Update() | {gameTime.TotalGameTime.TotalMilliseconds}");
            bool keyComboPressed =
                GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed;


            if (keyComboPressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            BuildWindowTitle();

            /*
             * A reminder of my failures and how I caused Roslyn to crash to the point that I didn't even
             * get a proper error message to debug: 
             * _manager.CurrentScreen.ExitRequested ??= Exit;
             */
            if (_manager.CurrentScreen != null)
            {
                _manager.Update(gameTime);
                _manager.CurrentScreen.ExitRequested += Exit;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Debugger.WriteLine($"Game.Draw()   | {gameTime.TotalGameTime.TotalMilliseconds}");
            GraphicsDevice.Clear(Color.Gray);
            base.Draw(gameTime);

            context ??= new DrawContext
                (drawingqueue, Matrix.Identity, GraphicsDevice, gameTime);

            if (context.DrawingQueue == null)
            {
                Debugger.WriteLine("SpriteBatch in DrawContext currently null!");
                return;
            }

            context.DrawingQueue.ClearQueue();
            _manager.Draw(context);
            sb.Begin(transformMatrix: context.TransformMatrix);
            context.DrawingQueue.Flush(sb);
            sb.End();

            base.Draw(gameTime);
        }

        private void BuildWindowTitle()
        {
            var keyboard = Keyboard.GetState();
            var gamepad = GamePad.GetState(PlayerIndex.One);
            var keys = keyboard.GetPressedKeys();
            string keyString = keys.Length > 0
                ? $"Keys: {string.Join(", ", keys.Select(k => k.ToString()))}"
                : "Keys: None";
            string padButtons = "Buttons: ";
            if (gamepad.IsConnected)
            {
                padButtons += string.Join(", ", new[]
                {
                    gamepad.Buttons.A == ButtonState.Pressed ? "A" : null,
                    gamepad.Buttons.B == ButtonState.Pressed ? "B" : null,
                    gamepad.Buttons.X == ButtonState.Pressed ? "X" : null,
                    gamepad.Buttons.Y == ButtonState.Pressed ? "Y" : null,
                    gamepad.Buttons.LeftShoulder == ButtonState.Pressed ? "LB" : null,
                    gamepad.Buttons.RightShoulder == ButtonState.Pressed ? "RB" : null,
                    gamepad.DPad.Left == ButtonState.Pressed ? "DPad.Left" : null,
                    gamepad.DPad.Right == ButtonState.Pressed ? "DPad.Right" : null,
                    gamepad.DPad.Up == ButtonState.Pressed ? "DPad.Up" : null,
                    gamepad.DPad.Down == ButtonState.Pressed ? "DPad.Down" : null
                }.Where(b => b != null));
            }
            else
            { padButtons += "None (Gamepad not connected)"; }
            Window.Title = $"{keyString} | {padButtons}";
        }

        private SpriteBatch sb = null;
        private SpriteFont _font = null;
        private readonly ScreenManager _manager = null;
        private DrawContext context;
        private readonly DrawQueue drawingqueue = new();
    }
}