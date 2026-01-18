using Microsoft.Xna.Framework;

namespace TrustIssues.Core
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 targetPosition)
        {
            var offset = Matrix.CreateTranslation((int)-targetPosition.X, (int)-targetPosition.Y, 0);

            var centerScreen = Matrix.CreateTranslation(400, 240, 0);

            Transform = offset * centerScreen;
        }
    }
}