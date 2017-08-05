using Assets.Scripts.Dictionaries;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemySpawnerManager : MonoBehaviour
    {

        public GameObject enemyPool;
        public int amountSpawnedEnemies;
        public int amountEnemiesBaseLevel;
        public int currentMaxAmountEnemies;
        public int gameMaxAmountEnemies;

        public int currentLevelDifficult;
        public float timeToChangeDifficult;
        public float currentDifficultTime;
        public float heavyEnemySpawnTime;
        public int maxAmountHeavyEnemyInScene;
        public int currentAmountHeavyEnemyInScene;


        Vector3[] spawnedPositions;
        ScoreCounterManager scoreManager;

        private void Start()
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreCounterManager>();

            currentDifficultTime = timeToChangeDifficult;
            currentMaxAmountEnemies = amountEnemiesBaseLevel;
            spawnedPositions = new Vector3[gameMaxAmountEnemies];
        }

        private void Update()
        {
            currentDifficultTime -= Time.deltaTime;

            //At each X seconds, increase the difficult
            ChangeDifficult();

            //canSpawnEnemy
            if (amountSpawnedEnemies <= currentMaxAmountEnemies)
            {
                SpawnEnemies();
            }
        }

        void ChangeDifficult()
        {
            if (currentDifficultTime <= 0)
            {
                currentLevelDifficult++;
                currentDifficultTime = timeToChangeDifficult;

                //Check if can increase max of enemies
                ChangeMaxAmountEnemies();
            }
        }

        void ChangeMaxAmountEnemies()
        {
            if (currentLevelDifficult % 10 == 0 && (currentMaxAmountEnemies + 1) < gameMaxAmountEnemies)
            {
                currentMaxAmountEnemies++;
            }
        }

        void SpawnEnemies()
        {
            int possibleAmountToSpawn = currentMaxAmountEnemies - amountSpawnedEnemies;

            // If for some reason it didn't increase the spawned amount, set it to max
            if (possibleAmountToSpawn == 0)
            {
                amountSpawnedEnemies = currentMaxAmountEnemies;
                return;
            }

            int amountEnemiesToSpawn = GetAmountEnemiesToSpawn(possibleAmountToSpawn);
            possibleAmountToSpawn -= amountEnemiesToSpawn;
            EnemyTypeEnum enemyTypeToSpawn = GetEnemyTypeToSpawn(currentLevelDifficult);

            //if enemy == heavy, check if there's already one in scene
            if (enemyTypeToSpawn == EnemyTypeEnum.Heavy)
            {
                //Spawn only one heavy at time
                amountEnemiesToSpawn = 1;

                //if it exceeds the max amount of heavy enemies in the scene we ignore the spawn of this enemy
                if (currentAmountHeavyEnemyInScene + 1 > maxAmountHeavyEnemyInScene)
                {
                    return;
                }

                currentAmountHeavyEnemyInScene++;
            }

            InstantiateEnemiesFromPool(enemyTypeToSpawn.ToString(), amountEnemiesToSpawn);
        }

        EnemyTypeEnum GetEnemyTypeToSpawn(int currentDifficult)
        {
            //get enemy type
            int maxEnemiesTypes = (Enum.GetValues(typeof(EnemyTypeEnum)) as EnemyTypeEnum[]).Length;
            EnemyTypeEnum enemyTypeToSpawn = (EnemyTypeEnum)RandomValueTool.GetRandomValue(0, maxEnemiesTypes);

            //if enemy type not for current level
            if (!EnemyLevel.EnemyIsAtLevel(enemyTypeToSpawn, currentDifficult))
            {
                return GetEnemyTypeToSpawn(currentDifficult);
            }

            return enemyTypeToSpawn;
        }

        int GetAmountEnemiesToSpawn(int maxPossibleAmount)
        {
            var amountValue = RandomValueTool.GetRandomValue(0, maxPossibleAmount);

            return amountValue;
        }

        void InstantiateEnemiesFromPool(string enemyType, int amountToSpawn)
        {
            int amountSpawned = 0;

            foreach (Transform enemy in enemyPool.transform)
            {
                if (!enemy.gameObject.active && enemy.name.Contains(enemyType))
                {
                    enemy.parent = null;
                    enemy.gameObject.SetActive(true);

                    //set all child to active
                    foreach (Transform item in enemy)
                    {
                        item.gameObject.SetActive(true);
                    }

                    enemy.GetComponent<IEnemySpawnInitialConfiguration>().SetInitialSpawnConfiguration();
                    amountSpawnedEnemies++;
                    amountSpawned++;
                }

                if (amountSpawned == amountToSpawn)
                {
                    break;
                }
            }

        }
    }
}
