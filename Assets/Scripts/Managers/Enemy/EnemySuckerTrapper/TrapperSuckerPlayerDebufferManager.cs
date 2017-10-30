using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Managers.Enemy.EnemySuckerTrapper
{
    public class TrapperSuckerPlayerDebufferManager : MonoBehaviour
    {
        public Transform player;
        public PlayerMovementInputController playerMovementInputController;
        EnemyTrapperAnimationController enemyTrapperAnimationController;

        public float debuffAmount;
        public float trapDuration;

        public bool hasTrappedPlayer;
        public bool hasCastAction;
        float durationTick;

        private void Start()
        {
            enemyTrapperAnimationController = GetComponent<EnemyTrapperAnimationController>();
        }
        /// <summary>
        /// facingDirection: 1 => Left to Right | -1 => Right to Left
        /// </summary>
        /// <param name="enemyFacingDirection"></param>
        public void SetPlayerDebuff(int enemyFacingDirection)
        {
            enemyTrapperAnimationController.PlayOpenMouth();

            float hDebuff = debuffAmount;
            if(enemyFacingDirection == 1 && debuffAmount > 0)
            {
                hDebuff *= -1;
            }

            playerMovementInputController.SetH_Debuff(hDebuff);

            hasTrappedPlayer = true;
        }

        private void Update()
        {
            if (hasTrappedPlayer)
            {
                float vDebuff = debuffAmount;
                if (player.position.y < 0)
                {
                    playerMovementInputController.SetV_Debuff(vDebuff);
                }
                else
                {
                    playerMovementInputController.SetV_Debuff(vDebuff * -1);
                }

                durationTick += Time.deltaTime;

                if(durationTick >= trapDuration)
                {
                    ResetPlayerDebuff();
                    enemyTrapperAnimationController.PlayClosingMouth();
                    hasTrappedPlayer = false;
                    hasCastAction = true;
                    durationTick = 0;
                    //GetComponent<BaseLateralEnemyPositionManager>().SendObjectToPool();
                }
            }
        }

        private void ResetPlayerDebuff()
        {
            playerMovementInputController.SetH_Debuff(0);
            playerMovementInputController.SetV_Debuff(0);
        }
    }
}
