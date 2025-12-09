using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoftwareEngeneeringProject.Entities
{
    internal class Hero
    {
        //Encapsulation intern aanpassen
        private Texture2D texture;
        private Vector2 position;

        public Hero(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            //movement
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Tekenen karakter op pos
            spriteBatch.Draw(texture, position,Color.White);
        }
    }
}
