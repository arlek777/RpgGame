namespace RpgGame.UI.Models
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