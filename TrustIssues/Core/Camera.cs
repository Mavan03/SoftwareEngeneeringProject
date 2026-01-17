using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace TrustIssues.Core
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 targetPosition)
        {
            // We ronden de positie af naar hele pixels met (int)
            // Dit voorkomt dat de wereld trilt als de speler stilstaat.

            var offset = Matrix.CreateTranslation(
                (int)-targetPosition.X,
                (int)-targetPosition.Y,
                0);

            var centerScreen = Matrix.CreateTranslation(
                400, // Helft schermbreedte (check je Game1 resolutie)
                240, // Helft schermhoogte
                0);

            Transform = offset * centerScreen;
        }
    }
}
