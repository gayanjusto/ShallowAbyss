using Assets.Scripts.Constants;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers.Enemy;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyCollisionCheckManager : MonoBehaviour
    {
        EnemyPoolManager enemyPoolManager;
        Transform topCollider;

        private void Start()
        {
            enemyPoolManager = GameObject.Find("EnemyPool").GetComponent<EnemyPoolManager>();
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
                PlayerCollisionManager playerCollisionManager = collision.gameObject.GetComponent<PlayerCollisionManager>();
                PlayerShieldManager shieldManager = collision.gameObject.GetComponent<PlayerShieldManager>();

                if (!playerCollisionManager.CanBeHit())
                {
                    return;
                }

                //If player has shield, decrease it instead of life
                if (shieldManager.HasShield())
                {
                    shieldManager.DecreaseShield();
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerLifeManager>().DecreaseLife();
                }
            }
        }
    }
}
