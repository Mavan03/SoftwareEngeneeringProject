using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Entities;

namespace TrustIssues.Core
{
    // tekenene van de tiles = srp
    public class TileManager
    {
        private Texture2D _tilesetTexture;
        private Dictionary<string, Rectangle> _sourceRects;
        private float _scale;

        public TileManager(Texture2D tilesetTexture, int originalTileSize, int targetSize)
        {
            _tilesetTexture = tilesetTexture;
            _scale = (float)targetSize / originalTileSize;
            _sourceRects = new Dictionary<string, Rectangle>();

            // Tile tex
            int startX = 96; int startY = 0;
            AddRect("TopLeft", startX, startY);
            AddRect("TopCenter", startX + 16, startY);
            AddRect("TopRight", startX + 32, startY);
            AddRect("MiddleLeft", startX, startY + 16);
            AddRect("MiddleCenter", startX + 16, startY + 16);
            AddRect("MiddleRight", startX + 32, startY + 16);
            AddRect("BottomLeft", startX, startY + 32);
            AddRect("BottomCenter", startX + 16, startY + 32);
            AddRect("BottomRight", startX + 32, startY + 32);
            _sourceRects.Add("Single", new Rectangle(startX + 48, startY, 16, 16));

            // Setup rectangles (Platforms)
            _sourceRects.Add("PlatformLeft", new Rectangle(272, 16, 16, 5));
            _sourceRects.Add("PlatformMid", new Rectangle(288, 16, 16, 5));
            _sourceRects.Add("PlatformRight", new Rectangle(304, 16, 16, 5));
        }

        private void AddRect(string name, int x, int y)
        {
            _sourceRects.Add(name, new Rectangle(x, y, 16, 16));
        }

        public void DrawTiles(SpriteBatch spriteBatch, List<Tile> tiles)
        {
            Dictionary<Point, Tile> tileMap = new Dictionary<Point, Tile>();
            foreach (var tile in tiles)
                if (!tileMap.ContainsKey(tile.GridPosition)) tileMap.Add(tile.GridPosition, tile);

            foreach (var tile in tiles)
            {
                string type = "MiddleCenter";
                int x = tile.GridPosition.X;
                int y = tile.GridPosition.Y;

                bool hasTop = tileMap.ContainsKey(new Point(x, y - 1));
                bool hasBottom = tileMap.ContainsKey(new Point(x, y + 1));
                bool hasLeft = tileMap.ContainsKey(new Point(x - 1, y));
                bool hasRight = tileMap.ContainsKey(new Point(x + 1, y));

                if (!tile.IsVisible) continue;

                if (tile.IsOneWay)
                {
                    bool leftIsPlatform = hasLeft && tileMap[new Point(x - 1, y)].IsOneWay;
                    bool rightIsPlatform = hasRight && tileMap[new Point(x + 1, y)].IsOneWay;

                    if (!leftIsPlatform && rightIsPlatform) type = "PlatformLeft";
                    else if (leftIsPlatform && rightIsPlatform) type = "PlatformMid";
                    else if (leftIsPlatform && !rightIsPlatform) type = "PlatformRight";
                    else type = "PlatformMid";
                }
                else
                {
                    if (!hasTop)
                    {
                        if (!hasLeft && !hasRight) type = "Single";
                        else if (!hasLeft && hasRight) type = "TopLeft";
                        else if (hasLeft && !hasRight) type = "TopRight";
                        else type = "TopCenter";
                    }
                    else if (!hasBottom)
                    {
                        if (!hasLeft && hasRight) type = "BottomLeft";
                        else if (hasLeft && !hasRight) type = "BottomRight";
                        else type = "BottomCenter";
                    }
                    else
                    {
                        if (!hasLeft && hasRight) type = "MiddleLeft";
                        else if (hasLeft && !hasRight) type = "MiddleRight";
                        else type = "MiddleCenter";
                    }
                }

                if (_sourceRects.ContainsKey(type))
                {
                    spriteBatch.Draw(_tilesetTexture, tile.Position, _sourceRects[type], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}