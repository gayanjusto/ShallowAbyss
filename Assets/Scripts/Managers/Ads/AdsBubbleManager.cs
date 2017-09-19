using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Managers.Ads
{
    public class AdsBubbleManager : MonoBehaviour
    {
        public AdsManager adsManager;

        Animator animator;
        int explodedTrigger = Animator.StringToHash("Exploded");
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ClickAdsBubble()
        {
            animator.SetTrigger(explodedTrigger);
            adsManager.InitializeAd();
        }
    }
}
