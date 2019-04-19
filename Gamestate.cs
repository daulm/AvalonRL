using System;
using System.Collections.Generic;

public class GameState
{
    private int missionsSuccess;
    private int missionsFail;
    private int currentRound;
    private int currCaptain;
    private int 
    private List<int> currTeam;
    private List<Round> rounds;

    public GameState()
	{
        missionsSuccess = 0;
        missionsFail = 0;
        currentRound = 0;
        currCaptain = 0;
	}

    public void SetCapt()
    {

    }

    public void SetTeam()
    {

    }

    public void SetVotes()
    {

    }

    public void SetMissionFails()
    {

    }

    public string GetCaptPattern()
    {
        return "";
    }

    public string GetMissionHist()
    {
        return "";
    }



    private class Round
    {
        public int captain;
        public int missionNum;  //0-4
        public List<int> team;
        public List<bool> votes;
        public int numFails;

        public Round()
        {

        }
    }
}
