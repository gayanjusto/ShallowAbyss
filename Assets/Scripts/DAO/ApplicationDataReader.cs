using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    public class ApplicationDataReader<T> where T : class
    {
        public void SaveData(T dataToSave, string pathToData)
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fileStream = File.Open(Application.persistentDataPath + pathToData, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            bf.Serialize(fileStream, dataToSave);

            fileStream.Close();
        }

        public T LoadData(string pathToData)
        {
            if (File.Exists(Application.persistentDataPath + pathToData))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileStream = File.Open(Application.persistentDataPath + pathToData, FileMode.Open);
                var data = (T)bf.Deserialize(fileStream);
                fileStream.Close();
                return data;
            }

            return null;
        }
    }
}
