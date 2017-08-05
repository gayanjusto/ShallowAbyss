using Assets.Scripts.Constants;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers.Enemy;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyCollisionCheckManager : MonoBehaviour
    {
        EnemyController enemyController;
        EnemySpawnerManager enemySpawnerManager;
        EnemyPoolManager enemyPoolManager;
        Transform topCollider;

        private void Start()
        {
            enemyPoolManager = GameObject.Find("EnemyPool").GetComponent<EnemyPoolManager>();
            enemyController = this.transform.parent.GetComponent<EnemyController>();
            enemySpawnerManager = GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerManager>();
            topCollider = GameObject.Find("TopCollider").transform;
        }

        private void Update()
        {
            //Has hit top collider, disable and send enemy to pool
            if (this.transform.position.y >= topCollider.position.y)
            {
                enemyPoolManager.SendObjectToPool(this.transform.parent.gameObject);
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
