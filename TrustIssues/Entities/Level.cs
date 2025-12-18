using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TrustIssues.Entities
{
    public class Level
    {
        public List<Tile> Tiles { get; set; } = new List<Tile>();
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public Vector2 PlayerStart { get; set; } = new Vector2(100,100);
        public Rectangle ExitZone { get; set; }
    }
}
