using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Core;

namespace TrustIssues.Entities
{
    public class WalkerEnemy : Enemy
    {
        private float speed = 2f;
        private int direction = 1;

        private float velocityY = 0f;
        private float gravity = 0.5f;

        private int height = 32;

        //animatie
        private AnimationManager _animManager;
        private SpriteEffects _spriteEffect;

        public WalkerEnemy(Texture2D texture, Vector2 startPosition)
        {
            Position = startPosition;
            var runAnim = new Animation(texture, 16, 32, 0.05f);
            _animManager = new AnimationManager();
            _animManager.Play(runAnim);
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
            // Update de animatie timer
            _animManager.Update(gameTime);

            // Spiegel de sprite op basis van richting
            if (direction > 0) _spriteEffect = SpriteEffects.FlipHorizontally; 
            else _spriteEffect = SpriteEffects.None;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _animManager.Draw(spriteBatch, Position, _spriteEffect);
        }
    }
    
}