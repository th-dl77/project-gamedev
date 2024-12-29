using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Assets
{
    public class GameAssets
    {
        private Dictionary<string, IAsset> assets;

        public GameAssets()
        {
            assets = new Dictionary<string, IAsset>();
        }

        public void AddAsset(string key, IAsset asset)
        {
            assets[key] = asset;
        }
        public IAsset GetAsset(string key)
        {
            return assets.ContainsKey(key) ? assets[key] : null;
        }
    }
}
