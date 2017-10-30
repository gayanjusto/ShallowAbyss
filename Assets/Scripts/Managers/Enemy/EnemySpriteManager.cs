using Assets.Scripts.Tools;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers.Enemy
{
    public class EnemySpriteManager : MonoBehaviour
    {

        //the maximum number of sprites in the repository
        public int maxAmountSprites;
        public int currentMaxSpriteModel;

        public string enemyResourcesFolder;

        SpriteRenderer objectSpriteRenderer;
        string resourcesPath;

        private void Awake()
        {
            objectSpriteRenderer = GetComponent<SpriteRenderer>();
            resourcesPath = string.Format("Sprites/{0}/", enemyResourcesFolder);
        }
    }
}
