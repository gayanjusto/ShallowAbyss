using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class InteractiveBubbleManager : MonoBehaviour
    {
        public InteractiveBubblesPoolManager interactiveBubblesPoolManager;
        int explodingBool = Animator.StringToHash("Exploded");
        Animator animator;
        public AudioSource bubblePlopAudioSource;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

   
        private void OnMouseDown()
        {
            bubblePlopAudioSource.Play();
            animator.SetBool(explodingBool, true);
            StartCoroutine(WaitForAnimation());
        }

        IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            StopCoroutine(WaitForAnimation());
            animator.SetBool(explodingBool, false);

            //reset bubble sprite
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/GameUI/Bubbles");
            interactiveBubblesPoolManager.DisableBubble(this.gameObject);
        }
    }
}
