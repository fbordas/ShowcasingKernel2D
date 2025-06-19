using Microsoft.Xna.Framework.Input;

namespace MonoGame.Kernel2D
{
    public abstract class InputBridge
    {
        protected KeyboardState _kb;
        protected GamePadState _gp;

        protected KeyboardState _previousKb;
        protected GamePadState _previousGp;

        public abstract void Update();
    }
}