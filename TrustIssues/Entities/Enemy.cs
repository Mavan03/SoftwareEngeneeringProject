using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TrustIssues.Entities
{
    public abstract class Enemy
    {
        public Vector2 Position;
        protected Texture2D texture;

        public abstract void Update(GameTime gameTime, Player player, List<Tile> tiles);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Position, Color.White);
        }
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
            }
        }
    }
}
