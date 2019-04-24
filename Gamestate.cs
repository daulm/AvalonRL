using System;
using System.Collections.Generic;

namespace AvalonRL
{
    public class GameState
    {
        private int numPlayers;
        private int missionsSuccess;
        private int missionsFail;
        private int currentRound;
        private int currCaptain;
        private int currMission;
        private int totalRounds;
        private List<int> currTeam;
        private List<Round> rounds;
        private List<int> missionHist;
        private int merlinSelected;
        private int finalResult;

        public GameState(int num)
	    {
            numPlayers = num;
            missionsSuccess = 0;
            missionsFail = 0;
            currentRound = 0;
            currCaptain = 0;
            currMission = 0;
            totalRounds = 0;
            currTeam = new List<int>();
            missionHist = new List<int>();
            rounds = new List<Round>();
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

        public void SetMissionFail(int player)
        {
            rounds[totalRounds].SetFail(player);
        }

        public void SetMissionResult(int result)
        {
            rounds[totalRounds].missionResult = result;
            if (result == 0)
            {
                missionsFail++;
            }
            else
            {
                missionsSuccess++;
            }
            missionHist.Add(result);
        }

        public void SetMerlinPick(int pick)
        {
            merlinSelected = pick;
        }

        public void NextRound()
        {
            if (rounds[totalRounds].missionResult >= 0)
            {
                currentRound = 0;
                currMission++;
            }
            totalRounds++;
            currCaptain = currCaptain + 1 % numPlayers;
            rounds.Add(new Round(currCaptain, currMission));
        }

        public void SetFinalResult(int result)
        {
            finalResult = result;
        }

        public int GetFailCount()
        {
            return missionsFail;
        }

        public int GetSuccessCount()
        {
            return missionsSuccess;
        }

        public List<int> GetCurrentTeam()
        {
            return currTeam;
        }

        public int GetCurrentRound()
        {
            return currentRound;
        }

        public string GetCaptPattern()
        {
            return "";
        }

        public List<int> GetMissionHist()
        {
            return missionHist;
        }

        public int GetFinalResult() {  return finalResult;  }



        private class Round
        {
            public int captain;
            public int missionNum;  //0-4
            public int missionResult;
            public List<int> team;
            public List<bool> votes;
            public int numFails;
            public List<int> playedFail;

            public Round(int capt, int misNo)
            {
                captain = capt;
                missionNum = misNo;
                numFails = 0;
                missionResult = -1;
                team = new List<int>();
                playedFail = new List<int>();

            }

            public void SetFail(int player)
            {
                playedFail.Add(player);
                numFails++;
            }
        }
    }
}