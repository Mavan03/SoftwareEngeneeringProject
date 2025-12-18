using System.Collections.Generic;

namespace TrustIssues.Data
{
    public class LevelMaps
    {
        //levle1 tutorial
        // # = Grond, S = Start, V = Vijand, X = Exit 
        public static string[] Level1 = new string[]
        {
            "....................",
            "....................",
            "....................",
            "...................X", // exit
            "############...#####",
            "#S...........#.....#", // Start
            "#........V.........#", // Enemy
            "#...#####...#####..#",
            "#..................#",
            "####################"
        };

        // level2
        public static string[] Level2 = new string[]
        {
            "X...................", // Linksboven exit
            "###.................",
            "..#.................",
            "..#######...........",
            "........##...####...",
            "........###...#......",
            "S.......#.V...#.....",
            "#########...##......",
            ".............#......",
            "#############......."
        };
        public static List<string[]> AllLevels = new List<string[]> { Level1, Level2 };
    }
}
