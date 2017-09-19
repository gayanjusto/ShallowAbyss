using Assets.Scripts.Dictionaries;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Managers.Enemy;
using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemySpawnerManager : MonoBehaviour
    {

        public GameObject enemyPool;
        public int amountSpawnedEnemies;
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
        public float heavyEnemySpawnTime;

        public int maxAmountHeavyEnemyInScene;
        public int currentAmountHeavyEnemyInScene;

        public int maxAmountChargerInScene;
        public int currentAmountChargerEnemyInScene;


        public short amountStandard, amountLight, amountCharger, amountHeavy;
        int maxEnemiesTypes;

        private void Start()
        {
            maxEnemiesTypes = (Enum.GetValues(typeof(EnemyTypeEnum)) as EnemyTypeEnum[]).Length - 1;

            currentDifficultTime = timeToChangeDifficult;
            currentMaxAmountEnemies = amountEnemiesBaseLevel;


            amountStandard = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/standard").text);
            amountLight = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/light").text);
            amountCharger = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/charger").text);
            amountHeavy = Convert.ToInt16(Resources.Load<TextAsset>("EnemyTypeCount/heavy").text);

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
            if(currentBreathingLevelAmount > 0)
            {
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
                currentMaxAmountEnemies++;
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

            int amountEnemiesToSpawn = GetAmountEnemiesToSpawn(possibleAmountToSpawn);
            possibleAmountToSpawn -= amountEnemiesToSpawn;

            //Get the type of enemy we'll spawn
            EnemyTypeEnum enemyTypeToSpawn = EnemySpawnerService.GetEnemyTypeToSpawn(currentLevelDifficult);

            //if enemy == heavy, check if there's already the max in scene
            if (enemyTypeToSpawn == EnemyTypeEnum.Heavy && !CanSpawnHeavyEnemy(ref amountEnemiesToSpawn))
            {
                return;
            }

            //if enemy == charger, check if there's already the max in scene
            if (enemyTypeToSpawn == EnemyTypeEnum.Charger && !CanSpawnChargerEnemy(ref amountEnemiesToSpawn))
            {
                return;
            }

            //Get the number of the enemy type. E.g: Standard/0 or Standard/1
            string enemyMaxVal = this.GetStringFieldValue("amount" + enemyTypeToSpawn.ToString());
            var enemyKindVal = EnemySpawnerService.GetEnemyKind(enemyTypeToSpawn, Convert.ToInt32(enemyMaxVal), currentLevelDifficult);

            //Find the enemytype category in the Hierachy. It's contained inside EnemyPool
            Transform enemyCategory = enemyPool.transform.FindChild(string.Format("{0}/{1}", enemyTypeToSpawn.ToString(), enemyKindVal));



            InstantiateEnemiesFromPool(enemyCategory, amountEnemiesToSpawn);
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
    }
}
