using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TrustIssues.Entities
{
    public abstract class Enemy
    {

        public Vector2 Position;
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
        protected Texture2D texture;
        public bool IsDead { get; protected set; } = false;
        public bool IsExpired { get; protected set; } = false;

        public bool IsStompable { get; protected set; } = true;
        public abstract void Update(GameTime gameTime, Player player, List<Tile> tiles);

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Die();

    }
}
