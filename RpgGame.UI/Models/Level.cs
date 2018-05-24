using System.Collections.Generic;

namespace RpgGame.UI.Models
{
    public class Level
    {
        public int Number { get; set; }
        public List<Monster> Monsters { get; set; }
        public Monster BigBoss { get; set; }
        public List<Item> Items { get; set; }
    }
}