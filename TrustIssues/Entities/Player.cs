using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TrustIssues.Observers;
using TrustIssues.Core;

namespace TrustIssues.Entities
{
    public class Player
    {
        public Vector2 Position;
        //txture
        private AnimationManager animManager;
        private Animation idleAnimation;
        private Animation runAnimation;
        private SpriteEffects spriteEffect;

        //phisics
        private Vector2 velocity;
        private const float Gravity = 0.35f;
        private const float JumpStrength=-9f;
        private const float MaxFallSpeed = 10f;
        private const float MoveSpeed = 5f; 
        private bool _isGrounded = false;

        //Hitbox
        private int width = 20;  // 14-20
        private int height = 30; 
        private int offsetX = 6; 
        private int offsetY = 2;

        private List<IGameObserver> Observers = new List<IGameObserver>();

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                     (int)Position.X + offsetX,
                     (int)Position.Y + offsetY,
                     width,
                     height
                 );
            }
        }
        public Player(Texture2D idleText,Texture2D runTex, Vector2 startPosition)
        {
            Position = startPosition;
            idleAnimation = new Animation(idleText, 11,32, 0.1f);
            runAnimation = new Animation(runTex, 12, 32, 0.05f);

            animManager = new AnimationManager();
            animManager.Play(idleAnimation);

        }
        public void Update(GameTime gameTime, List<Tile> tiles)
        {
            // Zwaartekracht 
            velocity.Y += Gravity;
            if (velocity.Y > MaxFallSpeed) velocity.Y = MaxFallSpeed;

            // X-AS BEWEGING & BOTSING
            Position.X += velocity.X;

            Rectangle playerRect = Bounds; // Update hitbox na X beweging

            foreach (var tile in tiles)
            {
                // OneWay platforms negeren 
                if (tile.IsSolid && !tile.IsOneWay && playerRect.Intersects(tile.Bounds))
                {
                    Rectangle overlap = Rectangle.Intersect(playerRect, tile.Bounds);

                    // Simpele X botsing resolutie
                    if (playerRect.Center.X < tile.Bounds.Center.X)
                        Position.X -= overlap.Width; // Duw naar links
                    else
                        Position.X += overlap.Width; // Duw naar rechts

                    velocity.X = 0;
                    playerRect = Bounds; // Update bounds direct!
                }
            }

            //Y-AS BEWEGING & BOTSING
            Position.Y += velocity.Y;

            playerRect = Bounds; // Update hitbox na Y beweging
            _isGrounded = false; // Reset grounded status

            foreach (var tile in tiles)
            {
                if (playerRect.Intersects(tile.Bounds))
                {
                    // HARDE MUUR (Solid & !OneWay) 
                    if (tile.IsSolid && !tile.IsOneWay)
                    {
                        Rectangle overlap = Rectangle.Intersect(playerRect, tile.Bounds);

                        if (velocity.Y > 0) // Landen
                        {
                            Position.Y -= overlap.Height;
                            velocity.Y = 0;
                            _isGrounded = true;
                        }
                        else if (velocity.Y < 0) // Hoofd stoten
                        {
                            Position.Y += overlap.Height;
                            velocity.Y = 0.5f; // Klein tikje terug
                        }
                        playerRect = Bounds;
                    }

                    //ONE-WAY PLATFORM
                    else if (tile.IsOneWay)
                    {

                        if (velocity.Y > 0 &&
                            playerRect.Bottom <= tile.Bounds.Bottom &&
                            (playerRect.Bottom - velocity.Y) <= tile.Bounds.Top + 5)
                        {
                            // Landen op platform
                            Position.Y = tile.Bounds.Top - height - offsetY; // Zet precies bovenop
                            velocity.Y = 0;
                            _isGrounded = true;
                        }
                    }
                }
            }
            if (velocity.X != 0)
            {
                animManager.Play(runAnimation);
            }
            else
            {
                animManager.Play(idleAnimation);
            }

            if (velocity.X > 0)
                spriteEffect = SpriteEffects.None;
            else if (velocity.X < 0)
                spriteEffect = SpriteEffects.FlipHorizontally;

            animManager.Update(gameTime);
            velocity.X = 0; // Reset input velocity
        }
        public void Move(Vector2 direction)
        {
            if (direction.X != 0)
                velocity.X = direction.X > 0 ? MoveSpeed : -MoveSpeed;
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
            animManager.Draw(spriteBatch, Position, spriteEffect);
        }

        public void AddObserver(IGameObserver observer)
        {
            Observers.Add(observer);
        }
        private void NotifyObeservers(string eventName)
        {
            foreach (var observer in Observers)
            {
                observer.OnNotify(eventName);
            }
        }
        public void Die()
        {
            NotifyObeservers("PlayerDied");
        }
    }
}
