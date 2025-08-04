using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleGame.Core
{
    public class PuzzleGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public PuzzleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            bool keyComboPressed =
                GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed;


            if (keyComboPressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
