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
        private int width = 14;  // 14-20
        private int height = 30; 
        private int offsetX = 9; 
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
            // Pas zwaartekracht toe
            velocity.Y += Gravity;
            if (velocity.Y > MaxFallSpeed) velocity.Y = MaxFallSpeed;

            // bewegen EERST X en Y tegelijk in onze positie
            Position += velocity;

            // halen de hitbox op van de NIEUWE positie
            Rectangle playerRect = Bounds;

            // We gaan ervan uit dat we vallen, tenzij we op een blok botsen
            _isGrounded = false;

            foreach (var tile in tiles)
            {
                // Alleen checken als het een muur is en we hem raken
                if (tile.IsSolid && playerRect.Intersects(tile.Bounds))
                {
                    // BEREKEN DE OVERLAP
                    // Rectangle.Intersect geeft ons het rechthoekje waar de twee elkaar raken
                    Rectangle overlap = Rectangle.Intersect(playerRect, tile.Bounds);
                                   
                    // Als de overlap smaller is dan dat hij hoog is...
                    // Dan raken we waarschijnlijk de ZIJKANT (Muur)
                    if (overlap.Width < overlap.Height)
                    {
                        // We duwen de speler horizontaal weg

                        // Komen we van links? (Speler center zit links van tegel center)
                        if (playerRect.Center.X < tile.Bounds.Center.X)
                        {
                            // Duw naar links
                            Position.X -= overlap.Width;
                        }
                        else
                        {
                            // Duw naar rechts
                            Position.X += overlap.Width;
                        }

                        // Stop X snelheid (zodat je niet blijft glijden)
                        velocity.X = 0;
                    }
                    // Anders is de overlap breder dan hoog...
                    // Dan raken we de VLOER of het PLAFOND
                    else
                    {
                        // Komen we van boven? (Speler center zit boven tegel center)
                        if (playerRect.Center.Y < tile.Bounds.Center.Y)
                        {
                            // We landen bovenop (Vloer)
                            Position.Y -= overlap.Height;
                            velocity.Y = 0;
                            _isGrounded = true;
                        }
                        else
                        {
                            // We stoten ons hoofd (Plafond)
                            Position.Y += overlap.Height;

                            // Laat de speler direct weer vallen (geen 0, anders plak je)
                            velocity.Y = 0.5f;
                        }
                    }

                    // Update de playerRect direct voor de volgende check in de loop!
                    // Dit lost het "Naad" probleem op.
                    playerRect = Bounds;

                }
            }
            //bepaal welke anim
            if(velocity.X !=0)
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
            velocity.X = 0;

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
