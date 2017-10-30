using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public int currentSpawnedAmount;
        public int maxSpawnAmount;

        protected EnemyPoolManager enemyPoolManager;

        private void Start()
        {
            enemyPoolManager = FindObjectOfType<EnemyPoolManager>();
        }
        public virtual void SendToPool(Transform poolToSend, GameObject enemy)
        {
            currentSpawnedAmount--;
            enemyPoolManager.SendObjectToPool(poolToSend, enemy);
        }

        public virtual bool CanSpawnEnemy()
        {
            return currentSpawnedAmount < maxSpawnAmount;
        }

        public virtual void IncreaseSpawnAmount(int amount)
        {
            currentSpawnedAmount+= amount;
        }

        public int GetAmountToSpawn()
        {
            return maxSpawnAmount - currentSpawnedAmount;
        }
    }
}
