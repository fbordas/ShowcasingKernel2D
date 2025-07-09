using MonoGame.Kernel2D.Helpers;
using MonoGame.Kernel2D.Animation;
using Microsoft.Xna.Framework.Content;

namespace PlatformingProject.Core.Helpers
{
    internal class SpritesheetLoader
    {
        internal static Spritesheet GetSpritesheetFromResources(string logicalPath, ContentManager content)
        {
            return EntitySpritesheetLoader.LoadEntitySpritesheet<SpritesheetLoader>(logicalPath, content);
        }
    }
}