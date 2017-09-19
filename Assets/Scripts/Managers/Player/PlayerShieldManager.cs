using Assets.Scripts.Entities.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class PlayerShieldManager : MonoBehaviour
    {
        public PlayerLifeManager playerLifeManager;
        public Text shieldAmountTxt;

        public int amountShields;
        public float shieldAfterHitDuration;
        public bool hasBeenHit;
        float afterHitDurationTickTime;


        private void Start()
        {

            PlayerLifeManager playerLifeManager = GetComponent<PlayerLifeManager>();
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

            playerData.SwapStoredShields();
            amountShields =  playerData.GetShieldBuff();
            int initialAmountShields = amountShields;

            PlayerStatusService.SavePlayerStatus(playerData);

            UpdateShieldText();
        }

        private void Update()
        {
            if (hasBeenHit)
            {
                afterHitDurationTickTime += Time.deltaTime;

                if(afterHitDurationTickTime >= shieldAfterHitDuration)
                {
                    hasBeenHit = false;
                    afterHitDurationTickTime = 0;
                }
            }
        }

        public bool CanBeHit()
        {
            return !HasShield() || HasShield() && !hasBeenHit;
        }

        public bool HasShield()
        {
            return amountShields > 0;
        }

        public void IncreaseShield()
        {
            amountShields++;

            UpdateShieldText();

        }
        public void DecreaseShield()
        {
            amountShields--;

            UpdateShieldText();

            playerLifeManager.SetPlayerInvulnerable();
        }

        void UpdateShieldText()
        {
            shieldAmountTxt.text = string.Format("x {0}", amountShields);
        }
    }
}
