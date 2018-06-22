using System;

namespace PorierACTPlugin
{
    public class StatusEffect
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime LastEnded { get; set; }

        public StatusEffect(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}