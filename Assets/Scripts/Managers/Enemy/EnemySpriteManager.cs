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
            backgroundManager =  GameObject.Find("BackgroundManager").GetComponent<BackgroundManager>();
            objectSpriteRenderer = GetComponent<SpriteRenderer>();
            resourcesPath = string.Format("Sprites/{0}/", enemyResourcesFolder);
        }

        public void SetSpriteForBackgroundContext()
        {
            if(backgroundManager.currentBackgroundLevel < maxAmountSprites)
            {
                currentMaxSpriteModel = backgroundManager.currentBackgroundLevel;
            }else
            {
                currentMaxSpriteModel = maxAmountSprites;
            }

            int spriteValue = UnityEngine.Random.Range(0, currentMaxSpriteModel);
            //int spriteValue = RandomValueTool.GetRandomValue(0, currentMaxSpriteModel * 10);
            objectSpriteRenderer.sprite = Resources.Load<Sprite>(string.Format("{0}{1}", resourcesPath, spriteValue));
        }
    }
}
