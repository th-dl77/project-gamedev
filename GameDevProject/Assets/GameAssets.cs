using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDevProject.Assets
{
    public class GameAssets
    {
        private Dictionary<string, IAsset> assets;

        public GameAssets(GraphicsDevice graphicsDevice)
        {
            assets = new Dictionary<string, IAsset>();

            Texture2D debugTexture = new Texture2D(graphicsDevice, 1, 1);
            TextureAsset debugTextureAsset = new TextureAsset(debugTexture);
            debugTexture.SetData(new[] { Color.Green });

            AddAsset("debug", debugTextureAsset);
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
            AddAsset("skeleton", new TextureAsset(content.Load<Texture2D>("Skeleton enemy")));
            AddAsset("golemRun", new TextureAsset(content.Load<Texture2D>("Golem_Run")));
            AddAsset("golemAttack", new TextureAsset(content.Load<Texture2D>("Golem_AttackA")));
            AddAsset("golemDeath", new TextureAsset(content.Load<Texture2D>("Golem_DeathB")));
            AddAsset("slimeDeath", new TextureAsset(content.Load<Texture2D>("Slime_Spiked_Death")));
            AddAsset("slimeRun", new TextureAsset(content.Load<Texture2D>("Slime_Spiked_Run")));
            AddAsset("slimeFight", new TextureAsset(content.Load<Texture2D>("Slime_Spiked_Ability")));
            AddAsset("batFight", new TextureAsset(content.Load<Texture2D>("Bat_Attack")));
            AddAsset("batFly", new TextureAsset(content.Load<Texture2D>("Bat_Fly")));
            AddAsset("batDeath", new TextureAsset(content.Load<Texture2D>("Bat_Death")));
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
