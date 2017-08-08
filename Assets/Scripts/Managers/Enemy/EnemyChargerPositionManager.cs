using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Managers.Enemy;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyChargerPositionManager : BaseEnemyPositionManager, IEnemySpawnPositionInitialConfiguration
    {
        public GameObject playerObj;

        public int chargeDirection;


        void Update()
        {
            if (this.transform.position.y >= playerObj.transform.position.y)
            {
                //Set vertical direction to zero
                enemyController.verticalDirection = 0;
                enemyController.horizontalDirection = chargeDirection;
                enemyController.minSpeed = 5;
            }

            if (this.transform.position.x <= base.screenLeftEdge.x - 2
                || this.transform.position.x >= base.screenRightEdge.x + 2)
            {
                //remove speed from object
                enemyController.horizontalDirection = 0;
                base.SendObjectToPool();
            }
        }

        public void SetInitialSpawnConfiguration()
        {
            base.Start();
            int side = RandomValueTool.GetRandomValue(0, 100);

            //Left side
            if (side < 50)
            {
                //Left to Right
                chargeDirection = 1;
                this.transform.position =new Vector3(base.screenLeftEdge.x, base.screenLeftEdge.y);
            }
            else
            {
                //Right to Left
                chargeDirection = -1;
                this.transform.position = new Vector3(base.screenRightEdge.x, base.screenRightEdge.y);
            }

            enemyController.verticalDirection = 1;
            enemyController.SetRandomSpeed();
        }

    }
}
