using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using TrustIssues.Core;
using TrustIssues.Entities;
using TrustIssues.Input;

namespace TrustIssues.States
{
    public class GameState : State
    {
        private Player player;
        private InputHandler InputHandler;
        private Camera camera;

        public GameState(Game1 game, ContentManager content) : base(game, content)
        {

        }
        public override void LoadContent()
        {
            Texture2D playerTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            playerTexture.SetData(new[] { Color.White });

            Texture2D blockTExture = new Texture2D(game.GraphicsDevice, 40, 40);
            Color[] data = new Color[40 * 40];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            blockTExture.SetData(data);
            player = new Player(blockTExture, new Vector2(100, 100));
            InputHandler = new InputHandler();
            camera = new Camera();
        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.Update(player);
            player.Update(gameTime);
            camera.Follow(player.Position);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin(transformMatrix: camera.Transform);
            // De player tekenen
            player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
