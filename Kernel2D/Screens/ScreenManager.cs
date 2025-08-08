using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Kernel2D.Drawing;
using Kernel2D.Screens.ScreenTransitions;
using Debugger = Kernel2D.Helpers.DebugHelpers;

namespace Kernel2D.Screens
{
    /// <summary>
    /// Manages the screens in a 2D game, allowing for screen transitions and updates.
    /// </summary>
    public class ScreenManager
    {
        private readonly Dictionary<string, ScreenBase> _screens = [];
        private readonly Dictionary<string, ScreenTransitionBase> 
            _transitions = [];
        private ScreenBase? _currentScreen;
        private ScreenBase? _pendingScreen;
        private ContentManager? _content;
        private static ScreenManager? _instance = null;
        private ScreenTransitionBase? _currentTransition;
        private ScreenTransitionBase? _outTransition;
        private ScreenTransitionBase? _inTransition;
        private readonly Stack<ScreenBase> _screenStack = new();

        /// <summary>
        /// Gets the current screen being managed by the ScreenManager.
        /// </summary>
        public ScreenBase? CurrentScreen => _currentScreen;

        /// <summary>
        /// Registers a screen with the ScreenManager using its name as the key.
        /// </summary>
        /// <param name="name">
        /// The unique name for the screen. This should be a string that
        /// identifies the screen uniquely within the manager.
        /// </param>
        /// <param name="screen">
        /// The instance of the <see cref="ScreenBase"/> to register.
        /// </param>
        public void RegisterScreen(string name, ScreenBase screen)
            => _screens[name] = screen;

        /// <summary>
        /// Gets the screen with the specified name.
        /// </summary>
        /// <param name="name">The name of the screen to get.</param>
        /// <returns>The screen with the specified name.</returns>
        public ScreenBase GetScreen(string name) => _screens[name];

        /// <summary>
        /// Changes the current screen being rendered by the ScreenManager.
        /// </summary>
        /// <param name="screenName">
        /// The name of the screen to switch to. This should match
        /// the name used when registering the screen.
        /// </param>
        /// <param name="content">
        /// The <see cref="ContentManager"/> used to load the content for the new screen.
        /// </param>
        /// <param name="transitions">
        /// Optional parameter that specifies the transitions to use when changing screens.
        /// If null, no transitions will be applied.
        /// </param>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified screen name does not exist in the ScreenManager.
        /// </exception>
        public void ChangeScreen(string screenName, ContentManager content,
            ScreenTransitionPair? transitions = null)
        {
            if (!_screens.TryGetValue(screenName, out var nextScreen))
                throw new KeyNotFoundException($"Screen '{screenName}' not found.");

            _pendingScreen = nextScreen;
            _content = content;
            _outTransition = transitions?.TransitionPreviousOut;
            _inTransition = transitions?.TransitionCurrentIn;

            if (_currentScreen != null && _outTransition != null)
            {
                _outTransition.Reset();
                _currentTransition = _outTransition;
            }
            else
            {
                // Nothing to fade out from? Just load immediately and begin fade-in
                CommitScreenChange();
            }
        }

        /// <summary>
        /// Commits the pending screen change by unloading the current screen,
        /// loading the new screen, and applying any transition effects.
        /// </summary>
        private void CommitScreenChange()
        {
            _currentScreen?.UnloadContent();
            _currentScreen = _pendingScreen;
            _pendingScreen = null;

            _screenStack.Clear();
            if (_currentScreen != null)
            { 
                _screenStack.Push(_currentScreen!);
                _currentScreen!.LoadContent(_content!);
            }

            
            if (_inTransition != null)
            {
                _inTransition.Reset();
                _currentTransition = _inTransition;
            }
            else
            {
                _currentTransition = null;
            }
        }

        /// <summary>
        /// Pushes a new screen onto the screen stack, loading its content
        /// and applying any specified transitions over whatever existing
        /// screen is currently active.
        /// </summary>
        /// <param name="newScreen">
        /// The new screen to push onto the stack. This should be an instance
        /// of <see cref="ScreenBase"/> that has been registered with the
        /// ScreenManager.
        /// </param>
        /// <param name="content">
        /// The <see cref="ContentManager"/> used to load the content for the
        /// new screen.
        /// </param>
        public void PushScreen(ScreenBase newScreen, ContentManager content,
            ScreenTransitionPair? transitions = null)
        {
            _content = content;
            _inTransition = transitions?.TransitionCurrentIn;

            _screenStack.Push(newScreen);
            newScreen.LoadContent(_content!);

            _currentScreen = newScreen;

            if (_inTransition != null)
            {
                _inTransition.Reset();
                _currentTransition = _inTransition;
            }
        }

        /// <summary>
        /// Removes the top screen from the screen stack, unloading its content
        /// and applying any specified transitions to the previous screen.
        /// </summary>
        public void PopScreen(ScreenTransitionPair? transitions = null)
        {
            if (_screenStack.Count <= 1) return; // no screen to pop

            var oldScreen = _screenStack.Pop();
            oldScreen.UnloadContent();

            _currentScreen = _screenStack.Peek();

            _inTransition = transitions?.TransitionPreviousOut;
            if (_inTransition != null)
            {
                _inTransition.Reset();
                _currentTransition = _inTransition;
            }
        }

        /// <summary>
        /// Updates the current screen with the given <see cref="GameTime"/>.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values used for game updates.
        /// </param>
        public void Update(GameTime gameTime)
        {
            if (_currentTransition != null)
            {
                _currentTransition.Update(gameTime);
                if (_currentTransition.IsFinished)
                {
                    if (_currentTransition == _outTransition)
                    {
                        CommitScreenChange();
                    }
                    else if (_currentTransition == _inTransition)
                    {
                        _inTransition = null;
                        _outTransition = null;
                        _currentTransition = null;
                    }
                }
            }
            else if (_screenStack.Count > 0)
            {
                _screenStack.Peek().Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the current screen using the provided <see cref="DrawContext"/>.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DrawContext"/> that contains the drawing parameters
        /// such as the sprite batch, transform matrix, graphics device, game
        /// time, and font.
        /// </param>
        public void Draw(DrawContext context)
        {
            if (_screenStack.Count == 0)
            {
                Debugger.WriteLine
                    ("ScreenManager.Draw() | No current screen -- checking for transitions...");
                if (_currentTransition != null)
                {
                    Debugger.WriteLine
                        ($"ScreenManager.Draw() | Drawing transition: {_currentTransition.GetType().Name}");
                }
                return;
            }

            // Draw all screens in the stack, bottom to top
            foreach (var screen in _screenStack.Reverse())
            {
                screen.Draw(context);
            }

            // Draw the transition on top if there is one
            _currentTransition?.Draw(context);
        }


        /// <summary>
        /// Gets the singleton instance of the ScreenManager.
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                _instance ??= new ScreenManager();
                return _instance;
            }
        }
    }

    /// <summary>
    /// Represents a pair of screen transitions for managing in and out transitions
    /// when changing screens in a 2D game.
    /// </summary>
    /// <param name="TransitionPreviousOut">
    /// The transition to apply when exiting the current screen.
    /// </param>
    /// <param name="TransitionCurrentIn">
    /// The transition to apply when entering the new screen.
    /// </param>
    public record ScreenTransitionPair
        (ScreenTransitionBase? TransitionPreviousOut, ScreenTransitionBase? TransitionCurrentIn);
}