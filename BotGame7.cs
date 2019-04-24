using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace AvalonRL
{
    public class BotGame7
    {
        private int alg;
        private List<int> positions;
        private int result;
        private GameState gamestate;
        private List<RLAI> players;
        private static List<int> teamSize = new List<int>() { 2, 3, 3, 4, 4 };
        private static List<int> failsReq = new List<int>() { 1, 1, 1, 2, 1 };
        private List<int> spyLocations;
        private int dumpLocation;
        private FileStream dumpfs;
        private static int gameSize = 7;


        private static Random rng = new Random();


        public BotGame7(int algorithm, int dataLoc)
        {
            alg = algorithm;
            dumpLocation = dataLoc;
            gamestate = new GameState(gameSize);
            players = new List<RLAI>();
            spyLocations = new List<int>();
            result = -1;
            //randomly set the positions
            positions = Shuffle(new List<int>(new int[] { 0, 0, 0, 2, 1, 1, 1 }));

            //create the bots
            for (int i = 0; i < positions.Count; i++)
            {
                int role = positions[i];
                players.Add(new RLAI(alg, role, i));
                //set the spy locations to be shared later
                if(role == 0) spyLocations.Add(i);
            }

        }

        public int Play()
        {
            int totalRnd = 0;
            int captain = 0;
            int mission = 0;
            List<bool> votes = new List<bool>();

            for (int i = 0; i < positions.Count; i++)
            {
                // send spy locations to spies and merlin
                int role = positions[i];
                if(role == 0 || role == 2) players[i].LocateSpies(spyLocations);
            }

            captain = totalRnd % gameSize;

            while (result < 0)
            {
                
                //choose team
                gamestate.SetTeam(players[captain].ChooseTeam(gamestate, teamSize[mission]));
                bool lastvote;
                int numPass = 0;
                int missionFails = 0;
                //vote
                for (int i = 0; i < players.Count; i++)
                {
                    lastvote = players[i].CastVote(gamestate);
                    if (lastvote)
                    {
                        numPass++;
                    }
                    votes.Add(lastvote);
                }

                gamestate.SetVotes(votes);

                if ((float)numPass > (float)gameSize / 2)
                {
                    //go on mission
                    foreach (int member in gamestate.GetCurrentTeam())
                    {
                        if(spyLocations.Contains(member))
                        {
                            int fail = players[member].MissionPlay(gamestate);
                            missionFails += fail;
                        }
                        
                    }
                    if (missionFails >= failsReq[mission])
                    {
                        // The mission failed
                        gamestate.SetMissionResult(0);
                        if (gamestate.GetFailCount() == 3)
                        {
                            //spies win
                            result = 0;
                            gamestate.SetFinalResult(result);
                            return result;
                        }
                    }
                    else
                    {
                        // The mission passed
                        gamestate.SetMissionResult(1);
                        if (gamestate.GetSuccessCount() == 3)
                        {
                            //Good guys win 3
                            //choose merlin
                            int merlinPick = players[positions.IndexOf(0)].PickMerlin(gamestate);
                            gamestate.SetMerlinPick(merlinPick);
                            if (merlinPick == positions.IndexOf(2))
                            {
                                //spies picked merlin and win
                                result = 0;
                                gamestate.SetFinalResult(result);
                                return result;
                            }
                            else
                            {
                                //good guys win
                                result = 1;
                                gamestate.SetFinalResult(result);
                                return result;
                            }
                            
                        }
                    }

                    if (result < 0)
                    {
                        //the game hasn't ended
                        totalRnd++;
                        captain = totalRnd % gameSize;
                        mission++;
                        gamestate.NextRound();
                    }  
                }
                else
                {
                    //vote didn't pass
                    if(gamestate.GetCurrentRound() == 4)
                    {
                        //spies win
                        result = 0;
                        gamestate.SetFinalResult(result);
                        return result;
                    }
                    else
                    {
                        totalRnd++;
                        captain = totalRnd % gameSize;
                        gamestate.NextRound();
                    }

                }
                


                votes.Clear();

            }

            return result;
        }

        public string ShowOutput()
        {
            string json = new JavaScriptSerializer().Serialize(gamestate.GetMissionHist());
            json = json + new JavaScriptSerializer().Serialize(positions);
            return new JavaScriptSerializer().Serialize(gamestate);
        }



        private void SetFileStream(FileStream fs)
        {
            dumpfs = fs;
        }

        private void RecordGame()
        {
            // make sure we have a game result
            if (result < 0) return;

            switch (dumpLocation)
            {
                default:
                    // write the output to a file in the location set

                    break;
            }

        }

        private List<int> Shuffle(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}