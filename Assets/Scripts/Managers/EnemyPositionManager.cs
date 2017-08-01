using Assets.Scripts.Constants;
using Assets.Scripts.Controllers;
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyPositionManager : MonoBehaviour
    {
        public float leftOffset;
        public float rightOffset;

        Vector3 screenTopEdge;
        public Vector3 screenLeftEdge;
        public Vector3 screenRightEdge;

        GameObject enemyPool;
        EnemyController enemyController;

        private void Start()
        {
            enemyPool = GameObject.Find("EnemyPool");
            screenTopEdge = ScreenPositionService.GetTopEdge(Camera.main);
            screenLeftEdge = ScreenPositionService.GetLeftEdge(Camera.main);
            screenRightEdge = ScreenPositionService.GetRightEdge(Camera.main);

            enemyController = GetComponent<EnemyController>();
        }

        private void Update()
        {
            //Has reached left limit and is walking to the left
            if(this.transform.position.x <= screenLeftEdge.x + leftOffset && enemyController.horizontalDirection == -1)
            {
                enemyController.horizontalDirection *= -1;
            }
            //Has reached right limit and is walking to the right
            if (this.transform.position.x >= screenRightEdge.x - rightOffset && enemyController.horizontalDirection == 1)
            {
                enemyController.horizontalDirection *= -1;
            }
        }

        public void SendObjectToPool()
        {
            this.transform.parent = enemyPool.transform;
            gameObject.SetActive(false);
        }

    }
}
