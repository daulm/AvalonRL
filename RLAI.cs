using System;
using System.Collections.Generic;

namespace AvalonRL
{
    public class RLAI
    {
        private int alg;
        private int role;
        private List<int> spyLoc;
        private int position;

        private static Random rng = new Random();

        public RLAI(int algorithm, int argrole, int pos)
	    {
            role = argrole;
            alg = algorithm;
            position = pos;

	    }

        public void LocateSpies(List<int> spies)
        {
            //the game will only call this for spies and merlin
            spyLoc = spies;
        }

        public bool CastVote(GameState state)
        {
            if (state.GetCurrentRound() == 4) return true;
            return rng.Next(2) == 1;
        }

        public List<int> ChooseTeam(GameState state, int size)
        {
            List<int> team = new List<int>();
            int pick;
            while (team.Count < size)
            {
                pick = rng.Next(7);
                if (!team.Contains(pick))
                {
                    team.Add(pick);
                }
            }

            return team;
        }

        public int MissionPlay(GameState state)
        {
            //only spies should be asked to play
            return rng.Next(2);
        }

        public int PickMerlin(GameState state)
        {
            int guess;
            while (true)
            {
                guess = rng.Next(7);
                if (!spyLoc.Contains(guess)) return guess;
            }
        }
    }
}
