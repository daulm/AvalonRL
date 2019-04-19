using System;
using System.Collections.Generic;

namespace AvalonRL
{
    public class BotGame7
    {
        private int alg;
        private List<int> positions;
        private int result;
        private Gamestate gamestate;
        private List<RLAI> players;
        private static List<int> teamSize = new List<int>() { 2, 3, 3, 4, 4 };


        private static Random rng = new Random();


        public BotGame7(int algorithm)
        {
            alg = algorithm;
            gamestate = new Gamestate();
            players = new List<RLAI>();
            result = -1;
            //randomly set the positions
            positions = new List<int>(new int[] { 0, 0, 0, 2, 1, 1, 1 });
            positions.Shuffle();

            //create the bots
            foreach (int role in positions)
            {
                players.Add(new RLAI(alg, role));
            }

        }

        public int Play()
        {
            while (result < 0)
            {
                //execute a round


                //vote

                //go on mission

                //choose merlin

                //update gamestate



                //update result if game is over


            }

            return result;
        }

        private void RecordGame()
        {
            // make sure we have a game result
            if (result < 0) return;

        }

        private static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}