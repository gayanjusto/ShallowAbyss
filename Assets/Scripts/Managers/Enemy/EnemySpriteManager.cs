using Assets.Scripts.Tools;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class EnemySpriteManager : MonoBehaviour
    {

        //The background manager will take 19 minutes to slide to the bottom of the image
        //an enemy can have up to 19 different sprites
        public BackgroundManager backgroundManager;

        //the maximum number of sprites in the repository
        public int maxAmountSprites;
        public int currentMaxSpriteModel;

        public string enemyResourcesFolder;

        SpriteRenderer objectSpriteRenderer;
        string resourcesPath;
        private void Awake()
        {
            backgroundManager = GameObject.Find("BackgroundManager").GetComponent<BackgroundManager>();
            objectSpriteRenderer = GetComponent<SpriteRenderer>();
            resourcesPath = string.Format("Sprites/{0}/", enemyResourcesFolder);
        }

        public void SetSpriteForBackgroundContext()
        {
            if (backgroundManager.currentBackgroundLevel < maxAmountSprites)
            {
                currentMaxSpriteModel = backgroundManager.currentBackgroundLevel + 1;
            }
            else
            {
                currentMaxSpriteModel = maxAmountSprites;
            }

            int fraction = 100 / currentMaxSpriteModel;
            int minVal = 0;
            int maxVal = fraction;

            int spriteValueRange = UnityEngine.Random.Range(0, 100);

            for (int spriteValue = 0; spriteValue < currentMaxSpriteModel; spriteValue++)
            {
                if (spriteValueRange >= minVal && spriteValueRange < maxVal)
                {
                    objectSpriteRenderer.sprite = Resources.Load<Sprite>(string.Format("{0}{1}", resourcesPath, spriteValue));
                }

                minVal += fraction;
                maxVal += fraction;
            }
        }
    }
}
