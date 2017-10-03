using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class TutorialDataService
    {
        static string tutorialDataPath
        {
            get
            {
                return Application.persistentDataPath + "/tutData.dat";
            }
        }
         
        static ApplicationDataReader<TutorialData> _dataReader;
        static ApplicationDataReader<TutorialData> DataReader
        {
            get
            {
                if(_dataReader == null)
                {
                    _dataReader = new ApplicationDataReader<TutorialData>();
                }

                return _dataReader;
            }
        }

        public static TutorialData GetTutorialData()
        {
            var tutorialData = DataReader.LoadData(tutorialDataPath);
            if(tutorialData == null)
            {
                tutorialData = new TutorialData();
            }

            return tutorialData;
        }

        public static void SaveTutorialData(TutorialData data)
        {
            DataReader.SaveData(data, tutorialDataPath);
        }
    }
}
