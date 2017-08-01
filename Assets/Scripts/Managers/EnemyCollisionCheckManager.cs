using Assets.Scripts.Constants;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyCollisionCheckManager : MonoBehaviour
    {
        EnemyController enemyController;
        EnemySpawnerManager enemySpawnerManager;
        Transform topCollider;

        private void Start()
        {
            enemyController = this.transform.parent.GetComponent<EnemyController>();
            enemySpawnerManager = GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerManager>();
            topCollider = GameObject.Find("TopCollider").transform;
        }

        private void Update()
        {
            //Has hit top collider, disable and send enemy to pool
            if (this.transform.position.y >= topCollider.position.y)
            {

                if (this.gameObject.name.Contains(EnemyTypeEnum.Heavy.ToString()))
                {
                    enemySpawnerManager.hasSpawnedHeavyEnemy = false;
                }

				enemySpawnerManager.amountSpawnedEnemies--;
                this.transform.parent.GetComponent<EnemyPositionManager>().SendObjectToPool();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == Tags.Player)
            {
                collision.gameObject.GetComponent<PlayerLifeManager>().DecreaseLife();
            }
        }
    }
}
