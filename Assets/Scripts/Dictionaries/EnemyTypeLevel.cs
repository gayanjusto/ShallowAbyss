using Assets.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Dictionaries
{
    public class EnemyTypeLevel
    {
        static Dictionary<EnemyTypeEnum, int> _enemyLevel;

        public static Dictionary<EnemyTypeEnum, int> GetAllEnemiesLevels()
        {
            if(_enemyLevel == null)
            {
                _enemyLevel = new Dictionary<EnemyTypeEnum, int>();

                _enemyLevel.Add(EnemyTypeEnum.Standard, 0);
                _enemyLevel.Add(EnemyTypeEnum.Light, 5);
                _enemyLevel.Add(EnemyTypeEnum.Charger, 8);
                _enemyLevel.Add(EnemyTypeEnum.Heavy, 12);
                _enemyLevel.Add(EnemyTypeEnum.Trapper, 30);

            }

            return _enemyLevel;
        }

        public static bool EnemyIsAtLevel(EnemyTypeEnum enemyType, int level)
        {
            var result = GetAllEnemiesLevels()[enemyType] <= level; ;
            return result;
        }
    }
}
