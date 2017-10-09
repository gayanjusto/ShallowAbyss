using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class PlayerTimeExperienceDataService
    {
        static string playerDataFilePath
        {
            get
            {
                return Application.persistentDataPath + "/playerTimeExperience.dat";
            }
        }

        public static void SaveTimeExperience(float timeExperience)
        {
            var appReader = new ApplicationDataReader<PlayerExperienceData>();
            var data = appReader.LoadData(playerDataFilePath);

            if (data == null)
            {
                data = new PlayerExperienceData();
            }
            data.InsertGameDuration(timeExperience);
            appReader.SaveDataAsync(data, playerDataFilePath);
        }

        public static PlayerExperienceData LoadTimeExperience()
        {
            var appReader = new ApplicationDataReader<PlayerExperienceData>();
            return appReader.LoadData(playerDataFilePath);
        }
    }
}
