using Assets.Scripts.Dictionaries;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers.UI;
using Assets.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class BackgroundService
    {
        //A background object has 10 units of size.
        //Top: 5
        //Mid: 0
        //Bottom: -5

        const float MAX_HEIGHT = 5;
        const float MIN_HEIGHT = -5;

        const float MAX_WIDTH = 6.5f;
        const float MIN_WIDTH = -6.5f;

        static Transform GetPropHolder(Transform backgroundPropsPool, string backgroundName)
        {
            Transform propsPool = null;
            foreach (Transform propHolder in backgroundPropsPool)
            {
                if (propHolder.name == backgroundName)
                {
                    propsPool = propHolder;
                    break;
                }
            }

            return propsPool;
        }

        public static Transform GetBackground(Transform backgroundsPool, BackgroundContextEnum context)
        {
            foreach (Transform bckg in backgroundsPool)
            {
                if (bckg.name == context.ToString())
                {
                    return bckg;
                }
            }

            return null;
        }

        public static BackgroundContextEnum GetLastBackground()
        {
            return Enum.GetValues(typeof(BackgroundContextEnum)).Cast<BackgroundContextEnum>().Last();

        }
        public static BackgroundContextEnum GetNextContext(BackgroundContextEnum currentContext, int maxBackgroundVal, ref int maxAmount)
        {
           
            int ctxVal = currentContext.GetHashCode();

            //has reached last level
            if (ctxVal == maxBackgroundVal)
            {
                return currentContext;
            }

            int nextVal = ctxVal + 1;

           if(nextVal == maxBackgroundVal)
            {
                //Abyss can have only 1 abyssal monster
                maxAmount = 1;
            }

            return (BackgroundContextEnum)nextVal;
        }

        public static void DisableCurrentBackground(Transform currentBackground, Transform backgroundPropsPool, Transform backgroundsPool)
        {
            //disable props and set them back into pool
            Transform propsHolder = GetPropHolder(backgroundPropsPool, currentBackground.name);

            foreach (Transform props in currentBackground)
            {
                props.gameObject.SetActive(false);
                props.parent = propsHolder;
            }

            //disable background
            currentBackground.gameObject.SetActive(false);
            currentBackground.transform.parent = backgroundsPool;
            currentBackground.transform.position = new Vector3(0, -6, 0);
        }

        public static List<GameObject> SetPropsForBackgroundContext(Transform backgroundPropsPool, BackgroundContextEnum currentContext, int maxProps, Transform backgroundDisplay)
        {
            List<GameObject> props = new List<GameObject>();

            var possiblePropsContexts = BackgroundPropsLevel.GetBackgroundPropLevel()[currentContext];
            List<Transform> contextsPools = new List<Transform>();

            //Find context in pool
            foreach (Transform item in backgroundPropsPool)
            {
                if (possiblePropsContexts.minLevel == item.name || possiblePropsContexts.maxLevel == item.name)
                {
                    contextsPools.Add(item);
                }
            }


            for (int i = 0; i < maxProps; i++)
            {
                int ctxVal = RandomValueTool.GetRandomValue(0, contextsPools.Count - 1);

                var child = GetChild(contextsPools[ctxVal], backgroundDisplay);

                if (child != null)
                    props.Add(child.gameObject);
            }


            return props;
        }

        static Transform GetChild(Transform contextPool, Transform backgroundDisplay)
        {
            int childCount = contextPool.childCount;

            if(childCount <= 0)
            {
                return null;
            }

            int childVal = RandomValueTool.GetRandomValue(0, childCount - 1);
            var child = contextPool.GetChild(childVal);
            child.GetComponent<BackgroundPropManager>().SetOriginalContext(contextPool);
            child.transform.parent = backgroundDisplay;
            return child;

        }
        public static void SetPropsForBackgroundContext(Transform backgroundPropsPool, ref Transform nextBackground)
        {
            nextBackground.parent = null;
            Transform propsPool = null;
            foreach (Transform propHolder in backgroundPropsPool)
            {
                if (propHolder.name == nextBackground.name)
                {
                    propsPool = propHolder;
                    break;
                }
            }

            int maxProps = propsPool.childCount;
            int minProps = maxProps / 2;

            int amountPropsToUse = RandomValueTool.GetRandomValue(minProps, maxProps);
            int currentPropUsage = 0;

            //Attach props to nextBackground Transform
            foreach (Transform prop in propsPool)
            {
                if (currentPropUsage == amountPropsToUse) { break; }
                prop.parent = nextBackground;
                prop.transform.localPosition = new Vector3(RandomValueTool.GetRandomFloatValue(MIN_WIDTH, MAX_WIDTH), RandomValueTool.GetRandomFloatValue(MIN_HEIGHT, MAX_HEIGHT), 1);
                prop.transform.localScale = new Vector3(RandomValueTool.GetRandomFloatValue(.8f, 1), RandomValueTool.GetRandomFloatValue(.8f, 1), 1);
                prop.GetComponent<SpriteRenderer>().flipX = RandomValueTool.GetRandomValue(0, 1) == 0 ? true : false;
                prop.gameObject.SetActive(true);
            }
        }
    }
}
