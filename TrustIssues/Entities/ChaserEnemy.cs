using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TrustIssues.Entities
{
    public class ChaserEnemy : Enemy
    {
        private float speed = 1f;
        public ChaserEnemy(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
        }

        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {
            Vector2 direction = player.Position - Position;
            if(direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            Vector2 velocity = direction * speed;

           
            Position.X += velocity.X;
            Rectangle myRect = Bounds;

            foreach (var tile in tiles)
            {
                if (tile.IsSolid && myRect.Intersects(tile.Bounds))
                {
                    Position.X -= velocity.X;
                    break;
                }
            }
            Position.Y += velocity.Y;
            myRect = Bounds; // Update hitbox 

            foreach (var tile in tiles)
            {
                if (tile.IsSolid && myRect.Intersects(tile.Bounds))
                {
                    Position.Y -= velocity.Y;
                    break;
                }
            }
        }

    }
}
