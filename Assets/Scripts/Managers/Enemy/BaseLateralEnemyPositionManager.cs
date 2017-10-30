using Assets.Scripts.Interfaces.Managers.Enemy;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class BaseLateralEnemyPositionManager : BaseEnemyPositionManager, IEnemySpawnPositionInitialConfiguration
    {
        public GameObject playerObj;

        public int chargeDirection;

        public SpriteRenderer spriteRenderer;

        protected override void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            base.Start();
        }


        public override void SetInitialSpawnConfiguration()
        {
            Start();
            int side = RandomValueTool.GetRandomValue(0, 100);

            bool flipX = false;
            //Left side
            if (side < 50)
            {
                //Left to Right
                chargeDirection = 1;
                this.transform.position = new Vector3(base.screenLeftEdge.x + leftOffset, base.screenLeftEdge.y + y_Position);
                this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            }
            else
            {
                //Right to Left
                chargeDirection = -1;
                this.transform.position = new Vector3(base.screenRightEdge.x + rightOffset, base.screenRightEdge.y + y_Position);

                //Flip Sprite
                flipX = true;

                this.transform.localScale = new Vector3(
                    this.transform.localScale.x == 1 ? this.transform.localScale.x * -1 : this.transform.localScale.x * 1,
                    this.transform.localScale.y, 
                    this.transform.localScale.z);

            }


            enemyController.verticalDirection = 1;
            enemyController.SetRandomSpeed();
            //spriteRenderer.flipX = flipX;
        }
    }
}
