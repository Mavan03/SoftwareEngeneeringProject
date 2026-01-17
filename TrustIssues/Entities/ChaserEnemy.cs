using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Core;

namespace TrustIssues.Entities
{
    public class ChaserEnemy : Enemy
    {
        private float speed = 1f;

        private AnimationManager _animManager;
        private SpriteEffects _spriteEffect;

        public ChaserEnemy(Texture2D texture, Vector2 startPosition)
        {
            Position = startPosition;

            // Bat Flying heeft 7 frames en is 46 pixels breed
            var flyAnim = new Animation(texture, 7, 46, 0.1f);

            _animManager = new AnimationManager();
            _animManager.Play(flyAnim);
        }
       

        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {

            Vector2 direction = player.Position - Position;
            if (direction.X > 0) _spriteEffect = SpriteEffects.FlipHorizontally;
            else _spriteEffect = SpriteEffects.None;

            _animManager.Update(gameTime);

            if (direction != Vector2.Zero)
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawPos = new Vector2(Position.X - 7, Position.Y);
            _animManager.Draw(spriteBatch, drawPos, _spriteEffect);
        }

        public new Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, 32, 32); }
        }

    }
}
