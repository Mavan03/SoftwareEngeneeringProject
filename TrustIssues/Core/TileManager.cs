using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Entities;

namespace TrustIssues.Core
{
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

            // 1. DE NORMALE GROND BLOKKEN (BEGINNEN BIJ X=96)
            int startX = 96;
            int startY = 0;

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

            // 2. HET BRUINE PLATFORM BALKJE (NIEUW!)
            // We pakken het bruine houten platform.
            // Let op de hoogte: 5 pixels! Dit zorgt dat hij er dun uitziet.
            _sourceRects.Add("PlatformLeft", new Rectangle(272, 16, 16, 5));

            // Midden (Recht stuk)
            _sourceRects.Add("PlatformMid", new Rectangle(288, 16, 16, 5));

            // Rechts (Ronde rechterkant)
            _sourceRects.Add("PlatformRight", new Rectangle(304, 16, 16, 5));
        }

        private void AddRect(string name, int x, int y)
        {
            _sourceRects.Add(name, new Rectangle(x, y, 16, 16));
        }

        public void DrawTiles(SpriteBatch spriteBatch, List<Tile> tiles)
        {
            // 1. Maak een snelle zoeklijst (bestond al)
            // We maken nu een dictionary zodat we het TYPE tile kunnen opzoeken van de buren!
            // Dit is nodig omdat we willen weten of de buurman OOK een platform is.
            Dictionary<Point, Tile> tileMap = new Dictionary<Point, Tile>();
            foreach (var tile in tiles)
            {
                if (!tileMap.ContainsKey(tile.GridPosition))
                    tileMap.Add(tile.GridPosition, tile);
            }

            foreach (var tile in tiles)
            {
                string type = "MiddleCenter";

                int x = tile.GridPosition.X;
                int y = tile.GridPosition.Y;

                // Check buren
                bool hasTop = tileMap.ContainsKey(new Point(x, y - 1));
                bool hasBottom = tileMap.ContainsKey(new Point(x, y + 1));
                bool hasLeft = tileMap.ContainsKey(new Point(x - 1, y));
                bool hasRight = tileMap.ContainsKey(new Point(x + 1, y));

                // --- PLATFORM LOGICA (NIEUW) ---
                if (tile.IsOneWay)
                {
                    // We moeten checken of de buren OOK platforms zijn.
                    // Als er links gewoon gras is, telt dat niet als 'platform-buurman'.

                    bool leftIsPlatform = hasLeft && tileMap[new Point(x - 1, y)].IsOneWay;
                    bool rightIsPlatform = hasRight && tileMap[new Point(x + 1, y)].IsOneWay;

                    if (!leftIsPlatform && rightIsPlatform)
                    {
                        type = "PlatformLeft"; // Begin van de balk
                    }
                    else if (leftIsPlatform && rightIsPlatform)
                    {
                        type = "PlatformMid";  // Middenstuk
                    }
                    else if (leftIsPlatform && !rightIsPlatform)
                    {
                        type = "PlatformRight"; // Einde van de balk
                    }
                    else
                    {
                        type = "PlatformMid"; // Los stukje (of Single maken als je die hebt)
                    }
                }
                // --- NORMALE GROND LOGICA (Bestaand) ---
                else
                {
                    // ... Je oude logica voor gras/aarde ...
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

                // --- TEKENEN ---
                if (_sourceRects.ContainsKey(type))
                {
                    spriteBatch.Draw(
                        _tilesetTexture,
                        tile.Position,
                        _sourceRects[type],
                        Color.White,
                        0f,
                        Vector2.Zero,
                        _scale,
                        SpriteEffects.None,
                        0f
                    );
                }
            }
        }
    }
}