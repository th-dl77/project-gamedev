using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevProject.Assets
{
    public class GameAssets
    {
        private Dictionary<string, IAsset> assets;

        public GameAssets()
        {
            assets = new Dictionary<string, IAsset>();
        }

        public void LoadContent(ContentManager content)
        {
            AddAsset("player", new TextureAsset(content.Load<Texture2D>("char_red_1")));
            AddAsset("buttonTexture", new TextureAsset(content.Load<Texture2D>("buttonTemplate")));
            AddAsset("deathScreen", new TextureAsset(content.Load<Texture2D>("deathScreen")));
            AddAsset("heartFull", new TextureAsset(content.Load<Texture2D>("heartFull")));
            AddAsset("heartEmpty", new TextureAsset(content.Load<Texture2D>("heartEmpty")));
            AddAsset("mainMenuBackground", new TextureAsset(content.Load<Texture2D>("backgroundMenu")));
            AddAsset("font", new FontAsset(content.Load<SpriteFont>("Font1")));
        }
        public void AddAsset(string key, IAsset asset)
        {
            assets[key] = asset;
        }
        public IAsset GetAsset(string key)
        {
            return assets.ContainsKey(key) ? assets[key] : null;
        }
        public SpriteFont GetFont(string key)
        {
            var fontAsset = GetAsset(key) as FontAsset;
            return fontAsset?.Font;
        }
        public Texture2D GetTexture(string key)
        {
            var textureAsset = GetAsset(key) as TextureAsset;
            return textureAsset?.Texture;
        }
    }
}
