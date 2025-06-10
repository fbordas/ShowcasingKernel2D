using System;
using EmptyProject.Core.BaseLogicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using K2D = MonoGame.Kernel2D.Animation;

namespace EmptyProject.Core
{
    /// <summary>
    /// The main class for the game, responsible for managing game components, settings, 
    /// and platform-specific configurations.
    /// </summary>
    public class EmptyProjectGame : Game
    {
        // Resources for drawing.
        private GraphicsDeviceManager graphicsDeviceManager;

        /// <summary>
        /// Indicates if the game is running on a mobile platform.
        /// </summary>
        public readonly static bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();

        /// <summary>
        /// Indicates if the game is running on a desktop platform.
        /// </summary>
        public readonly static bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

        /// <summary>
        /// Initializes a new instance of the game. Configures platform-specific settings, 
        /// initializes services like settings and leaderboard managers, and sets up the 
        /// screen manager for screen transitions.
        /// </summary>
        public EmptyProjectGame()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            // Share GraphicsDeviceManager as a service.
            Services.AddService(typeof(GraphicsDeviceManager), graphicsDeviceManager);

            Content.RootDirectory = "Content";

            // Configure screen orientations.
            graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Initializes the game, including setting up localization and adding the 
        /// initial screens to the ScreenManager.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }


        /// <summary>
        /// Updates the game's logic, called once per frame.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values used for game updates.
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            PollController();

            ap.Update(gameTime);

            if (currentState == PlayerState.Dashing && ap.HasFinishedPlaying)
            { 
                ap.Play(sheet.Animations["idle"]);
                currentState = PlayerState.Idle;
            }
            
            base.Update(gameTime);
        }

        private void PollController()
        {
            // Poll the controller for input.
            var state = GamePad.GetState(PlayerIndex.One);
            if (state.IsConnected)
            {
                if (state.Buttons.RightShoulder == ButtonState.Pressed &&
                    currentState == PlayerState.Idle)
                {
                    ap.Play(sheet.Animations["dash"]);
                    currentState = PlayerState.Dashing;
                }
            }
        }

        /// <summary>
        /// Draws the game's graphics, called once per frame.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values used for rendering.
        /// </param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            sb.Begin();
            ap.Draw(sb, playerTexture, new Vector2(150, 300));
            // ap2.Draw(sb, playerTexture, new Vector2(400, 300));
            // ap3.Draw(sb, playerTexture, new Vector2(650, 300));
            sb.End();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            sb = new SpriteBatch(GraphicsDevice);
            ap = new K2D.AnimationPlayer();
            // ap2 = new AnimationPlayer();
            // ap3 = new AnimationPlayer();

            playerTexture = Content.Load<Texture2D>("Player/zero-fixed-rows");

            var rawspritemap = AnimationLoaderHelper.GetSpritesFromJson("spriteMap.json");
            sheet = AnimationLoaderHelper.TranslateIntoDomainModel(rawspritemap, playerTexture, "Zero");

            ap.Play(sheet.Animations["idle"]);
            // ap2.Play(sheet.Animations["running"]);
            // ap3.Play(sheet.Animations["jumping"]);
        }

        private SpriteBatch sb = null;
        private Texture2D playerTexture = null;
        private K2D.AnimationPlayer ap = null;
        //private AnimationPlayer ap2 = null;
        //private AnimationPlayer ap3 = null;
        private K2D.Spritesheet sheet = null;
        private PlayerState currentState = PlayerState.Idle;


    }
}