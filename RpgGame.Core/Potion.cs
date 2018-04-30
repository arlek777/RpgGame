namespace RpgGame.Core
{
    public enum PotionType
    {
        Health = 0,
        Strength
    }

    public class Potion : Item
    {
        public PotionType Type { get; set; }
    }
}