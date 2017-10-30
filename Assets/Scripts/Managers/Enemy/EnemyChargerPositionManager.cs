using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Managers.Enemy;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyChargerPositionManager : BaseLateralEnemyPositionManager
    {
     
        protected override void Update()
        {
            if (this.transform.position.y >= playerObj.transform.position.y)
            {
                //Set vertical direction to zero
                enemyController.verticalDirection = 0;
                enemyController.horizontalDirection = chargeDirection;
            }

            if (this.transform.position.x <= base.screenLeftEdge.x - 2
                || this.transform.position.x >= base.screenRightEdge.x + 2)
            {
                //remove speed from object
                enemyController.horizontalDirection = 0;
                this.SendObjectToPool();
            }
        }
    }
}
