using UnityEngine;

namespace Assets.Scripts.Managers.Enemy.Spawners
{
    public class HeavySpawnManager : EnemySpawnManager
    {
        public float timeToSpawnHeavy;
        float heavySpawnTickTime;

        private void Update()
        {
            heavySpawnTickTime += Time.deltaTime;
        }

        public override bool CanSpawnEnemy()
        {
            return currentSpawnedAmount < maxSpawnAmount
                && heavySpawnTickTime >= timeToSpawnHeavy;
        }

        public override void SendToPool(Transform poolToSend, GameObject enemy)
        {
            heavySpawnTickTime = 0;
            base.SendToPool(poolToSend, enemy);
        }
    }
}
