using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TrustIssues.Entities
{
    public class WalkerEnemy : Enemy
    {
        private float speed = 2f;
        private int direction = 1;

        private float velocityY = 0f;
        private float gravity = 0.5f;

        private int height = 32;

        public WalkerEnemy(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
        }

        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {
            // Zwaartekracht
            velocityY += gravity;
            Position.Y += velocityY;

            bool isGrounded = false;
            Rectangle myRect = Bounds; 

            foreach (var tile in tiles)
            {
                if (tile.IsSolid && myRect.Intersects(tile.Bounds))
                {
                    // Als we vallen en de bovenkant raken
                    if (velocityY > 0 && Position.Y < tile.Bounds.Top)
                    {
                        Position.Y = tile.Bounds.Top - height;

                        Position.Y = (int)Position.Y;

                        velocityY = 0;
                        isGrounded = true;
                    }
                }
            }

            // 2. Slimme AI (Sensor)
            if (isGrounded)
            {
                float lookAheadX = Position.X + 16 + (16 * direction);
                float lookDownY = Position.Y + 40; 

                Vector2 sensorSpot = new Vector2(lookAheadX, lookDownY);
                bool groundDetected = false;

                foreach (var tile in tiles)
                {
                    if (tile.IsSolid && tile.Bounds.Contains(sensorSpot))
                    {
                        groundDetected = true;
                        break;
                    }
                }

                if (!groundDetected)
                {
                    direction *= -1;
                }
            }

            Position.X += speed * direction;

            myRect = Bounds;

            foreach (var tile in tiles)
            {
                if (tile.IsSolid && myRect.Intersects(tile.Bounds))
                {
                    if (myRect.Bottom > tile.Bounds.Top + 5)
                    {
                        direction *= -1;
                        Position.X += speed * direction;
                    }
                }
            }
        }
    }
}