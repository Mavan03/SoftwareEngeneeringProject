using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrustIssues.Core
{
    public class AnimationManager
    {
        private Animation _animation;
        private float _timer;

        public int CurrentFrame { get; private set; }

        public Rectangle CurrentFrameRect { get; private set; }

        public void Play(Animation animation)
        {
            if (_animation == animation) return;
            _animation = animation;
            CurrentFrame = 0;
            _timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            //timer  voor current frame
            if (_animation == null) return;
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                CurrentFrame++;
                if (CurrentFrame >= _animation.FrameCount)
                {
                    if (_animation.IsLooping)
                        CurrentFrame = 0;
                    else
                        CurrentFrame = _animation.FrameCount - 1; 
                }
            }
            CurrentFrameRect = new Rectangle(CurrentFrame * _animation.FrameWidth, 0, _animation.FrameWidth, _animation.Texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects)
        {
            // welk stuktje dat getekent moet worden
            if (_animation == null) return;
            spriteBatch.Draw(_animation.Texture, position, CurrentFrameRect, Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
        }
    }
}