using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Animation.Import;
using MonoGame.Kernel2D.Animation;
using System.Reflection;

namespace MonoGame.Kernel2D.Helpers
{
    /// <summary>
    /// Helper class for loading entity spritemaps from embedded JSON resources.
    /// </summary>
    public static class EntityLoaderHelper
    {
        /// <summary>
        /// Loads a <see cref="Spritesheet"/> from an embedded JSON spritemap resource.
        /// </summary>
        /// <param name="resourcePath">
        /// The path to the embedded JSON resource, relative to the assembly.
        /// </param>
        /// <param name="texture">
        /// The <see cref="Texture2D"/> that contains the sprite images for the spritesheet.
        /// </param>
        /// <param name="assembly">
        /// The assembly containing the embedded resource. If null, the calling assembly is used.
        /// </param>
        /// <returns>
        /// A <see cref="Spritesheet"/> object containing the animations and texture.
        /// </returns>
        /// <exception cref="InvalidDataException">
        /// Thrown if the embedded JSON resource is invalid or malformed.
        /// </exception>
        public static Spritesheet LoadFromEmbeddedJson
            (string resourcePath, Texture2D texture, Assembly? assembly = null)
        {
            var dto = EmbeddedJsonLoader.LoadFromResource<SpritesheetDTO>
                (resourcePath, assembly);
            return dto == null
                ? throw new InvalidDataException("Invalid or malformed animation resource.")
                : SpriteMapTranslator.ConvertToDomainModel(dto, texture);
        }
    }
}