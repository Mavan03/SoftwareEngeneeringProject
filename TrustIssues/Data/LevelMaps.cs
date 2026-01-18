using System.Collections.Generic;

namespace TrustIssues.Data
{
    public static class LevelMaps
    {
        // LEGEND: # = Grond, S = Start, V = Walker, B = Bat, T = Trap, X = Exit, - = Platform, I = Onzichtbare muur,F = Fake vloer
        public static List<string[]> AllLevels = new List<string[]>
        {
            // LEVEL 1
            new string[]
            {
                "#######################################",
                "#                                     #",
                "#                                    X#",
                "#                                   ###",
                "#                                  ####",
                "#                 B               #####",
                "#                   ------        #####",
                "#          F                    # #####",
                "#       #     #       V         # #####",
                "#      ##     ##     ###      # # #####",
                "#     ###     ###            ## # #####",
                "# S  ####     ####          ### # #####",
                "#########     #######IIFII##### # #####",
                "#      I      I       I I             #",
                "#      I TTTTTTTTTTTTTTTTTTTTTTTTTTTTT#",
                "#######################################"
            },
            
            // LEVEL 2:
            new string[]
            {   "#######################################",
                "#                                     #",
                "#                                     #",
                "#                                     #",
                "#                   V      ---  I     #",
                "#        --      ######         I    X#",
                "#---                             ######",
                "#                                     #",
                "#     ####FF###           -----       #",
                "#      ###FF##      ---               #",
                "#                                     #",
                "#                        --           #",
                "#                 ---                 #",
                "# S                                   #",
                "#######IIFFII####                     #",
                "#                                     #",
                "#TTTTTT  TT  TTTTTTTTTTTTTTTTTTTTTTTTT#",
                "#######################################"
            },

            // LEVEL 3
                new string[]
            {
                "#######################################",
                "#                                     #",
                "#X                                    #",
                "#####F###       --                    #",
                "#     I                 B             #",
                "#     I--    ----                     #",
                "#     I    I                          #",
                "#     I----I            ###FF###      #",
                "#     I    I          #####FF#####    #",
                "#TTTTTTTTTT    V     ######  #######  #",
                "###########  #####  #######  #######--#",
                "#                                     #",
                "#                                  ---#",
                "#                                     #",
                "#          T     T                    #",
                "# S       ### V ### V  ### TT ### V   #",
                "#######################################",
                "#TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT#",
                "#######################################"
            }
        };
    }
}