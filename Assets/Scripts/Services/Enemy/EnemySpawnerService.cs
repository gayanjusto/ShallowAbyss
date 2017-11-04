using Assets.Scripts.Dictionaries;
using Assets.Scripts.Entities.Enemy;
using Assets.Scripts.Enums;
using Assets.Scripts.Resolvers.Enemy;
using Assets.Scripts.Services.FireBase;
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

        public static EnemySpawnData GetEnemyTypeToSpawn(int currentDifficult, EnemySpawnerResolver enemySpawnerResolver)
        {
            //get enemy type
            EnemyTypeEnum enemyTypeToSpawn = (EnemyTypeEnum)RandomValueTool.GetRandomValue(0, _maxEnemiesTypes);
            var enemySpawn = enemySpawnerResolver.ResolveEnemySpawn(enemyTypeToSpawn);

            //if enemy type not for current level
            if (!EnemyTypeLevel.EnemyIsAtLevel(enemyTypeToSpawn, currentDifficult)
                || !enemySpawn.CanSpawnEnemy())
            {
                return GetEnemyTypeToSpawn(currentDifficult, enemySpawnerResolver);
            }

            return new EnemySpawnData(enemyTypeToSpawn, enemySpawn);
        }

        public static int GetEnemyKind(EnemyTypeEnum enemyType, int maxVal, int currentLevel)
        {
            int spawnKindVal = RandomValueTool.GetRandomValue(0, maxVal);

            if (enemyType == EnemyTypeEnum.Standard)
            {
                if (EnemyKindLevel.GetStandardKindLevel()[spawnKindVal] > currentLevel)
                {
                    return GetEnemyKind(enemyType, maxVal, currentLevel);
                }
                return spawnKindVal;

            }

            if (enemyType == EnemyTypeEnum.Light)
            {
                if (EnemyKindLevel.GetLightKindLevel()[spawnKindVal] > currentLevel)
                {
                    return GetEnemyKind(enemyType, maxVal, currentLevel);
                }
                return spawnKindVal;

            }

            if (enemyType == EnemyTypeEnum.Charger)
            {
                if (EnemyKindLevel.GetChargerKindLevel()[spawnKindVal] > currentLevel)
                {
                    return GetEnemyKind(enemyType, maxVal, currentLevel);
                }
                return spawnKindVal;

            }

            if (enemyType == EnemyTypeEnum.Heavy)
            {
                if (EnemyKindLevel.GetHeavyKindLevel()[spawnKindVal] > currentLevel)
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

            AnalyticsService.LogEvent("Average_Time", "Average_Time", averageTime);

            //Expert II
            if (averageTime > timeThreshold * 15)
            {
                return 30;
            }

            //Expert
            if (averageTime > timeThreshold * 10)
            {
                return 20;
            }

            //Hard
            if (averageTime > timeThreshold * 7)
            {
                return 10;
            }

            //Normal
            if (averageTime > timeThreshold * 2)
            {
                return 5;
            }

            //Beginner
            return 2;
        }
    }
}
