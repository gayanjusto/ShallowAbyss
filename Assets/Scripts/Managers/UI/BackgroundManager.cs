using Assets.Scripts.Enums;
using Assets.Scripts.Services;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class BackgroundManager : MonoBehaviour
    {
        public Transform backgroundsPool;
        public Transform backgroundPropsPool;

        public float timeToSlideBackground;
        public Vector3 amountToSlide;
        public float screenTop;
        public int currentBackgroundLevel;
        public int backgroundTick;

        public float screenSwapOffset;

        public Transform currentBackground;

        BackgroundContextEnum currentBackgroundContext;

        private void Start()
        {
            //we multiply by 2 since we want the bottom of the background to hit the screenTop
            //for such, the top of the background has to hit two the size of the screen top.
            screenTop = ScreenPositionService.GetTopEdge(Camera.main).y * 2;
            currentBackgroundContext = BackgroundContextEnum.Surface_1;
            currentBackground = BackgroundService.GetBackground(backgroundsPool, currentBackgroundContext);
            BackgroundService.SetPropsForBackgroundContext(backgroundPropsPool, ref currentBackground);


            StartCoroutine(SlideBackground());
        }

        private void Update()
        {
            if(currentBackground && currentBackground.position.y > screenTop)
            {
                Debug.Log("Trocar background!");
                //Disable current background
                BackgroundService.DisableCurrentBackground(currentBackground, backgroundPropsPool, backgroundsPool);

                //Get next background Enum
                //currentBackgroundContext = BackgroundService.GetNextContext(currentBackgroundContext, ref 0);

                currentBackground = BackgroundService.GetBackground(backgroundsPool, currentBackgroundContext);
                //Set next background as current
                BackgroundService.SetPropsForBackgroundContext(backgroundPropsPool, ref currentBackground);
            }
        }

        IEnumerator SlideBackground()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeToSlideBackground);
                //currentBackground.Translate(amountToSlide);

                backgroundTick++;

                if(backgroundTick % 60 == 0)
                {
                    currentBackgroundLevel++;
                }
            }
        }
    }
}
