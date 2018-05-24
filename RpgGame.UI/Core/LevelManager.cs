using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using RpgGame.UI.Models;

namespace RpgGame.UI.Core
{
    public static class Helper
    {
        public static Uri GetTileUri(string tile)
        {
            return new Uri($"{UiConstants.UriPackPrefix}/${UiConstants.TilesFolder}/${tile}");
        }
    }

    public class LevelManager
    {
        public List<Monster> FillMonsters(int spriteSize, int tileSize, GridGraph graph)
        {
            var random = new Random();
            var tileCount = spriteSize / tileSize;
            var monstersCount = random.Next(3, 5);
            var monsters = new List<Monster>();
            for (int i = 0; i < monstersCount; i++)
            {
                monsters.Add(new Monster()
                {
                    Name = "Monster",
                    TileId = new Random().Next(0, tileCount) * tileSize,
                    Health = 10,
                    Strength = 5,
                    LocationIndex = 
                });
            }
        }

        public void GenerateGuard()
        {

        }

        public void GenerateWeapon()
        {

        }

        public void GeneratePotion()
        {

        }
    }
}
