using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class PlayerLifeManager : MonoBehaviour
    {
        public int lifes;
        public int maxLifes;
        public bool canBeHit;
        public List<Image> lifeImages;
        public Image initialLifeImage;
        public Image gameOverPanel;
        public float lifeImageOffset_x;
        public ScoreCounterManager scoreCounterManager;
        public ScoreManager scoreManager;


        private void Start()
        {
            lifeImages = new List<Image>();
            lifeImages.Add(initialLifeImage);

            canBeHit = true;
            float pos_x = initialLifeImage.rectTransform.anchoredPosition.x;
            float pos_y = initialLifeImage.rectTransform.anchoredPosition.y;

            Debug.Log(pos_x);
            for (int i = 1; i < maxLifes; i++)
            {
                Image lifeImage = Instantiate(initialLifeImage);
                lifeImage.transform.SetParent(initialLifeImage.transform.parent, false);
                float newPosX = (pos_x *  (i + 1)) + (lifeImageOffset_x * i);
                Debug.Log(newPosX);
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

                    //Save score
                    scoreManager.SaveScore(scoreCounterManager.score);

                    gameOverPanel.gameObject.SetActive(true);

                    this.gameObject.SetActive(false);
                    return;
                }

                for (int i = lifeImages.Count -1; i > 0; i--)
                {
                    if (lifeImages[i].IsActive())
                    {
                        lifeImages[i].gameObject.SetActive(false);
                        break;
                    }
                }


                //Make player invulnerable for few seconds
                StartCoroutine(MakePlayerInvulnerable());
            }

        }

        IEnumerator MakePlayerInvulnerable()
        {
            canBeHit = false;
            yield return new WaitForSeconds(3);
            canBeHit = true;
        }
    }
}
