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
        public int lifes;
        public int maxLifes;
        public List<Image> lifeImages;
        public Image initialLifeImage;
        public float lifeImageOffset_x;
        public ScoreCounterManager scoreCounterManager;
        public PlayerStatusManager playerStatusManager;
        public GameOverManager gameOverManager;

        bool canBeHit;
        SpriteRenderer playerSpriteRenderer;

        private void Start()
        {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();

            //Player has bought life buffs?
            if (playerData.lifeBuff > 0)
            {
                maxLifes += playerData.lifeBuff;
                lifes += playerData.lifeBuff;
            }

            lifeImages = new List<Image>();
            lifeImages.Add(initialLifeImage);

            canBeHit = true;
            float pos_x = initialLifeImage.rectTransform.anchoredPosition.x;
            float pos_y = initialLifeImage.rectTransform.anchoredPosition.y;

            for (int i = 1; i < maxLifes; i++)
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
                lifes--;

                //Game Over
                if (lifes == 0)
                {
                    //Stop the score count
                    scoreCounterManager.enabled = false;

                    //Save player stats
                    //Turn life and shield buffs equals zero after death
                    PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
                    playerData.lifeBuff = 0;
                    playerData.shieldBuff = 0;
                    int finalScore = Mathf.FloorToInt(scoreCounterManager.score); ;
                    playerData.score += finalScore;
                    playerStatusManager.SavePlayerStatus(playerData);

                    gameOverManager.SetGameOver(finalScore);
                    DeduceLifeIcon();
                    DisablePlayer();
                }


                DeduceLifeIcon();

                //Make player invulnerable for few seconds
                SetPlayerInvulnerable();

                //FlashPlayerWhileInvulnerable
                if (lifes > 0)
                {
                    StartCoroutine(FlashPLayer());
                }
            }

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
        public bool CanBeHit()
        {
            return canBeHit;
        }

        public void SetPlayerInvulnerable()
        {
            StartCoroutine(MakePlayerInvulnerable());
        }
        IEnumerator MakePlayerInvulnerable()
        {
            canBeHit = false;
            yield return new WaitForSeconds(3);
            canBeHit = true;
        }

        void DisablePlayer()
        {
            playerSpriteRenderer.enabled = false;
            GetComponent<PlayerMovementInputController>().enabled = false;

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
