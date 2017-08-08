using Assets.Scripts.Services;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class BackgroundManager : MonoBehaviour
    {
        public float timeToSlideBackground;
        public Vector3 amountToSlide;

        public int currentBackgroundLevel;
        public int backgroundTick;

        public float screenSwapOffset;

        /// <summary>
        /// The background sprite has 2048 pixels of height. 
        /// The object world position goes from y: 58 to y: -58
        /// The manager will slide y: 0.1 per second, making a total of 1140 seconds which is 19 minutes
        /// </summary>
        public GameObject background;

        Vector3 screenTopEdge;

        float backgroundSize_Y;

        private void Start()
        {
            screenTopEdge = ScreenPositionService.GetTopEdge(Camera.main);

            StartCoroutine(SlideBackgroundDown());
        }

        IEnumerator SlideBackgroundDown()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeToSlideBackground);
                background.transform.Translate(amountToSlide);

                backgroundTick++;

                if(backgroundTick % 60 == 0)
                {
                    currentBackgroundLevel++;
                }
            }
        }
    }
}
