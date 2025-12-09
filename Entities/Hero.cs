using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoftwareEngeneeringProject.Entities
{
    public class Hero
    {
        //Encapsulation intern aanpassen
        private Texture2D texture;
        public Vector2 Position;//public voor lezen en schrijven
        public Vector2 Velocity;//snelheid

        public Hero(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            //movement
            //positie aanpassen op basis van snelheid
            Position += Velocity;

            Velocity = Vector2.Zero;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Tekenen karakter op pos
            spriteBatch.Draw(texture, Position,Color.White);
        }
        public void MoveLeft()
        {
            Velocity.X = -5;
        }
        public void MoveRight()
        {
            Velocity.X = 5;
        }
        public void Jump()
        {
            Velocity.Y = -5;
        }
        public void Crouch()
        {
            Velocity.Y = 5;
        }
    }
}
