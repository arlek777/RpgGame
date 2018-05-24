namespace RpgGame.UI.Models
{
    public class Creature
    {
        public int TileId { get; set; }
        public int LocationIndex { get; set; }
        public string Name { get; set; }
        public double Health { get; set; }
        public double Strength { get; set; }
        public uint Level { get; set; }
        public Inventory Inventory { get; set; }
    }
}
