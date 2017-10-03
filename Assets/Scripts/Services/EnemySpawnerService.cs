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
                if (EnemyKindLevel.GetStandardKindsLevel()[spawnKindVal] > currentLevel)
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

        public static int SetInitialDifficult(float timeThreshold)
        {
            var timeExperience = PlayerTimeExperienceDataService.LoadTimeExperience();
            float averageTime = 0;
            if (timeExperience != null)
            {
                averageTime = timeExperience.GetAverageGameTime();
            }

            //Beginner
            if (timeThreshold > averageTime)
            {
                return 5;
            }

            //Normal
            if (averageTime > timeThreshold * 2)
            {
                return 7;
            }

            //Hard
            if (averageTime > timeThreshold * 5)
            {
                return 10;
            }

            //Expert
            if (averageTime > timeThreshold * 10)
            {
                return 15;
            }

            //Expert
            if (averageTime > timeThreshold * 15)
            {
                return 20;
            }

            return 5;
        }
    }
}
