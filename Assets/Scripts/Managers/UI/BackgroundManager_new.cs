using Assets.Scripts.Enums;
using Assets.Scripts.Services;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class BackgroundManager_new : MonoBehaviour
    {
        public Transform backgroundDisplay;
        public int maxPropsAmount;
        public SpriteRenderer backgroundSpriteRenderer;
        public Color[] backgroundColors;

        public Transform propsPool;
        public List<GameObject> currentContextProps;
        public List<GameObject> nextContextProps;

        public float timeToChangeBackground;

        public float backgroundTickTime;
        int currentBackgroundColor;
        BackgroundContextEnum lastBackground;

        public BackgroundContextEnum currentBackgroundContext;
        bool hasNextContextLoaded;

        Thread randomizeThread;
        Thread nextPropsThread;
        void Start()
        {
            currentContextProps = BackgroundService.SetPropsForBackgroundContext(propsPool, currentBackgroundContext, maxPropsAmount, backgroundDisplay);

            for (int i = 0; i < currentContextProps.Count; i++)
            {
                currentContextProps[i].gameObject.SetActive(true);
            }
            ChangeBackgroundColor();

            lastBackground = BackgroundService.GetLastBackground();
        }

        void Update()
        {
            //Has loaded next context and still has props from previous background
            //Wait until all props have been unloaded.
            if (HasPropsFromPreviousBackground() && hasNextContextLoaded)
            {
                return;
            }
            else if (!HasPropsFromPreviousBackground() && hasNextContextLoaded)
            {
                SwapPropsToCurrentContext();
            }

            if (currentBackgroundContext == lastBackground) { return; }

            //Randomize props for this context
            if (!HasPropsFromPreviousBackground() && !hasNextContextLoaded)
            {
                currentContextProps = BackgroundService.SetPropsForBackgroundContext(propsPool, currentBackgroundContext, maxPropsAmount, backgroundDisplay);

                for (int i = 0; i < currentContextProps.Count; i++)
                {
                    currentContextProps[i].SetActive(true);
                }
            }

            backgroundTickTime += Time.deltaTime;

            if (backgroundTickTime >= timeToChangeBackground)
            {

                currentBackgroundContext = BackgroundService.GetNextContext(currentBackgroundContext, lastBackground.GetHashCode(), ref maxPropsAmount);
                LoadNextBackground();
            }
        }

        void LoadNextBackground()
        {
            backgroundTickTime = 0;

            nextContextProps = BackgroundService.SetPropsForBackgroundContext(propsPool, currentBackgroundContext, maxPropsAmount, backgroundDisplay);

            hasNextContextLoaded = true;
        }

        void SwapPropsToCurrentContext()
        {
            ChangeBackgroundColor();

            for (int i = 0; i < nextContextProps.Count; i++)
            {
                nextContextProps[i].gameObject.SetActive(true);
                currentContextProps.Add(nextContextProps[i]);
            }

            nextContextProps.Clear();
            hasNextContextLoaded = false;
        }

        void ChangeBackgroundColor()
        {
            if (currentBackgroundColor < backgroundColors.Length)
            {
                backgroundSpriteRenderer.color = backgroundColors[currentBackgroundColor];
                currentBackgroundColor++;
            }
        }

        bool HasPropsFromPreviousBackground()
        {
            return currentContextProps.Count > 0;
        }

        public void DisableProp(GameObject prop)
        {
            currentContextProps.Remove(prop);
        }
        
        //If true: all spawned props will be deactivated
        public bool RecallProps()
        {
            return hasNextContextLoaded == true;
        }
    }
}
