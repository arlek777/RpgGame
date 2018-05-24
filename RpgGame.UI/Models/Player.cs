namespace RpgGame.UI.Models
{
    public class Player : Creature
    {
        public Weapon CurrentWeapon { get; set; }
        public Guard CurrentGuard { get; set; }
    }
}