using Reinforced.Typings.Attributes;

namespace incognote.server
{
    [TsInterface]
    public class Action
    {
        public Action(string id, string description, bool enabled)
        {
            Id = id;
            Description = description;
            Enabled = enabled;
        }

        public string Id { get; }
        public string Description { get; }
        public bool Enabled { get; }
    }
}
