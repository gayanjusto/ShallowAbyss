using Assets.Scripts.Enums;
using Assets.Scripts.Managers.Enemy;
using Assets.Scripts.Managers.Enemy.Spawners;
using UnityEngine;

namespace Assets.Scripts.Resolvers.Enemy
{
    public class EnemySpawnerResolver : MonoBehaviour
    {

        public EnemySpawnManager ResolveEnemySpawn(EnemyTypeEnum enemyType)
        {
            if (enemyType == EnemyTypeEnum.Charger)
                return FindObjectOfType<ChargerSpawnManager>();
            if (enemyType == EnemyTypeEnum.Heavy)
                return FindObjectOfType<HeavySpawnManager>();
            if (enemyType == EnemyTypeEnum.Light)
                return FindObjectOfType<LightSpawnManager>();
            if (enemyType == EnemyTypeEnum.Standard)
                return FindObjectOfType<StandardSpawnManager>();
            if (enemyType == EnemyTypeEnum.Trapper)
                return FindObjectOfType<TrapperSpawnManager>();

            return null;
        }
    }
}
