using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Managers.Ads
{
    public class AdsGameOverManager : MonoBehaviour
    {
        public AdsManager adsManager;
        public GameObject gameOverPanel;

        Animator animator;
        int explodedTrigger = Animator.StringToHash("Exploded");
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ClickAdsBubble()
        {
            adsManager.ShowAd();
        }
    }
}
