using Microsoft.Xna.Framework.Graphics;

namespace TrustIssues.Core
{
    public class Animation
    {
        public Texture2D Texture { get; set; }
        public int FrameCount { get; set; }
        public float FrameSpeed { get; set; }
        public bool IsLooping { get; set; }
        public int FrameWidth { get; set; }

        public Animation(Texture2D texture, int frameCount, int frameWidth, float frameSpeed, bool isLooping = true)
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameWidth = frameWidth;
            FrameSpeed = frameSpeed;
            IsLooping = isLooping;
        }
    }
}