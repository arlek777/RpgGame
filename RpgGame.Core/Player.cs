namespace RpgGame.Core
{
    public class Player : Creature
    {
        public Weapon CurrentWeapon { get; set; }
        public Guard CurrentGuard { get; set; }
    }
}