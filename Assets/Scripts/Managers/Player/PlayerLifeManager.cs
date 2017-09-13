using Assets.Scripts.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class PlayerLifeManager : MonoBehaviour
    {
        public int lives;
        public int maxLifes;
        public Text amountLifeTxt;

        public ScoreManager scoreCounterManager;
        public PlayerStatusManager playerStatusManager;
        public GameOverManager gameOverManager;
        public DashManager dashManager;


        bool canBeHit;
        SpriteRenderer playerSpriteRenderer;

        private void Start()
        {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            PlayerStatusData playerData = PlayerStatusManager.PlayerDataInstance;
            maxLifes = playerData.GetLifeUpgrade();

            playerData.SwapStoredLivesToBuff();

            lives += playerData.GetLifeBuff();

            UpdateLifeText();

            canBeHit = true;
        }

        public void DecreaseLife()
        {
            Debug.Log("player hit");

            if (canBeHit)
            {
               // lives--;

                //Game Over
                if (lives == 0)
                {
                    //Stop the score count
                    scoreCounterManager.enabled = false;

                    //Save player stats
                    //Turn life and shield buffs equals zero after death
                    PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
                    playerData.SetLifeBuff(0);
                    playerData.SetShieldBuff(0);
                    int finalScore = Mathf.FloorToInt(scoreCounterManager.score); ;
                    playerData.IncreaseScore(finalScore);
                    playerStatusManager.SavePlayerStatus(playerData);

                    gameOverManager.SetGameOver(finalScore);
                    DisablePlayer();
                    UpdateLifeText();
                }
                else
                {
                    UpdateLifeText();

                    //Make player invulnerable for few seconds
                    SetPlayerInvulnerable();
                }

            }

        }


        public bool CanBeHit()
        {
            return canBeHit;
        }

        public void SetPlayerInvulnerable()
        {
            if (lives > 0)
            {

                StartCoroutine(MakePlayerInvulnerable());
                StartCoroutine(FlashPLayer());
            }
        }

        public void IncreaseLife()
        {
           
        }

        void UpdateLifeText()
        {
            amountLifeTxt.text = string.Format("x {0}", lives);
        }
        IEnumerator MakePlayerInvulnerable()
        {
            canBeHit = false;
            yield return new WaitForSeconds(3);
            canBeHit = true;
            StopCoroutine(MakePlayerInvulnerable());
        }

        void DisablePlayer()
        {
            Debug.Log("Disabled player");
            StopAllCoroutines();
            //Debug.Log("Player disabled");
            //dashManager.enabled = false;
            //playerSpriteRenderer.enabled = false;
            //GetComponent<PlayerMovementInputController>().enabled = false;
            this.gameObject.SetActive(false);
        }

        IEnumerator FlashPLayer()
        {
            while (!canBeHit)
            {
                yield return new WaitForSeconds(0.1f);
                playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;
            }

            if (canBeHit)
            {
                playerSpriteRenderer.enabled = true;
                StopCoroutine(FlashPLayer());
            }
        }
    }
}
