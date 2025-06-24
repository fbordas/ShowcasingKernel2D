using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Kernel2D.Input
{
    public abstract class InputBridge
    {
        private KeyboardState _keyboard;
        private GamePadState _gamepad;

        public void Update()
        {
            _keyboard = Keyboard.GetState();
            _gamepad = GamePad.GetState(PlayerIndex.One);
        }

        public bool IsGamepadConnected() => _gamepad.IsConnected;
    }

}
