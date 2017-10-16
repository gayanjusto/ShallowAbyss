using Assets.Scripts.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class InteractiveBubblesPoolManager : MonoBehaviour
    {
        public GameObject bubblesPool;
        public int maxBubbles;

        List<Transform> spawnedHolders;

        private void Start()
        {
            spawnedHolders = new List<Transform>();
            maxBubbles = bubblesPool.transform.childCount;
            StartCoroutine(GetBubbleFromPool());
        }
        public void DisableBubble(GameObject bubble)
        {
            spawnedHolders.Remove(bubble.transform.parent);
            bubble.SetActive(false);

            if (spawnedHolders.Count < maxBubbles)
            {
                StartCoroutine(GetBubbleFromPool());
            }
        }

        IEnumerator GetBubbleFromPool()
        {
            while (spawnedHolders.Count < maxBubbles)
            {
                yield return new WaitForSeconds(2);

                bool hasBubbles = false;
                int bubbleHolder_posX = RandomValueTool.GetRandomValue(-5, 5);
                //Find bubble from pool
                foreach (Transform bubbleHolder in bubblesPool.transform)
                {
                    if (spawnedHolders.Contains(bubbleHolder)) { continue; }
                    spawnedHolders.Add(bubbleHolder);
                    bubbleHolder.transform.position = new Vector3(bubbleHolder_posX, bubbleHolder.position.y);

                    foreach (Transform bubble in bubbleHolder.transform)
                    {
                        if (!bubble.gameObject.active)
                        {
                            bubble.gameObject.SetActive(true);
                            hasBubbles = true;
                            var rndScale = Random.Range(1, 3);
                            bubble.transform.localScale = new Vector3(rndScale, rndScale, rndScale);
                        }
                    }

                    if (hasBubbles) { break; }
                }
            }

        }
    }
}
