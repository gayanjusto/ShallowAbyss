using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerStatusManager : MonoBehaviour
    {
        const string scoreFilePath = "/playerscore.dat";

        public void SavePlayerStatus(float score, int lifeBuff, int shieldBuff)
        {
            BinaryFormatter bf = new BinaryFormatter();

            //Load currentScore with previously saved score
            PlayerStatusData playerData = LoadPlayerStatus();

            FileStream fileStream = File.Open(Application.persistentDataPath + scoreFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            PlayerStatusData scoreData = new PlayerStatusData(Mathf.FloorToInt(score), lifeBuff, shieldBuff);

            scoreData.score += playerData.score;

            bf.Serialize(fileStream, scoreData);

            fileStream.Close();
        }

        public void SavePlayerStatus(PlayerStatusData playerStatusData)
        {
            BinaryFormatter bf = new BinaryFormatter();

            //Load currentScore with previously saved score

            FileStream fileStream = File.Open(Application.persistentDataPath + scoreFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            bf.Serialize(fileStream, playerStatusData);

            fileStream.Close();
        }

        public PlayerStatusData LoadPlayerStatus()
        {
            if (File.Exists(Application.persistentDataPath + scoreFilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileStream = File.Open(Application.persistentDataPath + scoreFilePath, FileMode.Open);
                PlayerStatusData playerData = (PlayerStatusData)bf.Deserialize(fileStream);
                fileStream.Close();
                return playerData;
            }

            return new PlayerStatusData(0,0,0);
        }
    }

    [Serializable]
    public class PlayerStatusData
    {
        public PlayerStatusData(int score, int lifeBuff, int shieldBuff)
        {
            this.score = score;
            this.lifeBuff = lifeBuff;
            this.shieldBuff = shieldBuff;
            this.shipsOwnedIds = new List<int>();
        }
     
        public int score;
        public int lifeBuff;
        public int shieldBuff;
        public List<int> shipsOwnedIds;
    }
}
