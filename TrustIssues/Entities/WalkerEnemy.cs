using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Core;

namespace TrustIssues.Entities
{
    public class WalkerEnemy : Enemy
    {
        private float _speed = 2f;
        private int _direction = 1;
        private float _velocityY = 0f;      
        private float _gravity = 0.5f;     

        private AnimationManager _animManager;
        private Animation _hitAnimation;

        public WalkerEnemy(Texture2D texture, Texture2D hitTexture, Vector2 startPosition)
        {
            Position = startPosition;
            var runAnim = new Animation(texture, 16, 32, 0.05f);
            _hitAnimation = new Animation(hitTexture, 5, 32, 0.1f);

            _animManager = new AnimationManager();
            _animManager.Play(runAnim);
        }

        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {
            if (IsDead)
            {
                _animManager.Update(gameTime);
                if (_animManager.CurrentFrame >= _hitAnimation.FrameCount - 1) IsExpired = true;
                return;
            }

            // ZWAARTEKRACHT
            _velocityY += _gravity;
            Position.Y += _velocityY;

            Rectangle bounds = Bounds;
            bool isGrounded = false;

            foreach (var tile in tiles)
            {
                if (tile.IsSolid && bounds.Intersects(tile.Bounds))
                {
                    // Als we vallen en de bovenkant raken
                    if (_velocityY > 0 && Position.Y < tile.Bounds.Top)
                    {
                        Position.Y = tile.Bounds.Top - 32; 
                        _velocityY = 0;
                        isGrounded = true;
                    }
                }
            }

            // BEWEGEN 
            Position.X += _speed * _direction;
            bounds = Bounds; 

            foreach (var tile in tiles)
            {
                if (tile.IsSolid && bounds.Intersects(tile.Bounds))
                {
                    _direction *= -1; 
                    Position.X += _speed * _direction; 
                    break;
                }
            }

            if (isGrounded)
            {
                Vector2 lookAhead = new Vector2(Position.X + 16 + (16 * _direction), Position.Y + 40);
                bool groundAhead = false;
                foreach (var tile in tiles)
                {
                    if (tile.IsSolid && tile.Bounds.Contains(lookAhead))
                    {
                        groundAhead = true;
                        break;
                    }
                }
                if (!groundAhead) _direction *= -1; 
            }

            _animManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = _direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            _animManager.Draw(spriteBatch, Position, effect);
        }

        public override void Die()
        {
            if (!IsDead) { IsDead = true; _animManager.Play(_hitAnimation); }
        }
    }
}