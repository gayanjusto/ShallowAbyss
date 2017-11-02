using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    public class ApplicationDataReader<T> where T : class
    {
        public void SaveDataAsync(T dataToSave, string pathToData)
        {
            try
            {
                Thread trSave = new Thread(() =>
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    FileStream fileStream = File.Open(pathToData, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

                    bf.Serialize(fileStream, dataToSave);

                    fileStream.Close();
                });

                trSave.Start();
            }
            catch
            {
                SaveData(dataToSave, pathToData);
            }
         
        }

        public void SaveData(T dataToSave, string pathToData)
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fileStream = File.Open(pathToData, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            bf.Serialize(fileStream, dataToSave);

            fileStream.Close();
        }

        public T LoadData(string pathToData)
        {
            if (File.Exists(pathToData))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileStream = File.Open(pathToData, FileMode.Open);
                var data = (T)bf.Deserialize(fileStream);
                fileStream.Close();
                return data;
            }

            return null;
        }
    }
}
