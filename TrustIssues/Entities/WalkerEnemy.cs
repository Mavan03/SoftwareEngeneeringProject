using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Net;

namespace TrustIssues.Entities
{
    public class WalkerEnemy : Enemy
    {
        private float speed = 2f;
        private int direction = 1;

        public WalkerEnemy(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
        }
        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {
            Position.X += speed * direction;

            Rectangle myRect = Bounds;
            foreach(var tile in tiles)
            {
                if (tile.IsSolid && myRect.Intersects(tile.Bounds))
                {
                    if (direction == 1)
                    {
                        direction = -1;
                        Position.X += speed * direction;
                    }
                    else
                    {
                        direction = 1;
                        Position.X += speed * direction;
                    }
                }
            }
        }
    }
}
