using Assets.Scripts.Entities.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class PlayerShieldManager : MonoBehaviour
    {
        public Image initialShieldImage;
        public PlayerLifeManager playerLifeManager;

        public int amountShields;
        public List<Image> shieldsImages;
        public float shieldAfterHitDuration;
        public bool hasBeenHit;
        float afterHitDurationTickTime;

        //used to get life image initial position and decrease 'Y' position
        Image initialLifeImage;

        private void Start()
        {

            PlayerLifeManager playerLifeManager = GetComponent<PlayerLifeManager>();
            initialLifeImage = GetComponent<PlayerLifeManager>().initialLifeImage;
            float shieldImageOffset_x = playerLifeManager.lifeImageOffset_x;
            PlayerStatusData playerData = PlayerStatusManager.PlayerDataInstance;

            playerData.SwapStoredShields();
            amountShields =  playerData.GetShieldBuff();
            int initialAmountShields = amountShields;
            shieldsImages = new List<Image>();


            float pos_x = initialShieldImage.rectTransform.anchoredPosition.x;
            float pos_y = initialShieldImage.rectTransform.anchoredPosition.y;

            for (int i = 1; i < initialAmountShields + 1; i++)
            {
                IncreaseShield();
            }
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

            ////if it's the first iteration, simply enable the already exisiting first shield image
            if (amountShields == 1)
            {
                initialShieldImage.gameObject.SetActive(true);
                shieldsImages.Add(initialShieldImage);
                return;
            }

            float pos_x = initialShieldImage.rectTransform.anchoredPosition.x;
            float pos_y = initialShieldImage.rectTransform.anchoredPosition.y;
            float shieldImageOffset_x = GetComponent<PlayerLifeManager>().lifeImageOffset_x;
            int i = shieldsImages.Count + 1;

            Image _shieldImage = Instantiate(initialShieldImage);
            _shieldImage.gameObject.SetActive(true);
            _shieldImage.transform.SetParent(initialShieldImage.transform.parent, false);
            float newPosX = (pos_x * i) + (shieldImageOffset_x * (i - 1));
            _shieldImage.rectTransform.anchoredPosition = new Vector2(newPosX, pos_y);

            _shieldImage.gameObject.SetActive(true);

            shieldsImages.Add(_shieldImage);
        }
        public void DecreaseShield()
        {
            hasBeenHit = true;
            amountShields--;
            playerLifeManager.SetPlayerInvulnerable();

            for (int i = shieldsImages.Count -1; i >= 0; i--)
            {
                if (shieldsImages[i].IsActive())
                {
                    shieldsImages[i].gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}
