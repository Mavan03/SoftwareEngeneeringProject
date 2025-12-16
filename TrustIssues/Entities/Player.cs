using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrustIssues.Entities
{
    public class Player
    {
        public Vector2 Position;
        private Texture2D texture;

        public Player(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
        public void Move(Vector2 delta)
        {
            Position += delta;
        }
    }
}
