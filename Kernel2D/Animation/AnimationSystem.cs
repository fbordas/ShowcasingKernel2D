using Kernel2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Animation
{
    /// <summary>
    /// Manages a collection of animation players, allowing for registration,
    /// removal, playback, and drawing of animations.
    /// </summary>
    public class AnimationSystem
    {
        private readonly Dictionary<string, AnimationPlayer> _players = [];

        /// <summary>
        /// Registers a new animation player with the specified key and animation.
        /// </summary>
        /// <param name="key">The key, or name, of the entity the animation player
        /// will be working for.</param>
        /// <param name="animation">The animation to play immediately upon
        /// registration.</param>
        /// <returns>
        /// 
        /// </returns>
        public bool Register(string key, SpriteAnimation animation)
        {
            if (_players.ContainsKey(key)) return false;

            var player = new AnimationPlayer();
            _players.Add(key, player);
            player.Play(animation);
            return true;
        }

        /// <summary>
        /// Plays the specified animation for the player associated with the given key.
        /// </summary>
        /// <param name="key">The key, or name, of the entity the animation player
        /// needs to look for.</param>
        /// <param name="animation">The animation to play.</param>
        /// <param name="onComplete">
        /// A callback to execute after playback is completed. Can be null if nothing
        /// is desired to run post-completion.
        /// </param>
        public void Play(string key, SpriteAnimation animation, Action? onComplete = null)
        {
            if (_players.TryGetValue(key, out var player))
            { player.Play(animation, onComplete); }
        }

        /// <summary>
        /// Updates all registered animation players based on the game time.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values used
        /// for game updates.</param>
        public void Update(GameTime gameTime)
        {
            foreach (var player in _players.Values)
            { player.Update(gameTime); }
        }

        /// <summary>
        /// Draws all registered animation players using the specified sprite batch and texture.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DrawContext"/> to enqueue the drawing onscreen to.
        /// </param>
        /// <param name="tex">The <see cref="Texture2D"/> to get the elements to be drawn.</param>
        /// <param name="positions">A table of keys of entities to draw, and the positions
        /// where each will be drawn onscreen.</param>
        public void Draw(DrawContext context, Texture2D tex, Dictionary<string, Vector2> positions)
        {
            foreach (var (key, player) in _players)
            {
                if (positions.TryGetValue(key, out var pos))
                    player.Draw(context, tex, pos);
            }
        }

        /// <summary>
        /// Draws a specific animation player associated with the given key at the specified position.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DrawContext"/> to enqueue the drawing onscreen to.
        /// </param>
        /// <param name="tex">The <see cref="Texture2D"/> to get the elements to be drawn.</param>
        /// <param name="position">The <see cref="Vector2"/> coordinates of where to draw
        /// the entity.</param>
        /// <param name="key">The key, or name, of the <see cref="AnimationPlayer"/> to use
        /// to draw the specified texture.</param>
        /// <param name="facingRight">Optional: True if the entity is facing right, False
        /// otherwise.</param>
        public void Draw(DrawContext context, Texture2D tex, Vector2 position,
            string key, bool facingRight = true)
        {
            if (_players.TryGetValue(key, out var player))
            {
                player.Draw(context, tex, position,
                    facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            }
        }

        /// <summary>
        /// Unregisters an animation player associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the <see cref="AnimationPlayer"/>
        /// to remove.</param>
        /// <returns>True if removal is successful; false otherwise.</returns>
        public bool Unregister(string key)
        {
            if (_players.ContainsKey(key))
            {
                _players.Remove(key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves the animation player associated with the specified key.
        /// </summary>
        /// <param name="key">The key, or name, of the entity to get the
        /// <see cref="AnimationPlayer"/> for.</param>
        /// <returns>The <see cref="AnimationPlayer"/> associated with
        /// the provided key if it exists; null if the key doesn't exist.</returns>
        public AnimationPlayer? GetPlayer(string key)
            => _players.TryGetValue(key, out var player) ? player : null;
    }
}