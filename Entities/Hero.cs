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

        //Physics var
        private const float Gravity = 0.5f;
        private const float JumpStrength = -12f;
        private const float MoveSpeed = 5f;

        private bool isGrounded;
        public Hero(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            //movement
            //zwaartekracht
            Velocity.Y += Gravity;
            //positie aanpassen op basis van snelheid
            Position += Velocity;

            //collision temp
            if (Position.Y >=400)
            {
                Position.Y = 400; // zet op de grond
                Velocity.Y = 0; // stop met vallen
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            //reset snelheid
            Velocity.X = 0;

            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Tekenen karakter op pos
            spriteBatch.Draw(texture, Position,Color.White);
        }
        public void MoveLeft()
        {
            Velocity.X = -MoveSpeed;
        }
        public void MoveRight()
        {
            Velocity.X = MoveSpeed;
        }
        public void Jump()
        {
            if(isGrounded)
            {
                Velocity.Y = -MoveSpeed;
                isGrounded = false;
            }
        }
        public void Crouch()
        {
            
        }
    }
}
