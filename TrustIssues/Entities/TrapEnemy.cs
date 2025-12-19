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
        public new Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X + 4, (int)Position.Y + 20, 24, 12);
            }
        }
    }
}
