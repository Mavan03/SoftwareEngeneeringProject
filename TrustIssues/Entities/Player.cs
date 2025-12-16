using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TrustIssues.Entities
{
    public class Player
    {
        public Vector2 Position;

        //phisics
        private Vector2 velocity;
        private const float Gravity = 0.5f;
        private const float JumpStrength=-12f;
        private bool _isGrounded = false;

        //txture
        private Texture2D texture;

        public Player(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            Position = startPosition;
        }
        public void Update(GameTime gameTime)
        {
            //zwaartekracht toepassen + sneller vallen + update pos
            velocity.Y += Gravity;
            Position += velocity;

            //vloer
            if (Position.Y >= 400)
            {
                Position.Y = 400;//Position is ClearOptions grond
                velocity.Y = 0;//stop vallen
                _isGrounded = true;//op vloer
            }
            else
                _isGrounded = false;

            velocity.X = 0;
        }
        public void Move(Vector2 delta)
        {
            Position.X += delta.X;
        }
        public void Jump()
        {
            if(_isGrounded)
            {
                velocity.Y = JumpStrength;
                _isGrounded = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
