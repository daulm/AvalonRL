using System;
using System.Collections.Generic;

namespace AvalonRL
{
    public class GameState
    {
        private static int numPlayers = 7;
        private int missionsSuccess;
        private int missionsFail;
        private int currentRound;
        private int currCaptain;
        private int totalRounds;
        private List<int> currTeam;
        private List<Round> rounds;

        public GameState()
	    {
            missionsSuccess = 0;
            missionsFail = 0;
            currentRound = 0;
            currCaptain = 0;
            totalRounds = 0;
            currTeam = new List<int>();
            rounds.Add(new Round(0, 0));
	    }


        public void SetTeam(List<int> newTeam)
        {
            currTeam = new List<int>(newTeam);
            rounds[totalRounds].team = new List<int>(newTeam);
        }

        public void SetVotes(List<bool> vote)
        {
            rounds[totalRounds].votes = new List<bool>(vote);
        }

        public void SetMissionFails(int fails)
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

            public Round(int capt, int misNo)
            {
                captain = capt;
                missionNum = misNo;
                team = new List<int>();

            }
        }
    }
}