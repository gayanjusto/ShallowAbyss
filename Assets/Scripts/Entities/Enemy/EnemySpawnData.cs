using Assets.
    Scripts.Enums;
using Assets.Scripts.Managers.Enemy;

namespace Assets.Scripts.Entities.Enemy
{
    public struct EnemySpawnData
    {
        public EnemySpawnData(EnemyTypeEnum enemyType, EnemySpawnManager enemySpawnManager)
        {
            this.enemyType = enemyType;
            this.enemySpawnManager = enemySpawnManager;
        }
        public EnemyTypeEnum enemyType;
        public EnemySpawnManager enemySpawnManager;
    }
}
