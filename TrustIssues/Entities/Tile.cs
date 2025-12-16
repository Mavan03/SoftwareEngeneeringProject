using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;
namespace TrustIssues.Entities
{
    public class Tile
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }

        //hitbox
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 40, 40);
            }
        }
        //check voor solid van blok
        public bool IsSolid { get; set; } = true;

        public Tile(Texture2D texture, Vector2 position, bool isSolid)
        {
            Texture = texture;
            Position = position;
            IsSolid = isSolid;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

    }
}
