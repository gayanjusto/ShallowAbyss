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

        static PlayerStatusData _playerDataInstance;
        public static PlayerStatusData PlayerDataInstance
        {
            get
            {
                if(_playerDataInstance == null)
                {
                    _playerDataInstance = instance.LoadPlayerStatus();
                }

                return _playerDataInstance;
            }
        }

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

   
        public void SavePlayerStatus(PlayerStatusData playerStatusData)
        {
            BinaryFormatter bf = new BinaryFormatter();

            //Load currentScore with previously saved score

            FileStream fileStream = File.Open(Application.persistentDataPath + playerDataFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            bf.Serialize(fileStream, playerStatusData);

            fileStream.Close();

            PlayerStatusManager._playerDataInstance = playerStatusData;
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
                PlayerStatusData playerData = CreateInitialPlayerStatus(appDataReader);

                return playerData;
            }
        }

        PlayerStatusData CreateInitialPlayerStatus(ApplicationDataReader<PlayerStatusData> appDataReader)
        {
            PlayerStatusData playerData = new PlayerStatusData(999999999, 2, 1);

            //player always owns the first ship by default
            playerData.GetOwnedShipsIDs().Add(1);

            playerData.IncreaseDashUpgrade();

            appDataReader.SaveData(playerData, playerDataFilePath);
            SavePlayerStatus(playerData);

            return playerData;
        }

        public static bool HasPlayerDataFile()
        {
            return File.Exists(Application.persistentDataPath + playerDataFilePath);
        }
    }


}
