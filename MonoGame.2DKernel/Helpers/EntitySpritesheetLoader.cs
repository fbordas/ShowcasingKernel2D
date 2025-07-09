using System.Reflection;
using Microsoft.Xna.Framework.Content;
using MonoGame.Kernel2D.Animation;
using MonoGame.Kernel2D.Animation.Import;

namespace MonoGame.Kernel2D.Helpers
{
    /// <summary>
    /// Helper class for loading entity spritemaps from embedded JSON resources.
    /// </summary>
    public static class EntitySpritesheetLoader
    {
        /// <summary>
        /// Loads a <see cref="Spritesheet"/> from an embedded JSON spritemap resource.
        /// </summary>
        /// <typeparam name="TAnchor">
        /// This generic type MUST be from the assembly where the resource is embedded.
        /// This is typically the calling game project, whether it's a standalone executable
        /// or a game library.
        /// </typeparam>
        /// <param name="logicalPath">
        /// The path to the embedded JSON resource. This path must always be relative to the
        /// assembly.
        /// </param>
        /// <param name="content">
        /// The <see cref="ContentManager"/> used to load the texture associated with the spritesheet.
        /// </param>
        /// <param name="_assembly">
        /// The assembly containing the embedded resource. If null, the calling assembly is used.
        /// </param>
        /// <returns>
        /// A <see cref="Spritesheet"/> object containing the animations and texture.
        /// </returns>
        /// <exception cref="InvalidDataException">
        /// Thrown if the embedded JSON resource is invalid or malformed.
        /// </exception>
        public static Spritesheet LoadEntitySpritesheet<TAnchor>
            (string logicalPath, ContentManager content)
        {
            var rootNs = typeof(TAnchor).Namespace!;
            string resourcePath = $"{rootNs}.Resources.{logicalPath}.json";
            var dto = EmbeddedJsonLoader.LoadFromResource<SpritesheetDTO, TAnchor>(resourcePath);
            return dto == null
                ? throw new InvalidDataException("Invalid or malformed animation resource.")
                : SpriteMapTranslator.ConvertToDomainModel(dto, content, logicalPath);
        }
    }
}