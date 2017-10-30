using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class BaseEnemyPositionManager : MonoBehaviour, IEnemySpawnPositionInitialConfiguration
    {
        public EnemySpawnManager enemySpawnManager;
        protected EnemyController enemyController;

        protected WallsManager wallsManager;

        protected Vector3 screenTopEdge;
        public Vector3 screenLeftEdge;
        public Vector3 screenRightEdge;
        public Vector3 screenBottomEdge;

        public Transform objPool;

        public float leftOffset;
        public float rightOffset;

        public float y_Position;
        public float z_Position;

        Vector3 GetPositionToSpawnEnemy()
        {
            Vector3 newPos = new Vector3(
                RandomValueTool.GetRandomValue
                (
                    (int)ScreenPositionService.GetLeftEdge(Camera.main).x, 
                    (int)ScreenPositionService.GetRightEdge(Camera.main).x
                ),
                screenBottomEdge.y - RandomValueTool.GetRandomValue(5, 15) + y_Position,
                z_Position);

            return newPos;
        }

        float GetEnemyRightEdge()
        {
            return screenRightEdge.x - rightOffset;
        }

        float GetEnemyLeftEdge()
        {
            return screenLeftEdge.x + leftOffset;
        }

        protected virtual void Start()
        {
            wallsManager = GameObject.Find("WallsManager").GetComponent<WallsManager>();
            enemyController = GetComponent<EnemyController>();
            screenTopEdge = ScreenPositionService.GetTopEdge(Camera.main);
            screenLeftEdge = ScreenPositionService.GetLeftEdge(Camera.main);
            screenRightEdge = ScreenPositionService.GetRightEdge(Camera.main);
            screenBottomEdge = ScreenPositionService.GetBottomEdge(Camera.main);
        }

        protected virtual void Update()
        {
            //Has reached left limit and is walking to the left
            if (this.transform.position.x <= GetEnemyLeftEdge() && enemyController.horizontalDirection == -1)
            {
                enemyController.horizontalDirection *= -1;
            }
            //Has reached right limit and is walking to the right
            if (this.transform.position.x >= GetEnemyRightEdge() && enemyController.horizontalDirection == 1)
            {
                enemyController.horizontalDirection *= -1;
            }
        }

        public virtual void SendObjectToPool()
        {
            enemySpawnManager.SendToPool(objPool, this.gameObject);
        }

        protected void SetInitialSpawnPosition()
        {
            float pos_z = transform.position.z;
            Vector3 rndPos = GetPositionToSpawnEnemy();

            this.transform.position = new Vector3(rndPos.x, rndPos.y, z_Position);
        }
       
        public virtual void SetInitialSpawnConfiguration()
        {
            //Start();
            SetInitialSpawnPosition();
        }
    }
}
