using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrustIssues.Observers;
using TrustIssues.Core;

namespace TrustIssues.Entities
{
    public class Player
    {
        public Vector2 Position;

        // Texture & Animatie
        private AnimationManager animManager;
        private Animation idleAnimation;
        private Animation runAnimation;
        private Animation jumpAnimation;
        private Animation fallAnimation;
        private SpriteEffects spriteEffect;

        // Physics
        private Vector2 velocity;
        private const float Gravity = 0.35f;
        private const float JumpStrength = -9f;
        private const float MaxFallSpeed = 10f;
        private const float MoveSpeed = 5f;
        private bool _isGrounded = false;
        public Vector2 Velocity => velocity;

        // Hitbox
        private int width = 20;
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

        public Player(Texture2D idleText, Texture2D runTex, Texture2D jumpTex, Texture2D fallTex, Vector2 startPosition)
        {
            Position = startPosition;
            // Zorg dat FrameWidth 32 is (voor je 32x32 tileset)
            idleAnimation = new Animation(idleText, 11, 32, 0.1f);
            runAnimation = new Animation(runTex, 12, 32, 0.05f);
            jumpAnimation = new Animation(jumpTex, 1, 32, 0.1f);
            fallAnimation = new Animation(fallTex, 1, 32, 0.1f);

            animManager = new AnimationManager();
            animManager.Play(idleAnimation);
        }

        public void Update(GameTime gameTime, List<Tile> tiles)
        {
            // 1. Zwaartekracht toepassen
            velocity.Y += Gravity;
            if (velocity.Y > MaxFallSpeed) velocity.Y = MaxFallSpeed;

            // ==========================================================
            // STAP 1: X-AS BEWEGING
            // ==========================================================
            Position.X += velocity.X;
            Rectangle playerRect = Bounds;

            foreach (var tile in tiles)
            {
                if (tile.IsSolid && !tile.IsOneWay && playerRect.Intersects(tile.Bounds))
                {
                    if (velocity.X > 0) Position.X = tile.Bounds.Left - width - offsetX;
                    else if (velocity.X < 0) Position.X = tile.Bounds.Right - offsetX;

                    velocity.X = 0;
                    playerRect = Bounds;
                }
            }

            // ==========================================================
            // STAP 2: Y-AS BEWEGING
            // ==========================================================
            Position.Y += velocity.Y;
            playerRect = Bounds;

            // We resetten _isGrounded, maar onthouden even of we vorige keer grounded waren
            bool wasGrounded = _isGrounded;
            _isGrounded = false;

            foreach (var tile in tiles)
            {
                if (playerRect.Intersects(tile.Bounds))
                {
                    // HARDE MUUR
                    if (tile.IsSolid && !tile.IsOneWay)
                    {
                        if (velocity.Y > 0) // Landen
                        {
                            // HARD AFRONDEN NAAR HELE PIXELS
                            // Dit voorkomt dat je op bijv 300.35 blijft hangen
                            Position.Y = (int)(tile.Bounds.Top - height - offsetY);

                            velocity.Y = 0;
                            _isGrounded = true;
                        }
                        else if (velocity.Y < 0) // Hoofd stoten
                        {
                            Position.Y = (int)(tile.Bounds.Bottom - offsetY);
                            velocity.Y = 0;
                        }
                    }
                    // ONE-WAY PLATFORM
                    else if (tile.IsOneWay)
                    {
                        if (velocity.Y > 0 &&
                            playerRect.Bottom <= tile.Bounds.Bottom &&
                            (playerRect.Bottom - velocity.Y) <= tile.Bounds.Top + 5)
                        {
                            Position.Y = (int)(tile.Bounds.Top - height - offsetY);
                            velocity.Y = 0;
                            _isGrounded = true;
                        }
                    }
                }
            }

            // ==========================================================
            // ANIMATIE SELECTIE
            // ==========================================================

            // 1. Springen (Negatieve Y)
            if (velocity.Y < 0)
            {
                animManager.Play(jumpAnimation);
            }
            // 2. Vallen
            // HIER IS DE TRUC: Verander > 0 naar > 0.8f (of iets groter dan je Gravity 0.35)
            // Hierdoor negeert hij die kleine "tril-beweging" van de zwaartekracht.
            else if (velocity.Y > 0.8f && !_isGrounded)
            {
                animManager.Play(fallAnimation);
            }
            // 3. Rennen
            else if (velocity.X != 0)
            {
                animManager.Play(runAnimation);
            }
            // 4. Stilstaan
            else
            {
                animManager.Play(idleAnimation);
            }

            // Spiegelen
            if (velocity.X > 0) spriteEffect = SpriteEffects.None;
            else if (velocity.X < 0) spriteEffect = SpriteEffects.FlipHorizontally;

            animManager.Update(gameTime);
            velocity.X = 0; // Reset input voor volgende frame
        }

        public void Move(Vector2 direction)
        {
            if (direction.X != 0)
                velocity.X = direction.X > 0 ? MoveSpeed : -MoveSpeed;
        }

        public void Jump()
        {
            if (_isGrounded)
            {
                velocity.Y = JumpStrength;
                _isGrounded = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {    
            Vector2 drawPos = new Vector2((int)Position.X, (int)Position.Y);

            animManager.Draw(spriteBatch, drawPos, spriteEffect);
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
        public void Bounce()
        {
            // Lanceer de speler weer omhoog
            velocity.Y = -8f; // Iets minder hard dan een normale sprong (-12f)
            _isGrounded = false;
        }
        public void Die()
        {
            NotifyObeservers("PlayerDied");
        }
    }
}