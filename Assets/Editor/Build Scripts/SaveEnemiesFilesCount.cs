using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SaveEnemiesFilesCount
{

    //[MenuItem("Build/With Resource Count")]
    static SaveEnemiesFilesCount()
    {
        var standardEnemyCount = GetFilesCount("Standard");
        File.WriteAllText(Application.dataPath + "/Resources/EnemyTypeCount/standard.txt", standardEnemyCount.ToString());

        var lightEnemyCount = GetFilesCount("Light");
        File.WriteAllText(Application.dataPath + "/Resources/EnemyTypeCount/light.txt", lightEnemyCount.ToString());

        var chargerEnemyCount = GetFilesCount("Charger");
        File.WriteAllText(Application.dataPath + "/Resources/EnemyTypeCount/charger.txt", chargerEnemyCount.ToString());

        var heavyEnemyCount = GetFilesCount("Heavy");
        File.WriteAllText(Application.dataPath + "/Resources/EnemyTypeCount/heavy.txt", heavyEnemyCount.ToString());

        AssetDatabase.Refresh();
    }

    static int GetFilesCount(string enemyFolderPath)
    {
        var files = Directory.GetFiles(string.Format("{0}/Resources/Prefabs/Enemies/{1}",
            Application.dataPath, enemyFolderPath), "*.prefab", SearchOption.AllDirectories);

        if(files == null)
        {
            return 0;
        }

        //EnemyCount is zero based, so we have to deduce in 1
        return files.Length - 1;
    }
}
