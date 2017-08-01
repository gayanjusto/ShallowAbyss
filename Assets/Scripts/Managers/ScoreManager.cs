using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        const string scoreFilePath = "/playerscore.dat";

        public void SaveScore(float score)
        {
            BinaryFormatter bf = new BinaryFormatter();

            //Load currentScore with previously saved score
            int previousScore = LoadScore();

            FileStream fileStream = File.Open(Application.persistentDataPath + scoreFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            ScoreData scoreData = new ScoreData(Mathf.FloorToInt(score));

            scoreData.score += previousScore;

            bf.Serialize(fileStream, scoreData);

            fileStream.Close();
        }

        public int LoadScore()
        {
            int scoreValue = 0;
            if (File.Exists(Application.persistentDataPath + scoreFilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileStream = File.Open(Application.persistentDataPath + scoreFilePath, FileMode.Open);
                ScoreData scoreData = (ScoreData)bf.Deserialize(fileStream);
                fileStream.Close();

                scoreValue += scoreData.score;
            }

            return scoreValue;
        }
    }

    [Serializable]
    class ScoreData
    {
        public ScoreData(int score)
        {
            this.score = score;
        }
        public int score;
    }
}
