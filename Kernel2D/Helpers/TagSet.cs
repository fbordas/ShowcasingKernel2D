namespace Kernel2D.Helpers
{
    public class TagSet
    {
        private const string NoneTag = nameof(BasicAnimationTypes.None);

        private readonly HashSet<string> _tags = [];

        public TagSet() { _tags.Add(NoneTag); }

        public void Add(BasicAnimationTypes tag)
        {
            if (tag == BasicAnimationTypes.None) return;
            _tags.Add(tag.ToString());
            _tags.Remove(NoneTag);
        }

        public void Add(string tag)
        {
            if (string.Equals(tag, NoneTag, StringComparison.OrdinalIgnoreCase)) return;
            _tags.Add(tag);
            _tags.Remove(NoneTag);
        }

        public void Remove(BasicAnimationTypes tag)
        { _tags.Remove(tag.ToString()); EnsureFallbackTag(); }

        public void Remove(string tag)
        { _tags.Remove(tag); EnsureFallbackTag(); }

        public bool Has(BasicAnimationTypes tag) => _tags.Contains(tag.ToString());

        public bool Has(string tag) => _tags.Contains(tag);

        public IReadOnlyCollection<string> Tags => _tags;

        public void Clear()
        { _tags.Clear(); _tags.Add(NoneTag); }

        public void AddFromEnumFlags(BasicAnimationTypes flags)
        {
            foreach (BasicAnimationTypes value in Enum.GetValues(typeof(BasicAnimationTypes)))
            {
                if (value != BasicAnimationTypes.None && flags.HasFlag(value))
                    Add(value);
            }
        }

        public BasicAnimationTypes ToEnumFlags()
        {
            BasicAnimationTypes result = BasicAnimationTypes.None;
            foreach (string tag in _tags)
            {
                if (Enum.TryParse(tag, out BasicAnimationTypes parsed))
                    result |= parsed;
            }
            return result;
        }

        private void EnsureFallbackTag()
        { if (_tags.Count == 0) { _tags.Add(NoneTag); } }

        public override string ToString() => string.Join(", ", _tags);
    }
}
