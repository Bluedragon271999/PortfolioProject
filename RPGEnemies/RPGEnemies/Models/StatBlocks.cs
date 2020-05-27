using System;
using System.Collections.Generic;

namespace RPGEnemies.Models
{
    public partial class StatBlocks
    {
        public string CharacterName { get; set; }
        public int? Brawn { get; set; }
        public int? Agility { get; set; }
        public int? Intellect { get; set; }
        public int? Cunning { get; set; }
        public int? Willpower { get; set; }
        public int? Presence { get; set; }
        public string Description { get; set; }
        public string Talents { get; set; }
        public string Abilities { get; set; }
        public string Weapons { get; set; }
        public string Skills { get; set; }
    }
}
