using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class EnemyPoolManager : MonoBehaviour
    {
        public EnemySpawnerManager enemySpawnerManager;

        private void Start()
        {
            enemySpawnerManager = GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerManager>();
        }
        public void SendObjectToPool(Transform poolToSend, GameObject obj)
        {
            //if game object equals to heavy enemy, decrease the amount of heavy enemy spawned
            if (obj.name.Contains("Heavy"))
            {
                enemySpawnerManager.currentAmountHeavyEnemyInScene--;
            }

            enemySpawnerManager.amountSpawnedEnemies--;
            obj.transform.parent = poolToSend;

            //set all children to inactive
            foreach (Transform item in obj.transform)
            {
                item.gameObject.SetActive(false);
            }
            obj.SetActive(false);
        }
    }
}
