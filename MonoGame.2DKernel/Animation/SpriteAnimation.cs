namespace MonoGame.Kernel2D.Animation
{
    public class SpriteAnimation
    {
        public string Name { get; set; } = string.Empty;
        public List<AnimationFrame> Frames = [];
        public bool Loop { get; set; }
        public HashSet<string> Tags { get; set; } = [];
    }
}
