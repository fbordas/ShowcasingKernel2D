namespace MonoGame.Kernel2D.Animation
{
    public class SpriteAnimation
    {
        public string Name { get; set; } = string.Empty;
        public List<AnimationFrame> Frames = [];
        public bool Loop { get; set; }
        public BasicAnimationTypes Tags { get; set; }
        public List<string> ExtraTypes { get; set; } = [];
    }
}
