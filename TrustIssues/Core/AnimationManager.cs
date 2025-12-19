using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrustIssues.Core
{
    public class AnimationManager
    {
        private Animation animation;
        private float timer;
        private int currentFram;

        public Rectangle CurrentFrameRect { get; private set; }

        public void Play(Animation animation)
        {
            if (this.animation == animation) return;
            this.animation = animation;
            currentFram = 0;
            timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (animation == null) return;
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(timer > animation.FrameSpeed)
            {
                timer = 0f;
                currentFram++;
                if(currentFram >= animation.FrameCount)
                {
                    if (animation.IsLooping)
                    {
                        currentFram = 0;
                    }
                    else
                        currentFram = animation.FrameCount - 1;
                }
            }
            CurrentFrameRect = new Rectangle(currentFram * animation.FrameWidth, 0, animation.FrameWidth, animation.Texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects)
        {
            if (animation == null) return;
            spriteBatch.Draw(
                animation.Texture,
                position,
                CurrentFrameRect,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                effects,
                0f);
        }
    }
}
