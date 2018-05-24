using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RpgGame.UI.Core
{
    public class MapLoader
    {
        private XElement _levelMapElement;

        public void Load(string levelName)
        {
            var levelFullPath = new Uri($"{UiConstants.UriPackPrefix}/{UiConstants.MapLevelsFolder}/{levelName}.oel").AbsolutePath;
            var document = XDocument.Load(File.OpenRead("../../" + levelFullPath));
            if (document.Root == null) throw new NullReferenceException("Level is not found.");

            _levelMapElement = document.Root;
        }

        public int[] GetMapLayer(string name)
        {
            if(_levelMapElement == null) throw new NullReferenceException("Level map is not loaded.");

            int[] layer = _levelMapElement.Elements().FirstOrDefault(el => el.Name == name)?
                .Value.Split(',', '\n')
                .Select(int.Parse)
                .ToArray();

            return layer;
        }
    }
}