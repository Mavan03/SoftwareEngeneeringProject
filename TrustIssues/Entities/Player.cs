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
        public Vector2 Velocity => velocity;

        private const float Gravity = 0.35f;
        private const float JumpStrength = -9f;
        private const float MaxFallSpeed = 10f;
        private const float MoveSpeed = 5f;

        private Vector2 velocity;
        private bool _isGrounded = false;

        // Animatie
        private AnimationManager animManager;
        private Animation idleAnimation, runAnimation, jumpAnimation, fallAnimation;
        private SpriteEffects spriteEffect;

        // Hitbox
        private int width = 20;
        private int height = 30;
        private int offsetX = 6;
        private int offsetY = 2;

        // Observer Pattern
        private List<IGameObserver> Observers = new List<IGameObserver>();

        public Rectangle Bounds => new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, width, height);

        public Player(Texture2D idleText, Texture2D runTex, Texture2D jumpTex, Texture2D fallTex, Vector2 startPosition)
        {
            Position = startPosition;
            idleAnimation = new Animation(idleText, 11, 32, 0.1f);
            runAnimation = new Animation(runTex, 12, 32, 0.05f);
            jumpAnimation = new Animation(jumpTex, 1, 32, 0.1f);
            fallAnimation = new Animation(fallTex, 1, 32, 0.1f);

            animManager = new AnimationManager();
            animManager.Play(idleAnimation);
        }

        public void Update(GameTime gameTime, List<Tile> tiles)
        {
            ApplyGravity();
            MoveX(tiles);
            MoveY(tiles);
            UpdateAnimation(gameTime);

            velocity.X = 0;
        }

        private void ApplyGravity()
        {
            velocity.Y += Gravity;
            if (velocity.Y > MaxFallSpeed) velocity.Y = MaxFallSpeed;
        }

        private void MoveX(List<Tile> tiles)
        {
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
        }

        private void MoveY(List<Tile> tiles)
        {
            Position.Y += velocity.Y;
            Rectangle playerRect = Bounds;
            _isGrounded = false;

            foreach (var tile in tiles)
            {
                if (playerRect.Intersects(tile.Bounds))
                {
                    if (tile.IsSolid && !tile.IsOneWay) 
                    {
                        if (velocity.Y > 0) { Position.Y = (int)(tile.Bounds.Top - height - offsetY); velocity.Y = 0; _isGrounded = true; }
                        else if (velocity.Y < 0) { Position.Y = (int)(tile.Bounds.Bottom - offsetY); velocity.Y = 0; }
                    }
                    else if (tile.IsOneWay) 
                    {
                        if (velocity.Y > 0 && playerRect.Bottom <= tile.Bounds.Bottom && (playerRect.Bottom - velocity.Y) <= tile.Bounds.Top + 5)
                        {
                            Position.Y = (int)(tile.Bounds.Top - height - offsetY);
                            velocity.Y = 0;
                            _isGrounded = true;
                        }
                    }
                }
            }
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (velocity.Y < 0) animManager.Play(jumpAnimation);
            else if (velocity.Y > 0.8f && !_isGrounded) animManager.Play(fallAnimation);
            else if (velocity.X != 0) animManager.Play(runAnimation);
            else animManager.Play(idleAnimation);

            if (velocity.X > 0) spriteEffect = SpriteEffects.None;
            else if (velocity.X < 0) spriteEffect = SpriteEffects.FlipHorizontally;

            animManager.Update(gameTime);
        }

        public void Move(Vector2 direction)
        {
            if (direction.X != 0) velocity.X = direction.X > 0 ? MoveSpeed : -MoveSpeed;
        }

        public void Jump()
        {
            if (_isGrounded) { velocity.Y = JumpStrength; _isGrounded = false; }
        }

        public void Bounce()
        {
            velocity.Y = -8f;
            _isGrounded = false;
        }

        public void Die()
        {
            NotifyObeservers("PlayerDied");
        }

        public void AddObserver(IGameObserver observer) => Observers.Add(observer);
        private void NotifyObeservers(string eventName) { foreach (var obs in Observers) obs.OnNotify(eventName); }

        public void Draw(SpriteBatch spriteBatch)
        {
            animManager.Draw(spriteBatch, new Vector2((int)Position.X, (int)Position.Y), spriteEffect);
        }
    }
}