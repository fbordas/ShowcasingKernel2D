using Debugger = Kernel2D.Helpers.DebugHelpers;

using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Drawing
{
    /// <summary>
    /// Represents a queue for drawing commands in a 2D game.
    /// </summary>
    public class DrawQueue
    {
        /// <summary>
        /// A list of draw commands that are queued for execution.
        /// </summary>
        private readonly List<IDrawCommand> _queue = [];

        /// <summary>
        /// Enqueues a draw command to be executed later.
        /// </summary>
        /// <param name="call">
        /// The draw command to be added to the queue.
        /// </param>
        public void Enqueue(IDrawCommand call) => _queue.Add(call);

        /// <summary>
        /// Flushes the draw queue by executing all queued drawing commands in order of
        /// their layer depth, from the highest layer (back) to the lowest layer (front).
        /// </summary>
        /// <param name="sb">
        /// The <see cref="SpriteBatch"/> used to execute the draw commands.
        /// </param>
        public void Flush(SpriteBatch sb)
        {
            foreach (var command in _queue.OrderByDescending(c => c.Layer))
            {
                switch (command)
                {
                    case SpriteDrawCommand sprite:
                        sb.Draw(sprite.Texture, sprite.Position, sprite.SourceRectangle,
                            sprite.Color, sprite.Rotation, sprite.Origin, sprite.Scale,
                            sprite.Effects, sprite.LayerDepth);
                        break;
                    case TextDrawCommand text:
                        sb.DrawString(text.SpriteFont, text.Text, text.Position,
                            text.Color, text.Rotation, text.Origin,
                            text.Scale, text.Effects, text.LayerDepth);
                        break;
                    default:
                        Debugger.WriteLine($"Unsupported draw command: {command.GetType().Name}");
                        throw new NotSupportedException(
                            $"Draw command type {command.GetType().Name} is not supported.");
                }
            }
            _queue.Clear();
        }

        /// <summary>
        /// Clears the draw queue, removing all queued drawing commands without executing them.
        /// </summary>
        public void ClearQueue() => _queue.Clear();
    }
}
