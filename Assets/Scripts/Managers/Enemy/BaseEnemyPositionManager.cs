using Assets.Scripts.Controllers;
using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class BaseEnemyPositionManager : MonoBehaviour
    {
        protected EnemyController enemyController;
        protected EnemyPoolManager enemyPoolManager;
        protected WallsManager wallsManager;

        protected Vector3 screenTopEdge;
        public Vector3 screenLeftEdge;
        public Vector3 screenRightEdge;
        public Vector3 screenBottomEdge;

        protected void Start()
        {
            wallsManager = GameObject.Find("WallsManager").GetComponent<WallsManager>();
            enemyController = GetComponent<EnemyController>();

            screenTopEdge = ScreenPositionService.GetTopEdge(Camera.main);
            screenLeftEdge = ScreenPositionService.GetLeftEdge(Camera.main);
            screenRightEdge = ScreenPositionService.GetRightEdge(Camera.main);
            screenBottomEdge = ScreenPositionService.GetBottomEdge(Camera.main);

            enemyPoolManager = GameObject.Find("EnemyPool").GetComponent<EnemyPoolManager>();
        }

        public void SendObjectToPool()
        {
            enemyPoolManager.SendObjectToPool(this.gameObject);
        }

        protected void SetInitialSpawnPosition()
        {
            float pos_z = transform.position.z;
            Vector3 rndPos = GetPositionToSpawnEnemy();

            this.transform.position = new Vector3(rndPos.x, rndPos.y);
        }

        Vector3 GetPositionToSpawnEnemy()
        {
            Vector3 newPos = new Vector3(
                RandomValueTool.GetRandomValue
                (
                    (int)ScreenPositionService.GetLeftEdge(Camera.main).x, (int)ScreenPositionService.GetRightEdge(Camera.main).x
                ),
                screenBottomEdge.y - RandomValueTool.GetRandomValue(5, 15));

            return newPos;
        }
    }
}
