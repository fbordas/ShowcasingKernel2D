using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.Kernel2D.Animation.Import
{
    /// <summary>
    /// Translates a <see cref="SpritesheetDTO"/> into a domain model
    /// <see cref="Spritesheet"/>. This class is responsible for converting
    /// the data transfer object (DTO) representation of a spritesheet
    /// into the domain model used within the application.
    /// </summary>
    internal static class SpriteMapTranslator
    {
        /// <summary>
        /// Converts a <see cref="SpritesheetDTO"/> into a domain model
        /// <see cref="Spritesheet"/>. This method takes a DTO that contains
        /// the name of the spritesheet, its associated animations, and their
        /// frames, and converts it into a domain model object that can be used
        /// within the application. The method also requires a <see cref="Texture2D"/>
        /// representing the texture associated with the spritesheet, which is
        /// used to populate the `Texture` property of the resulting `Spritesheet` object.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="SpritesheetDTO"/> to convert.
        /// </param>
        /// <param name="texture">
        /// The <see cref="Texture2D"/> that represents the texture
        /// associated with the spritesheet.
        /// </param>
        /// <param name="_cont">
        /// The <see cref="ContentManager"/> used to load the texture from the specified path.
        /// </param>
        /// <param name="_path">
        /// The path to the texture file that contains the spritesheet image
        /// inside the MGCB content file. This path is used to load the texture
        /// from the content pipeline, allowing the application to access the
        /// spritesheet's image data.
        /// </param>
        /// <returns>
        /// A <see cref="Spritesheet"/> object that represents the
        /// converted data from the DTO. This object contains
        /// the name of the spritesheet, its texture,
        /// and a collection of animations, each with its frames.
        /// </returns>
        internal static Spritesheet ConvertToDomainModel
            (SpritesheetDTO dto, ContentManager _cont, string _path)
        {
            Texture2D tex = _cont.Load<Texture2D>(_path);
            var sheet = new Spritesheet
            {
                Name = dto.Name,
                Texture = tex,
                Animations = []
            };
            foreach (var animDto in dto.Animations)
            {
                var anim = new SpriteAnimation
                {
                    Name = animDto.Name,
                    Loop = animDto.Loop
                };
                foreach (var frameDto in animDto.Frames)
                {
                    anim.Frames.Add(new AnimationFrame
                    {
                        Name = frameDto.Name,
                        SourceRectangle = frameDto.Frame,
                        Duration = frameDto.Duration
                    });
                }
                sheet.Animations[anim.Name] = anim;
            }
            return sheet;
        }
    }
}
