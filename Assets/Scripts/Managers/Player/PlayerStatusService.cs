using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public static class PlayerStatusService
    {
        static string playerDataFilePath
        {
            get
            {
                return Application.persistentDataPath + "/playerscore.dat";
            }
        }

        static PlayerStatusData _playerDataInstance;

        public static void SavePlayerStatus(PlayerStatusData playerStatusData = null)
        {
            var appDataReader = new ApplicationDataReader<PlayerStatusData>();

            if(playerStatusData == null)
            {
                playerStatusData = appDataReader.LoadData(playerDataFilePath);
            }
            appDataReader.SaveDataAsync(playerStatusData, playerDataFilePath);
        }

        static void SaveData(string appDataPath, PlayerStatusData playerStatusData = null)
        {
            BinaryFormatter bf = new BinaryFormatter();

            //Load currentScore with previously saved score

            FileStream fileStream = File.Open(appDataPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            if (playerStatusData == null)
                bf.Serialize(fileStream, _playerDataInstance);
            else
            {
                _playerDataInstance = playerStatusData;
                bf.Serialize(fileStream, playerStatusData);
            }

            fileStream.Close();
        }

        public static PlayerStatusData LoadPlayerStatus()
        {
            var appDataReader = new ApplicationDataReader<PlayerStatusData>();

            if (!HasPlayerDataFile())
            {

                PlayerStatusData playerData = CreateInitialPlayerStatus(appDataReader);
                _playerDataInstance = playerData;
                return _playerDataInstance;
            }

            if (_playerDataInstance == null)
            {
                _playerDataInstance = appDataReader.LoadData(playerDataFilePath);
            }

            return _playerDataInstance;
        }

        static PlayerStatusData CreateInitialPlayerStatus(ApplicationDataReader<PlayerStatusData> appDataReader)
        {
            PlayerStatusData playerData = new PlayerStatusData(0, 1, 1);

            //player always owns the first ship by default
            playerData.GetOwnedShipsIDs().Add(1);

            playerData.IncreaseDashUpgrade();

            appDataReader.SaveDataAsync(playerData, playerDataFilePath);
            SavePlayerStatus(playerData);

            return playerData;
        }

        public static bool HasPlayerDataFile()
        {
            return File.Exists(playerDataFilePath);
        }
    }


}
