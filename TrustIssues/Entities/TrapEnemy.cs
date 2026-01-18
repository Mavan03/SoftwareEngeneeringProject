using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TrustIssues.Entities
{
    internal class TrapEnemy : Enemy
    {
        private Texture2D _texture;

        public TrapEnemy(Texture2D texture, Vector2 startPosition)
        {
            _texture = texture;
            Position = startPosition;
            IsStompable = false; 
        }

        public override void Update(GameTime gameTime, Player player, List<Tile> tiles) { } 
        public override void Die() { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                
                spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            }
        }

        public new Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X + 6, (int)Position.Y + 14, 20, 18);
            }
        }
    }
}