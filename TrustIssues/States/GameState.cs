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
        private Texture2D backgroundTexture;

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
        private TileManager TileManager;
        private Texture2D ExitTexture;
        private Texture2D idleTex;
        private Texture2D runTex;
        public override void LoadContent()
        {
            camera = new Camera();
            InputHandler = new InputHandler();
            enemyFactory = new EnemyFactory(content, game.GraphicsDevice);

            idleTex = content.Load<Texture2D>("Idle (32x32)"); 
            runTex = content.Load<Texture2D>("Run (32x32)");

            Texture2D terrainTex = content.Load<Texture2D>("Terrain (16x16)");
            TileManager = new TileManager(terrainTex, 16, 32);

            backgroundTexture = content.Load<Texture2D>("Blue"); // Of "Green"

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
                    Vector2 pos = new Vector2(x * 32, y * 32);
                    switch (tileType)
                    {
                        case '#': // Muur
                            CurrentLevel.Tiles.Add(new Tile(pos,true,false,new Point(x,y)));
                            break;
                        case '-': // Platform (Solid = false*, OneWay = true)
                            CurrentLevel.Tiles.Add(new Tile(pos, false, true, new Point(x, y)));
                            break;
                        case 'S': // Start speler
                            if (player == null)
                            {
                                // Hackje: even een witte texture maken
                                player = new Player(idleTex,runTex, pos);
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
                        case 'B': // Bat 
                            Enemy bat = enemyFactory.CreateEnemy(EnemyType.Chaser, pos);
                            CurrentLevel.Enemies.Add(bat);
                            break;
                        case 'T': // Trap
                            Enemy trap = enemyFactory.CreateEnemy(EnemyType.Trap, pos);
                            CurrentLevel.Enemies.Add(trap);
                            break;
                        case 'X': // Exit
                            CurrentLevel.ExitZone = new Rectangle((int)pos.X, (int)pos.Y, 32, 32);
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

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            int screenWidth = game.GraphicsDevice.Viewport.Width;
            int screenHeight = game.GraphicsDevice.Viewport.Height;

            for (int x = 0; x < screenWidth; x += 64)
            {
                for (int y = 0; y < screenHeight; y += 64)
                {
                    spriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);
                }
            }
            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: camera.Transform, samplerState: SamplerState.PointClamp);
            //blokken
            if (CurrentLevel != null)
            {
                TileManager.DrawTiles(spriteBatch, CurrentLevel.Tiles);
                // ... rest van je draw code
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
