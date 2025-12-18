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

        private float velocityY = 0f;
        private float gravity = 0.5f;
        public WalkerEnemy(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
        }
        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {
            velocityY += gravity;
            Position.Y += velocityY;
            bool isGrounded = false;

            Rectangle myRect = Bounds;
            foreach (var tile in tiles)
            {
                if (tile.IsSolid && myRect.Intersects(tile.Bounds))
                {
                    // Als we vallen (snelheid > 0) en we raken de bovenkant van een blokje
                    if (velocityY > 0 && Position.Y < tile.Bounds.Top)
                    {
                        Position.Y = tile.Bounds.Top - 40; // Zet bovenop (aanname 40px hoog)
                        velocityY = 0; // Stop met vallen
                    }
                    isGrounded = true;
                }
            }
            if (isGrounded)
            {
               
                float lookAheadX = Position.X + 20 + (25 * direction); 
                float lookDownY = Position.Y + 45; 

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
            if (Position.Y > 1000)
            {
                
            }
        }
    }
}
