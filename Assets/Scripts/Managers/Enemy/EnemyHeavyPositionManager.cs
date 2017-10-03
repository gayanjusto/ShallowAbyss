using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class EnemyHeavyPositionManager : BaseEnemyPositionManager
    {
        public override void SendObjectToPool()
        {
            base.enemySpawnerManager.currentAmountHeavyEnemyInScene--;
            base.SendObjectToPool();
        }
    }
}
