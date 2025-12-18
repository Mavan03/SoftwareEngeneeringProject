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
using TrustIssues.Observers;
using TrustIssues.Data;

namespace TrustIssues.States
{
    public class GameState : State, IGameObserver
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

        //leves manager
        private int CurrentLevelIndex = 0;
        private Level CurrentLevel;
        private Texture2D TileTexture;
        private Texture2D ExitTexture;
        public override void LoadContent()
        {
            camera = new Camera();
            InputHandler = new InputHandler();
            enemyFactory = new EnemyFactory(content, game.GraphicsDevice);

            //texture grond
            TileTexture = new Texture2D(game.GraphicsDevice, 40, 40);
            Color[] data = new Color[40 * 40];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            for (int i = 0; i < data.Length; i++)
            {
                TileTexture.SetData(data);
            }
            //texture exit
            ExitTexture = new Texture2D(game.GraphicsDevice, 40, 40);
            Color[] exitData = new Color[40 * 40];
            for (int i = 0; i < exitData.Length; ++i) exitData[i] = Color.Gold;
            ExitTexture.SetData(exitData);
            //level laden
            LoadLevel(CurrentLevelIndex);

        }
        private void LoadLevel(int index)
        {
            if(index >= LevelMaps.AllLevels.Count)
            {
                game.ChangeState(new VictoryState(game, content));
                return;
            }
            string[] mapLayout = LevelMaps.AllLevels[index];
            CurrentLevel = new Level();
            for (int y = 0; y < mapLayout.Length; y++)
            {
                string line = mapLayout[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char tileType = line[x];
                    Vector2 pos = new Vector2(x * 40, y * 40);
                    switch (tileType)
                    {
                        case '#': // Muur
                            CurrentLevel.Tiles.Add(new Tile(TileTexture, pos, true));
                            break;
                        case 'S': // Start speler
                            if (player == null)
                            {
                                // Hackje: even een witte texture maken
                                Texture2D pTex = new Texture2D(game.GraphicsDevice, 40, 40);
                                Color[] pData = new Color[40 * 40];
                                for (int i = 0; i < pData.Length; i++) pData[i] = Color.White;
                                pTex.SetData(pData);

                                player = new Player(pTex, pos);
                                player.AddObserver(this);
                            }
                            else
                            {
                                player.Position = pos;
                            }
                            break;
                        case 'V': // Vijand
                            Enemy enemy = enemyFactory.CreateEnemy(EnemyType.Walker, pos);
                            CurrentLevel.Enemies.Add(enemy);
                            break;
                        case 'X': // Exit
                            CurrentLevel.ExitZone = new Rectangle((int)pos.X, (int)pos.Y, 40, 40);
                            break;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.Update(player);
            player.Update(gameTime, CurrentLevel.Tiles);
            camera.Follow(player.Position);

            foreach (var enemy in CurrentLevel.Enemies)
            {
                enemy.Update(gameTime, player, CurrentLevel.Tiles);

                if (enemy.Bounds.Intersects(player.Bounds))
                {
                    player.Die();
                }
            }
            if (player.Bounds.Intersects(CurrentLevel.ExitZone))
            {
                CurrentLevelIndex++;
                LoadLevel(CurrentLevelIndex);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin(transformMatrix: camera.Transform);

            //blokken
            foreach (var tile in CurrentLevel.Tiles)
            {
                tile.Draw(spriteBatch);
            }

            spriteBatch.Draw(ExitTexture, CurrentLevel.ExitZone, Color.Gold);

            foreach (var enemy in CurrentLevel.Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            
            // De player tekenen
            player.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void OnNotify(string eventName)
        {
            if(eventName == "PlayerDied")
            {
                game.ChangeState(new GameOverState(game, content));
            }
        }
    }
}
