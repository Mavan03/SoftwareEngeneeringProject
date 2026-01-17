using System.Collections.Generic;

namespace TrustIssues.Data
{
    public class LevelMaps
    {
        // # = Grond, S = Start, V = Vijand, X = Exit 
        public static List<string[]> AllLevels = new List<string[]>
        {
            new string[]
            {
                "#######################################",
                "#                                     #",
                "#                                     #",
                "#                                     #",
                "#                                     #",
                "#                   B                 #", // B = Vleermuis (vliegt)
                "#                                     #",
                "#      V                  V         X #", // X = Einde
                "# S   ###     T    T     ###       ####", // S = Start, T = Spikes
                "#######################################"
            },
            
            // ==========================================================
            // LEVEL 2: THE CLIMB (OMHOOG)
            // Gebruik de '---' platforms om naar de top te springen.
            // ==========================================================
            new string[]
            {
                "######################",
                "#                   X#", // Doel is helemaal rechtsboven
                "#                #####",
                "#             ---    #",
                "#          ---       #",
                "#       ---      B   #", // Pas op voor de vleermuis tijdens het springen
                "#    ---             #",
                "#                    #",
                "#---        V        #", // Paddestoel loopt op een zwevend platform
                "#      ###########   #",
                "#                    #",
                "# S                  #", // Start beneden
                "######################"
            },

            // ==========================================================
            // LEVEL 3: HARDCORE (ALLES DOOR ELKAAR)
            // Omhoog, omlaag, rennen en springen.
            // ==========================================================
            new string[]
            {
                "#######################################",
                "#                                     #", // Einde is linksboven...
                "#X                                    #",
                "#####          B          B           #",
                "#         ---       ---       ---     #",
                "#                                     #",
                "#                                     #",
                "#               V         V           #",
                "#     #####   #####     #####   ####  #", // ...maar je moet via rechts
                "#  --                                -#",
                "#       -    -     -     -     -      #",
                "#       T     T     T     T     T     #", // Pas op voor de spikes beneden!
                "# S    ###   ###   ###   ###   ###    #",
                "#######################################"
            }
        };
    }
}
