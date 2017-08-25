using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerStatusManager : MonoBehaviour
    {
        const string playerDataFilePath = "/playerscore.dat";
        public static PlayerStatusManager instance;

        private void Awake()
        {
            if (instance)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
                instance = this;
            }
        }

        public void SavePlayerStatus(float score, int lifeBuff, int shieldBuff)
        {
            BinaryFormatter bf = new BinaryFormatter();

            //Load currentScore with previously saved score
            PlayerStatusData playerData = LoadPlayerStatus();

            FileStream fileStream = File.Open(Application.persistentDataPath + playerDataFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            PlayerStatusData scoreData = new PlayerStatusData(Mathf.FloorToInt(score), lifeBuff, shieldBuff);

            scoreData.score += playerData.score;

            bf.Serialize(fileStream, scoreData);

            fileStream.Close();
        }

        public void SavePlayerStatus(PlayerStatusData playerStatusData)
        {
            BinaryFormatter bf = new BinaryFormatter();

            //Load currentScore with previously saved score

            FileStream fileStream = File.Open(Application.persistentDataPath + playerDataFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            bf.Serialize(fileStream, playerStatusData);

            fileStream.Close();
        }

        public PlayerStatusData LoadPlayerStatus()
        {

            var appDataReader = new ApplicationDataReader<PlayerStatusData>();
            var loadedPlayerData = appDataReader.LoadPlayerStatus(playerDataFilePath);

            if (loadedPlayerData != null)
            {
                return loadedPlayerData;
            }
            else
            {
                PlayerStatusData playerData = new PlayerStatusData(0, 0, 0);

                playerData.shipsOwnedIds = new List<int>();

                //player always owns the first ship by default
                playerData.shipsOwnedIds.Add(1);

                appDataReader.SaveData(playerData, playerDataFilePath);
                SavePlayerStatus(playerData);

                return playerData;
            }
        }
    }


}
