using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
namespace TrustIssues.Entities
{
    public class Tile
    {
        public Vector2 Position { get; private set; }
        public bool IsSolid { get; private set; }
        public bool IsOneWay { get; private set; } 
        public Point GridPosition { get; private set; }

        public Tile(Vector2 position, bool isSolid, bool isOneWay, Point gridPos)
        {
            Position = position;
            IsSolid = isSolid;
            IsOneWay = isOneWay;
            GridPosition = gridPos;
        }
        //hitbox
        public Rectangle Bounds
        {
            get
            {
                if (IsOneWay)
                    return new Rectangle((int)Position.X, (int)Position.Y, 32, 8);
                return new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
            }
        }
    }
}
