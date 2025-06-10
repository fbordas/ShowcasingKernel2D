using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame.Kernel2D.Animation
{
    public class AnimationSystem
    {
        private readonly Dictionary<string, AnimationPlayer> _players = [];

        public void Register(string key, SpriteAnimation animation)
        {
            var player = new AnimationPlayer();
            player.Play(animation);
            _players[key] = player;
        }

        public void Play(string key, SpriteAnimation animation)
        {
            if (_players.TryGetValue(key, out var player))
                player.Play(animation);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var player in _players.Values)
                player.Update(gameTime);
        }

        public void Draw(SpriteBatch batch, Texture2D tex, Dictionary<string, Vector2> positions)
        {
            foreach (var (key, player) in _players)
            {
                if (positions.TryGetValue(key, out var pos))
                    player.Draw(batch, tex, pos);
            }
        }

        public AnimationPlayer? GetPlayer(string key)
            => _players.TryGetValue(key, out var player) ? player : null;
    }
}
