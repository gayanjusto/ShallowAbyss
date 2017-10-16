
using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Player;
using UnityEngine;
using System.Linq;
namespace Assets.Scripts.Services
{
    public static class HighScoreService
    {
        static string highScoreDataFilePath
        {
            get
            {
                return Application.persistentDataPath + "/highScore.dat";
            }
        }

        public static void SaveNewScore(int score)
        {
            var appReader = new ApplicationDataReader<HighScoreData>();
            var highScoreData = appReader.LoadData(highScoreDataFilePath);

            if (highScoreData != null)
            {
                var scores = highScoreData.GetScores();

                for (int i = 0; i < scores.Length; i++)
                {
                    if(score > scores[i])
                    {
                        scores[i] = score;
                        break;
                    }
                }

                highScoreData.SetScores(scores.OrderByDescending(x => x).ToArray());
            }else
            {
                var scores = new int[10];
                scores[0] = score;
                highScoreData = new HighScoreData();
                highScoreData.SetScores(scores);
            }

            appReader.SaveData(highScoreData, highScoreDataFilePath);
        }

        public static int GetHighestScore()
        {
            var appReader = new ApplicationDataReader<HighScoreData>();
            return appReader.LoadData(highScoreDataFilePath).GetHighestScore();
        }
    }
}
