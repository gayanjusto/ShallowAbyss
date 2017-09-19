using Assets.Scripts.Dictionaries;
using Assets.Scripts.Enums;
using Assets.Scripts.Tools;
using System;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class EnemySpawnerService
    {
        static int _maxEnemiesTypes
        {
            get
            {
                return (Enum.GetValues(typeof(EnemyTypeEnum)) as EnemyTypeEnum[]).Length - 1;
                
            }
        }

        public static EnemyTypeEnum GetEnemyTypeToSpawn(int currentDifficult)
        {
            //get enemy type
            EnemyTypeEnum enemyTypeToSpawn = (EnemyTypeEnum)RandomValueTool.GetRandomValue(0, _maxEnemiesTypes);

            //if enemy type not for current level
            if (!EnemyTypeLevel.EnemyIsAtLevel(enemyTypeToSpawn, currentDifficult))
            {
                return GetEnemyTypeToSpawn(currentDifficult);
            }

            return enemyTypeToSpawn;
        }

        public static int GetEnemyKind(EnemyTypeEnum enemyType, int maxVal, int currentLevel)
        {
            int spawnKindVal = RandomValueTool.GetRandomValue(0, maxVal);

            if (enemyType == EnemyTypeEnum.Standard)
            {
                if(EnemyKindLevel.GetStandardKindsLevel()[spawnKindVal] > currentLevel)
                {
                    return GetEnemyKind(enemyType, maxVal, currentLevel);
                }
                return spawnKindVal;

            }

            if (enemyType == EnemyTypeEnum.Light)
            {
                if (EnemyKindLevel.GetLightKindsLevel()[spawnKindVal] > currentLevel)
                {
                    return GetEnemyKind(enemyType, maxVal, currentLevel);
                }
                return spawnKindVal;

            }

            if (enemyType == EnemyTypeEnum.Charger)
            {
                if (EnemyKindLevel.GetChargerKindsLevel()[spawnKindVal] > currentLevel)
                {
                    return GetEnemyKind(enemyType, maxVal, currentLevel);
                }
                return spawnKindVal;

            }

            if (enemyType == EnemyTypeEnum.Heavy)
            {
                if (EnemyKindLevel.GetHeavyKindsLevel()[spawnKindVal] > currentLevel)
                {
                    return GetEnemyKind(enemyType, maxVal, currentLevel);
                }
                return spawnKindVal;

            }
            return spawnKindVal;
        }
    }
}
