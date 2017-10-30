using Assets.Scripts.Controllers;
using Assets.Scripts.Managers.Enemy.EnemySuckerTrapper;

namespace Assets.Scripts.Managers.Enemy
{
    public class EnemySuckerPositionManager : BaseLateralEnemyPositionManager
    {
        TrapperSuckerPlayerDebufferManager trapperSuckerPlayerDebufferManager;

        protected override void Start()
        {
            base.Start();
            trapperSuckerPlayerDebufferManager = GetComponent<TrapperSuckerPlayerDebufferManager>();
        }
        protected override void Update()
        {
            if (this.transform.position.y >= 0
                && !trapperSuckerPlayerDebufferManager.hasTrappedPlayer
                && !trapperSuckerPlayerDebufferManager.hasCastAction)
            {
                enemyController.verticalDirection = 0;

                trapperSuckerPlayerDebufferManager.SetPlayerDebuff(chargeDirection);
            }

            if (trapperSuckerPlayerDebufferManager.hasCastAction)
            {
                enemyController.verticalDirection = 1;
            }
        }

        public override void SendObjectToPool()
        {
            trapperSuckerPlayerDebufferManager.hasCastAction = false;
            base.SendObjectToPool();
        }
    }
}
