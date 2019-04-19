using System;
using System.Collections.Generic;

public class RLAI
{
    private int alg;
    private int role;
    private List<int> spyLoc;

    private static Random rng = new Random();

    public RLAI(int algorithm, int role)
	{
        role = role;
        alg = algorithm;

	}

    public LocateSpies(List<int> spies)
    {
        //the game will only call this for spies and merlin
        spyLoc = spies;
    }

    public bool CastVote(Gamestate state)
    {
        return rng.Next(2);
    }

    public List<int> ChooseTeam(Gamestate state, int size)
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
    }
}
