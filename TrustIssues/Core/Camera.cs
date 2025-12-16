using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace TrustIssues.Core
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 targetPosition)
        {
            var offset = Matrix.CreateTranslation(
                -targetPosition.X,
                -targetPosition.Y,
                0);

            var centerScreen = Matrix.CreateTranslation(
                400,
                240,
                0);
            Transform = offset * centerScreen;
        }
    }
}
