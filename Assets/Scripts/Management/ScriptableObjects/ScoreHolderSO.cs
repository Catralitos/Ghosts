using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Score Holder")]
    public class ScoreHolderSO : ScriptableObject
    {
        public int CurrentPacmanScore { get; private set; }
        public int CurrentGhostScore { get; private set; }
        public List<int> PastScores { get; private set; }

        public void Init()
        {
            CurrentPacmanScore = 0;
            //The game has:
            //246 pellets worth 10 points
            //4 power pellets worth 50 points
            //And at 4 opportunities to eat 4 ghosts, the first is 200 and doubles with each ghost
            //That's the maximum score (sans bonus fruits) you can get in a Pacman maze
            CurrentGhostScore = 246 * 10 + 4 * 50 + (200 + 400 + 800 + 1600);
        }
        
        public void DecreaseScore(int amount)
        {
            CurrentPacmanScore += amount;
            CurrentGhostScore -= amount;
        }

        public void IncreaseScore(int amount)
        {
            CurrentGhostScore += amount;
        }
        
        public void EndGame()
        {
            PastScores ??= new List<int>();
            PastScores.Add(CurrentGhostScore);
            SceneManager.LoadScene("HighScoreScreen");
        }
        
        public int GetLastScore()
        {
            return PastScores[^1];
        }

        public List<int> GetHighScores(int amount)
        {
            int toReturn = amount > PastScores.Count ? PastScores.Count : amount;
            return PastScores.OrderByDescending(i => i).ToList().GetRange(0, toReturn);
        }
    }
}