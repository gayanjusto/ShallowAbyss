using Assets.Scripts.Dictionaries;
using Assets.Scripts.Enums;
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
        public int intervalToSpawnHeavyEnemy;
        public int gameMaxAmountEnemies;
        Vector3 screenBottomEdge;

        public float leftSpawnEdge;
        public float rightSpawnEdge;
        public int currentLevelDifficult;
        public float timeToChangeDifficult;
        public float currentDifficultTime;
        public float heavyEnemySpawnTime;
        public bool hasSpawnedHeavyEnemy;

        Vector3[] spawnedPositions;
        ScoreCounterManager scoreManager;

        private void Start()
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreCounterManager>();

            screenBottomEdge = ScreenPositionService.GetBottomEdge(Camera.main);
            leftSpawnEdge = screenBottomEdge.x + 3;
            rightSpawnEdge = ScreenPositionService.GetRightEdge(Camera.main).x;
            currentDifficultTime = timeToChangeDifficult;
            currentMaxAmountEnemies = amountEnemiesBaseLevel;
            spawnedPositions = new Vector3[currentMaxAmountEnemies];
            heavyEnemySpawnTime = intervalToSpawnHeavyEnemy;
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
            if (hasSpawnedHeavyEnemy)
            {
                return;
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

        Vector3 GenerateUniquePositionToSpawn(int index)
        {
            Vector3 newPos = new Vector3(
                RandomValueTool.GetRandomValue
                (
                    (int)ScreenPositionService.GetLeftEdge(Camera.main).x, (int)ScreenPositionService.GetRightEdge(Camera.main).x
                ),
                screenBottomEdge.y - RandomValueTool.GetRandomValue(5, 15));



            for (int i = 0; i < spawnedPositions.Length; i++)
            {
                if (newPos == spawnedPositions[i])
                {
                    return GenerateUniquePositionToSpawn(index);
                }
            }

            spawnedPositions[index] = newPos;
            return newPos;
        }

        void InstantiateEnemiesFromPool(string enemyType, int amountToSpawn)
        {

            int amountSpawned = 0;

            foreach (Transform enemy in enemyPool.transform)
            {
                Vector3 newPos = GenerateUniquePositionToSpawn(amountToSpawn);

                if (!enemy.gameObject.active && enemy.name.Contains(enemyType))
                {
                    enemy.parent = null;
                    enemy.position = newPos;
                    enemy.gameObject.SetActive(true);
                    enemy.transform.FindChild("CollisionCheck").gameObject.SetActive(true);

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
