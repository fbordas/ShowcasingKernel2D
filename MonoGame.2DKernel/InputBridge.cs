using Microsoft.Xna.Framework.Input;

namespace MonoGame.Kernel2D
{
    public abstract class InputBridge
    {
        protected KeyboardState _kb;
        protected GamePadState _gp;

        public abstract void Update();
    }
}