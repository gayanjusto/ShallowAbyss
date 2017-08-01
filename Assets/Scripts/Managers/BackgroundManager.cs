using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class BackgroundManager : MonoBehaviour
    {
        public float timeToSlideBackground;
        public Vector3 amountToSlide;

        private void Start()
        {
            StartCoroutine(SlideBackgroundDown());
        }

        IEnumerator SlideBackgroundDown()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeToSlideBackground);
                this.transform.Translate(amountToSlide);
            }
        }
    }
}
