using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Core;
using TrustIssues.Entities;
using TrustIssues.Factories;
using TrustIssues.Input;
using TrustIssues.Observers;
using TrustIssues.Data;

namespace TrustIssues.States
{
    public class GameState : State, IGameObserver
    {
        private Texture2D _backgroundTexture;
        private Texture2D _endTexture;
        private Player _player;
        private InputHandler _inputHandler;
        private Camera _camera;
        private EnemyFactory _enemyFactory;
        private Level _currentLevel;
        private TileManager _tileManager;
        private int _currentLevelIndex;

        // Textures voor speler (cache)
        private Texture2D _idleTex, _runTex, _jumpTex, _fallTex;

        public GameState(Game1 game, ContentManager content, int startLevel = 0) : base(game, content)
        {
            _currentLevelIndex = startLevel;
        }

        public override void LoadContent()
        {
            //centrale content game tex laden
            _camera = new Camera();
            _inputHandler = new InputHandler();
            _enemyFactory = new EnemyFactory(content, game.GraphicsDevice);

            _idleTex = content.Load<Texture2D>("Idle (32x32)");
            _runTex = content.Load<Texture2D>("Run (32x32)");
            _jumpTex = content.Load<Texture2D>("Jump (32x32)");
            _fallTex = content.Load<Texture2D>("Fall (32x32)");

            Texture2D terrainTex = content.Load<Texture2D>("Terrain (16x16)");
            _tileManager = new TileManager(terrainTex, 16, 32);

            _backgroundTexture = content.Load<Texture2D>("Blue");
            _endTexture = content.Load<Texture2D>("End (Idle)");

            LoadLevel(_currentLevelIndex);
        }

        private void LoadLevel(int index)
        {
            if (index >= LevelMaps.AllLevels.Count)
            {
                game.ChangeState(new VictoryState(game, content));
                return;
            }

            string[] mapLayout = LevelMaps.AllLevels[index];
            _currentLevel = new Level();

            for (int y = 0; y < mapLayout.Length; y++)
            {
                string line = mapLayout[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char tileType = line[x];
                    Vector2 pos = new Vector2(x * 32, y * 32);

                    switch (tileType)
                    {
                        case '#': _currentLevel.Tiles.Add(new Tile(pos, true, false, new Point(x, y))); break;
                        case '-': _currentLevel.Tiles.Add(new Tile(pos, false, true, new Point(x, y))); break;
                        case 'S':
                            if (_player == null)
                            {
                                _player = new Player(_idleTex, _runTex, _jumpTex, _fallTex, pos);
                                _player.AddObserver(this);
                            }
                            else
                            {
                                _player.Position = pos;
                            }
                            break;
                        case 'I':
                            var invisTile = new Tile(pos, true, false, new Point(x, y));
                            invisTile.IsVisible = false;
                            _currentLevel.Tiles.Add(invisTile);
                            break;
                        case 'F':
                            _currentLevel.Tiles.Add(new Tile(pos, false, false, new Point(x, y)));
                            break;
                        case 'V': _currentLevel.Enemies.Add(_enemyFactory.CreateEnemy(EnemyType.Walker, pos)); break;
                        case 'B': _currentLevel.Enemies.Add(_enemyFactory.CreateEnemy(EnemyType.Chaser, pos)); break;
                        case 'T': _currentLevel.Enemies.Add(_enemyFactory.CreateEnemy(EnemyType.Trap, pos)); break;
                        case 'X': _currentLevel.ExitZone = new Rectangle((int)pos.X, (int)pos.Y, 32, 32); break;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            _inputHandler.Update(_player);
            _player.Update(gameTime, _currentLevel.Tiles);
            _camera.Follow(_player.Position);

            for (int i = _currentLevel.Enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _currentLevel.Enemies[i];
                enemy.Update(gameTime, _player, _currentLevel.Tiles);

                if (enemy.IsExpired)
                {
                    _currentLevel.Enemies.RemoveAt(i);
                    continue;
                }

                if (!enemy.IsDead && _player.Bounds.Intersects(enemy.Bounds))
                {
                    if (_player.Velocity.Y > 0 && _player.Bounds.Bottom < enemy.Bounds.Center.Y && enemy.IsStompable)
                    {
                        enemy.Die();
                        _player.Bounce();
                    }
                    else
                    {
                        _player.Die();
                    }
                }
            }

            if (_player.Bounds.Intersects(_currentLevel.ExitZone))
            {
                _currentLevelIndex++;
                LoadLevel(_currentLevelIndex);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            for (int x = 0; x < game.GraphicsDevice.Viewport.Width; x += 64)
                for (int y = 0; y < game.GraphicsDevice.Viewport.Height; y += 64)
                    spriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);

            if (_currentLevel != null)
            {
                _tileManager.DrawTiles(spriteBatch, _currentLevel.Tiles);

                Rectangle exitRect = _currentLevel.ExitZone;
                spriteBatch.Draw(_endTexture, new Vector2(exitRect.X - 16, exitRect.Y - 32), new Rectangle(0, 0, 64, 64), Color.White);

                foreach (var enemy in _currentLevel.Enemies) enemy.Draw(spriteBatch);
                _player.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        // observer 
        public void OnNotify(string eventName)
        {
            if (eventName == "PlayerDied")
                game.ChangeState(new GameOverState(game, content, _currentLevelIndex));
        }
    }
}