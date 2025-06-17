using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using K2D = MonoGame.Kernel2D;

namespace EmptyProject.Core
{
    internal class PlatformerInputBridge : K2D.InputBridge
    {   
        public override void Update()
        {
            _kb = Keyboard.GetState();
            _gp = GamePad.GetState(PlayerIndex.One);
        }

        public bool JumpPressed()
            => _kb.IsKeyDown(Keys.Space) || (_gp.IsConnected && _gp.Buttons.A == ButtonState.Pressed);

        public bool MoveLeft()
            => _kb.IsKeyDown(Keys.Left) || _kb.IsKeyDown(Keys.A) || 
                _gp.IsButtonDown(Buttons.DPadLeft) || _gp.IsButtonDown(Buttons.LeftThumbstickLeft);

        public bool MoveRight()
            => _kb.IsKeyDown(Keys.Right) || _kb.IsKeyDown(Keys.D) || 
                _gp.IsButtonDown(Buttons.DPadRight) || _gp.IsButtonDown(Buttons.LeftThumbstickRight);

        public bool DashPressed()
            => _kb.IsKeyDown(Keys.LeftShift) ||
               (_gp.IsConnected && _gp.Buttons.RightShoulder == ButtonState.Pressed);

        public bool IsIdle() => _kb.GetPressedKeyCount() == 0 &&
                                _gp.Buttons.A == ButtonState.Released &&
                                _gp.Buttons.B == ButtonState.Released &&
                                _gp.Buttons.X == ButtonState.Released &&
                                _gp.Buttons.Y == ButtonState.Released &&
                                _gp.Buttons.LeftShoulder == ButtonState.Released &&
                                _gp.Buttons.RightShoulder == ButtonState.Released &&
                                _gp.DPad.Up == ButtonState.Released &&
                                _gp.DPad.Down == ButtonState.Released &&
                                _gp.DPad.Left == ButtonState.Released &&
                                _gp.DPad.Right == ButtonState.Released;
    }
}
