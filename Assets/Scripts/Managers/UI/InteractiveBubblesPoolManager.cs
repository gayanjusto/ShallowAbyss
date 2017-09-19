using Assets.Scripts.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class InteractiveBubblesPoolManager : MonoBehaviour
    {
        public GameObject bubblesPool;

        private void Start()
        {
            StartCoroutine(GetBubbleFromPool());
        }
        public void DisableBubble(GameObject bubble)
        {
            bubble.SetActive(false);
            StartCoroutine(GetBubbleFromPool());
        }

        IEnumerator GetBubbleFromPool()
        {
            yield return new WaitForSeconds(2);

            int bubbleHolder_posX = RandomValueTool.GetRandomValue(-3, 3);
            //Find bubble from pool
            foreach (Transform bubbleHolder in bubblesPool.transform)
            {
                GameObject bubble = bubbleHolder.GetChild(0).gameObject;
                if (!bubble.active)
                {
                    bubbleHolder.transform.position = new Vector3(bubbleHolder_posX, bubbleHolder.position.y);
                    bubble.SetActive(true);
                    StopCoroutine(GetBubbleFromPool());
                    break;
                }
            }
        }
    }
}
