using Assets.Scripts.Entities.Player;
using Assets.Scripts.Services.SocialServices;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class PlayerLifeManager : MonoBehaviour
    {
        public GameObject dummyDeadPlayer;

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

        void SetDummyDeadPlayer()
        {
            dummyDeadPlayer.transform.position = this.transform.position;
            dummyDeadPlayer.gameObject.SetActive(true);

            dummyDeadPlayer.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
        }
        public void DecreaseLife()
        {

            if (canBeHit)
            {
                hitAudioSource.Play();

                lives--;
                PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

                //Game Over
                if (lives <= 0)
                {

                    SetDummyDeadPlayer();

                    gameOverManager.TakeScreenShot();
                    //Stop the score count
                    scoreCounterManager.enabled = false;

                    //Save player stats
                    playerData.SetLifeBuff(0);

                    int finalScore =  scoreCounterManager.FinishAndGetFinalScore();
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
                StartCoroutine(MakePlayerInvulnerableRoutine());
                playerSpritesManager.FlashSprites();
            }
        }

        public void IncreaseLife()
        {
            lives++;
            UpdateLifeText();
        }

        public void SetInvulnerableForDash()
        {
            canBeHit = false;
        }

        public void SetVulnerableAfterDash()
        {
            SetPlayerInvulnerable();
        }

        void UpdateLifeText()
        {
            amountLifeTxt.text = string.Format("x {0}", lives);
        }
        IEnumerator MakePlayerInvulnerableRoutine()
        {
            canBeHit = false;
            yield return new WaitForSeconds(3);
            canBeHit = true;
            StopCoroutine(MakePlayerInvulnerableRoutine());
        }

        void DisablePlayer()
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
        }
    }
}
