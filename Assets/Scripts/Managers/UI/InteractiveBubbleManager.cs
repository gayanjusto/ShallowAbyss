using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class InteractiveBubbleManager : MonoBehaviour
    {
        public InteractiveBubblesPoolManager interactiveBubblesPoolManager;
        int explodingBool = Animator.StringToHash("Exploded");
        Animator animator;
        Vector3 clickedPosition;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

   
        private void OnMouseDown()
        {
            clickedPosition = this.transform.localPosition;
            animator.SetBool(explodingBool, true);
            StartCoroutine(WaitForAnimation());
            //Play exploding animation
            //interactiveBubblesPoolManager.DisableBubble(this.gameObject);
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
