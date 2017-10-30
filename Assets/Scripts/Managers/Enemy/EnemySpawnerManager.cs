using Assets.Scripts.Dictionaries;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Managers.Enemy;
using Assets.Scripts.Resolvers.Enemy;
using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        public EnemySpawnerResolver enemySpawnerResolver;
        public GameObject enemyPool;
        public int amountSpawnedEnemies;
        public int timeExperienceThreshold;
        public int amountEnemiesBaseLevel;
        public int currentMaxAmountEnemies;
        public int intervalLevelForBreathing;
        int realMaxAmout;
        public int amountLevelsToBreath;
        int currentBreathingLevelAmount;

        public int gameMaxAmountEnemies;

        public int currentLevelDifficult;
        public float timeToChangeDifficult;
        public float currentDifficultTime;
        public float trapperSuckerEnemySpawnTime;
        public float trapperEnemySpawnTickTime;
        bool canSpawnTrapper;

        public int maxAmountHeavyEnemyInScene;
        public int currentAmountHeavyEnemyInScene;

        public int maxAmountChargerInScene;
        public int currentAmountChargerEnemyInScene;

        public int maxAmountTrapperSuckerInScene;
        public int currentAmountTrapperSuckerEnemyInScene;


        public short amountStandard, amountLight, amountCharger, amountHeavy, amountTrapper;
        int maxEnemiesTypes;

        private void Start()
        {
            amountEnemiesBaseLevel = EnemySpawnerService.SetInitialDifficult(timeExperienceThreshold);

            maxEnemiesTypes = (Enum.GetValues(typeof(EnemyTypeEnum)) as EnemyTypeEnum[]).Length - 1;

            currentDifficultTime = timeToChangeDifficult;
            currentMaxAmountEnemies = amountEnemiesBaseLevel;

            //These are used with the method: GetMaxAmountOfEnemyType()
            amountStandard = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/standard").text);
            amountLight = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/light").text);
            amountCharger = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/charger").text);
            amountHeavy = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/heavy").text);
            amountTrapper = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/trapper").text);

            StartCoroutine(SpawnEnemyRoutine());
        }

        private void Update()
        {

            currentDifficultTime -= Time.deltaTime;

            //At each X seconds, increase the difficult
            ChangeDifficult();

            ////canSpawnEnemy
            //if (amountSpawnedEnemies < currentMaxAmountEnemies)
            //{
            //    SpawnEnemies();
            //}
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
            if (currentBreathingLevelAmount > 0)
            {
                //if is breathing, keep increasing max amount
                if (currentLevelDifficult % 2 == 0)
                    realMaxAmout += 2;

                currentBreathingLevelAmount--;
                return;
            }
            //Give the player some time to breath by reducing the difficult in 50%
            if (currentLevelDifficult % intervalLevelForBreathing == 0)
            {
                realMaxAmout = currentMaxAmountEnemies;
                currentMaxAmountEnemies /= 2;
                currentBreathingLevelAmount = amountLevelsToBreath;
                return;
            }

            currentMaxAmountEnemies = realMaxAmout != 0 ? realMaxAmout : currentMaxAmountEnemies;
            if (currentLevelDifficult % 2 == 0 && (currentMaxAmountEnemies + 1) < gameMaxAmountEnemies)
            {
                currentMaxAmountEnemies += 2;
                realMaxAmout = currentMaxAmountEnemies;
            }
        }

        void SpawnEnemies()
        {
            int possibleAmountToSpawn = currentMaxAmountEnemies - amountSpawnedEnemies;

            // If for some reason it didn't increase the spawned amount, set it to max
            if (possibleAmountToSpawn <= 0)
            {
                amountSpawnedEnemies = currentMaxAmountEnemies;
                return;
            }

            //Get the type of enemy we'll spawn
            var enemySpawnData = EnemySpawnerService.GetEnemyTypeToSpawn(currentLevelDifficult, enemySpawnerResolver);
            int amountEnemiesToSpawn = 0;

            if (possibleAmountToSpawn > enemySpawnData.enemySpawnManager.GetAmountToSpawn())
                amountEnemiesToSpawn = enemySpawnData.enemySpawnManager.GetAmountToSpawn();
            else
                amountEnemiesToSpawn = GetAmountEnemiesToSpawn(possibleAmountToSpawn);

            possibleAmountToSpawn -= amountEnemiesToSpawn;

            enemySpawnData.enemySpawnManager.IncreaseSpawnAmount(amountEnemiesToSpawn);
            ////if enemy == heavy, check if there's already the max in scene
            //if (enemyTypeToSpawn == EnemyTypeEnum.Heavy && !CanSpawnHeavyEnemy(ref amountEnemiesToSpawn))
            //{
            //    return;
            //}

            ////if enemy == charger, check if there's already the max in scene
            //if (enemyTypeToSpawn == EnemyTypeEnum.Charger && !CanSpawnChargerEnemy(ref amountEnemiesToSpawn))
            //{
            //    return;
            //}

            ////if enemy == charger, check if there's already the max in scene
            //if (enemyTypeToSpawn == EnemyTypeEnum.Trapper && !CanSpawnTrapperEnemy(ref amountEnemiesToSpawn))
            //{
            //    return;
            //}


            //Get the number of the enemy type. E.g: Standard/0 or Standard/1
            string enemyMaxVal = GetMaxAmountOfEnemyType(enemySpawnData.enemyType);

            var enemyKindVal = EnemySpawnerService.GetEnemyKind(enemySpawnData.enemyType, Convert.ToInt32(enemyMaxVal), currentLevelDifficult);

            //Find the enemytype category in the Hierachy. It's contained inside EnemyPool
            Transform enemyCategory = enemyPool.transform.FindChild(string.Format("{0}/{1}", enemySpawnData.enemyType.ToString(), enemyKindVal));


            InstantiateEnemiesFromPool(enemyCategory, amountEnemiesToSpawn);
        }

        string GetMaxAmountOfEnemyType(EnemyTypeEnum enemyTypeToSpawn)
        {
            return this.GetStringFieldValue("amount" + enemyTypeToSpawn.ToString());
        }

        bool CanSpawnTrapperEnemy(ref int amountEnemiesToSpawn)
        {
            if (trapperEnemySpawnTickTime <= trapperSuckerEnemySpawnTime)
            {
                return false;
            }
            else
            {
                canSpawnTrapper = true;
            }
            //Spawn only one trapper at time
            amountEnemiesToSpawn = 1;

            //if it exceeds the max amount of heavy enemies in the scene we ignore the spawn of this enemy
            if (currentAmountTrapperSuckerEnemyInScene + 1 > maxAmountTrapperSuckerInScene)
            {
                return false;
            }

            canSpawnTrapper = false;
            trapperEnemySpawnTickTime = 0;
            currentAmountTrapperSuckerEnemyInScene++;
            return true;
        }

        bool CanSpawnChargerEnemy(ref int amountEnemiesToSpawn)
        {
            //Spawn only one heavy at time
            amountEnemiesToSpawn = 1;

            //if it exceeds the max amount of heavy enemies in the scene we ignore the spawn of this enemy
            if (currentAmountChargerEnemyInScene + 1 > maxAmountChargerInScene)
            {
                return false;
            }

            currentAmountChargerEnemyInScene++;
            return true;
        }

        bool CanSpawnHeavyEnemy(ref int amountEnemiesToSpawn)
        {
            //Spawn only one heavy at time
            amountEnemiesToSpawn = 1;

            //if it exceeds the max amount of heavy enemies in the scene we ignore the spawn of this enemy
            if (currentAmountHeavyEnemyInScene + 1 > maxAmountHeavyEnemyInScene)
            {
                return false;
            }

            currentAmountHeavyEnemyInScene++;
            return true;
        }

        int GetAmountEnemiesToSpawn(int maxPossibleAmount)
        {
            var amountValue = RandomValueTool.GetRandomValue(0, maxPossibleAmount);

            return amountValue;
        }

        void InstantiateEnemiesFromPool(Transform enemyPool, int amountToSpawn)
        {
            int amountSpawned = 0;

            foreach (Transform enemy in enemyPool)
            {
                if (amountSpawned >= amountToSpawn)
                {
                    return;
                }

                if (!enemy.gameObject.active)
                {
                    enemy.GetComponent<BaseEnemyPositionManager>().objPool = enemyPool;
                    enemy.parent = null;
                    enemy.gameObject.SetActive(true);

                    //set all child to active
                    foreach (Transform item in enemy)
                    {
                        item.gameObject.SetActive(true);
                    }

                    enemy.GetComponent<IEnemySpawnPositionInitialConfiguration>().SetInitialSpawnConfiguration();
                    //enemy.GetComponent<EnemySpriteManager>().SetSpriteForBackgroundContext();
                    amountSpawnedEnemies++;
                    amountSpawned++;
                }

                if (amountSpawned == amountToSpawn)
                {
                    break;
                }
            }

        }

        IEnumerator SpawnEnemyRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(.3f);
                if (amountSpawnedEnemies < currentMaxAmountEnemies)
                {
                    SpawnEnemies();
                }
            }
        }
    }
}
