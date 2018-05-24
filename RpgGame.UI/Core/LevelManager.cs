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
        public static List<Monster> FillMonsters(int spriteSize, int tileSize, GridGraph graph)
        {
            var random = new Random();
            var tileCount = spriteSize / tileSize;
            var monstersCount = random.Next(3, 5);
            var monsters = new List<Monster>();
            for (int i = 0; i < monstersCount; i++)
            {
                var li = GetLocationIndex(graph);
                monsters.Add(new Monster()
                {
                    Name = "Monster",
                    TileId = new Random().Next(0, tileCount) * tileSize,
                    Health = 10,
                    Strength = 5,
                    LocationIndex = li
                });
            }

            return monsters;
        }

        private static int GetLocationIndex(GridGraph graph)
        {
            var random = new Random();
            int locationIndex;
            while (true)
            {
                locationIndex = random.Next(0, graph.Nodes.Length - 1);
                var neighborsIndexes = graph.GetNeighborsIndexes(locationIndex);
                var can = true;
                foreach (var neighborsIndex in neighborsIndexes)
                {
                    if (graph.Nodes[neighborsIndex] != -1)
                    {
                        can = false;
                        break;
                    }
                }

                var value = graph.Nodes[locationIndex];
                if (can || value == -1) break;
            }

            return locationIndex;
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
