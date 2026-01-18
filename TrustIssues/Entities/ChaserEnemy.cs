using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Core;

namespace TrustIssues.Entities
{
    public class ChaserEnemy : Enemy
    {
        private float _speed = 1.5f;
        private AnimationManager _animManager;
        private Animation _flyAnimation;
        private Animation _hitAnimation;
        private SpriteEffects _spriteEffect;

        public ChaserEnemy(Texture2D flyTexture, Texture2D hitTexture, Vector2 startPosition)
        {
            Position = startPosition;
            _flyAnimation = new Animation(flyTexture, 7, 46, 0.1f, true);
            _hitAnimation = new Animation(hitTexture, 5, 46, 0.1f, false);

            _animManager = new AnimationManager();
            _animManager.Play(_flyAnimation);
        }

        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {
            if (IsDead)
            {
                _animManager.Update(gameTime);
                if (_animManager.CurrentFrame >= _hitAnimation.FrameCount - 1) IsExpired = true;
                return;
            }

            // Chase logic
            Vector2 direction = player.Position - Position;
            if (direction != Vector2.Zero) direction.Normalize();
            Position += direction * _speed;

            _spriteEffect = direction.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            _animManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawPos = new Vector2(Position.X - 7, Position.Y);
            _animManager.Draw(spriteBatch, drawPos, _spriteEffect);
        }

        public override void Die()
        {
            if (!IsDead) { IsDead = true; _animManager.Play(_hitAnimation); }
        }

        public new Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y + 10, 32, 20);
    }
}