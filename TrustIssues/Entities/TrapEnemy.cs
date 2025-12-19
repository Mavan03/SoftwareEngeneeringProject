using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TrustIssues.Entities
{
    internal class TrapEnemy : Enemy
    {
        public TrapEnemy(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
        }
        public override void Update(GameTime gameTime, Player player, List<Tile> tiles)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            }
        }
        public new Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X + 4, (int)Position.Y + 16, 24, 16);
            }
        }
    }
}
