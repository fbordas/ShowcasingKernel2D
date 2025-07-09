using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Kernel2D.Animation;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Input;
using MonoGame.Kernel2D.Screens;

namespace PlatformingProject.Core
{
    public class PlatformingGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _input.Update();
            PollInput();

            BuildWindowTitle();

            //_manager.CurrentScreen.ExitRequested ??= Exit;
            _manager.CurrentScreen.ExitRequested += Exit;
            
            base.Update(gameTime);
        }

        private void PollInput()
        {
            //player.ProcessPlayerActions(_input); 
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            base.Draw(gameTime);

            //context ??= new DrawContext(sb, Matrix.Identity, GraphicsDevice, gameTime, _font);

            string display = Window.Title;

            sb.Begin();
            //player.Draw(gameTime);
            sb.DrawString(_font, display, new Vector2(10, 10), Color.White);
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
        private Spritesheet sheet = null;
        private SpriteFont _font = null;
        private readonly PlatformerInputBridge _input = new();
        private ScreenManager _manager = null;
        private DrawContext context;
    }
}
