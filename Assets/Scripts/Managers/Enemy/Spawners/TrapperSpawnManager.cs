using UnityEngine;

namespace Assets.Scripts.Managers.Enemy.Spawners
{
    public class TrapperSpawnManager : EnemySpawnManager
    {
        public float timeToSpawnTrapper;
        float trapperSpawnTimeTick;

        private void Update()
        {
            trapperSpawnTimeTick += Time.deltaTime;
        }

        public override bool CanSpawnEnemy()
        {
            return currentSpawnedAmount < maxSpawnAmount
                && trapperSpawnTimeTick >= timeToSpawnTrapper;
        }

        public override void SendToPool(Transform poolToSend, GameObject enemy)
        {
            trapperSpawnTimeTick = 0;
            base.SendToPool(poolToSend, enemy);
        }
    }
}
