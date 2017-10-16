using System;
using System.Linq;

namespace Assets.Scripts.Entities.Player
{
    [Serializable]
    public class HighScoreData
    {
        int[] scores;

        public int GetHighestScore()
        {
            return scores.Max();
        }

        public void SetScores(int[] scores)
        {
            this.scores = scores;
        }

        public int[] GetScores()
        {
            return this.scores;
        }
    }
}
