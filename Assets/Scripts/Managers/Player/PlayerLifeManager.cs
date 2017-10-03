using Assets.Scripts.Entities.Player;
using System.Collections;
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
        public GameOverManager gameOverManager;
        public DashManager dashManager;
        public AudioSource hitAudioSource;

        bool canBeHit;
        PlayerSpritesManager playerSpritesManager;
        private void Start()
        {
            playerSpritesManager = GetComponent<PlayerSpritesManager>();
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();
            maxLifes = playerData.GetLifeUpgrade();

            playerData.SwapStoredLivesToBuff();

            lives += playerData.GetLifeBuff();

            UpdateLifeText();

            canBeHit = true;

            PlayerStatusService.SavePlayerStatus(playerData);

            hitAudioSource.clip.LoadAudioData();
        }

        public void DecreaseLife()
        {

            if (canBeHit)
            {
                hitAudioSource.Play();

                lives--;
                PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

                //Game Over
                if (lives == 0)
                {
                    //Stop the score count
                    scoreCounterManager.enabled = false;

                    //Save player stats
                    playerData.SetLifeBuff(0);

                    int finalScore = Mathf.FloorToInt(scoreCounterManager.score);
                    playerData.IncreaseScore(finalScore);
                    PlayerStatusService.SavePlayerStatus(playerData);

                    gameOverManager.SetGameOver(finalScore);
                    DisablePlayer();
                    UpdateLifeText();
                }
                else
                {
                    //buffs means extra lives, so we reduce one from total amount of lives the player has
                    playerData.SetLifeBuff(lives -1);
                    PlayerStatusService.SavePlayerStatus(playerData);
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
                playerSpritesManager.FlashSprites();
            }
        }

        public void IncreaseLife()
        {
            lives++;
            UpdateLifeText();
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
            StopAllCoroutines();
            this.gameObject.SetActive(false);
        }
    }
}
