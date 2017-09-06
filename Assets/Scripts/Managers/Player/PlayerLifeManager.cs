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
        public List<Image> lifeImages;
        public Image initialLifeImage;
        public float lifeImageOffset_x;
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

            //Player has bought life buffs?
            if (playerData.GetLifeBuff() > 0)
            {
                lives += playerData.GetLifeBuff();
            }

            lifeImages = new List<Image>();
            lifeImages.Add(initialLifeImage);

            canBeHit = true;
            float pos_x = initialLifeImage.rectTransform.anchoredPosition.x;
            float pos_y = initialLifeImage.rectTransform.anchoredPosition.y;

            for (int i = 1; i < lives; i++)
            {
                Image lifeImage = Instantiate(initialLifeImage);
                lifeImage.transform.SetParent(initialLifeImage.transform.parent, false);
                float newPosX = (pos_x * (i + 1)) + (lifeImageOffset_x * i);
                lifeImage.rectTransform.anchoredPosition = new Vector2(newPosX, pos_y);
                lifeImages.Add(lifeImage);
            }
        }

        public void DecreaseLife()
        {
            if (canBeHit)
            {
                lives--;

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
                    DeduceLifeIcon();
                    DisablePlayer();
                }
                else
                {

                    DeduceLifeIcon();

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
            lives++;

            int i = lifeImages.Count;
            var lastImg = lifeImages[i - 1];
            float pos_x = initialLifeImage.rectTransform.anchoredPosition.x;
            float pos_y = initialLifeImage.rectTransform.anchoredPosition.y;

            Image lifeImage = Instantiate(initialLifeImage);
            lifeImage.transform.SetParent(initialLifeImage.transform.parent, false);
            float newPosX = (pos_x * (i + 1)) + (lifeImageOffset_x * i);
            lifeImage.rectTransform.anchoredPosition = new Vector2(newPosX, pos_y);
            lifeImages.Add(lifeImage);
        }

        IEnumerator MakePlayerInvulnerable()
        {
            canBeHit = false;
            yield return new WaitForSeconds(3);
            canBeHit = true;
            StopCoroutine(MakePlayerInvulnerable());
        }

        void DeduceLifeIcon()
        {
            for (int i = lifeImages.Count - 1; i >= 0; i--)
            {
                if (lifeImages[i].IsActive())
                {
                    lifeImages[i].gameObject.SetActive(false);
                    break;
                }
            }
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
