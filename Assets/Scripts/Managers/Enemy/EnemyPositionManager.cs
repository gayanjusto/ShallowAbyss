using System;
using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Managers.Enemy;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyPositionManager : BaseEnemyPositionManager, IEnemySpawnPositionInitialConfiguration
    {
        public float leftOffset;
        public float rightOffset;

        public void SetInitialSpawnConfiguration()
        {
            base.Start();
            //float wallScale = base.wallsManager.GetLeftWallScale().x;
            //leftOffset = this.transform.localScale.x / 2 + wallScale;
            //rightOffset = leftOffset + this.transform.localScale.x;
            base.SetInitialSpawnPosition();
        }

        private void Update()
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


        float GetEnemyRightEdge()
        {
            return screenRightEdge.x - rightOffset;
        }

        float GetEnemyLeftEdge()
        {
            return screenLeftEdge.x + leftOffset;
        }
    }
}
