using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using TrustIssues.Core;
using TrustIssues.Entities;
using TrustIssues.Input;
using System.Collections.Generic;
using TrustIssues.Factories;
using System.Linq;

namespace TrustIssues.States
{
    public class GameState : State
    {
        private Player player;
        private InputHandler InputHandler;
        private Camera camera;

        private List<Tile> tiles;
        public GameState(Game1 game, ContentManager content) : base(game, content)
        {

        }

        private List<Enemy> enemies;
        private EnemyFactory enemyFactory;
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

            Texture2D tileTexture = new Texture2D(game.GraphicsDevice, 40, 40);
            Color[] blaockData = new Color[40 * 40];
            for (int i = 0; i < blaockData.Length; ++i) blaockData[i] = Color.Black;
            tileTexture.SetData(data);

            //de vloer
            tiles = new List<Tile>();
            // Maak een rij blokjes van X=0 tot X=800 op hoogte Y=300
            for (int x = 0; x < 20; x++)
            {
                tiles.Add(new Tile(tileTexture, new Vector2(x * 40, 300), true));
            }
            tiles.Add(new Tile(tileTexture, new Vector2(0, 260), true));
            tiles.Add(new Tile(tileTexture, new Vector2(800, 260), true));
            // Voeg een 'zwevend' platform toe om op te springen
            tiles.Add(new Tile(tileTexture, new Vector2(200, 200), true));
            tiles.Add(new Tile(tileTexture, new Vector2(240, 200), true));

            enemyFactory = new EnemyFactory(content, game.GraphicsDevice);
            enemies = new List<Enemy>();
            enemies.Add(enemyFactory.CreateEnemy(EnemyType.Walker, new Vector2(300, 260)));
            enemies.Add(enemyFactory.CreateEnemy(EnemyType.Walker, new Vector2(500, 260)));

        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.Update(player);
            player.Update(gameTime, tiles);
            camera.Follow(player.Position);

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, player, tiles);

                if (enemy.Bounds.Intersects(player.Bounds))
                {
                    player.Position = new Vector2(100, 100);
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin(transformMatrix: camera.Transform);

            //blokken
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            
            // De player tekenen
            player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
