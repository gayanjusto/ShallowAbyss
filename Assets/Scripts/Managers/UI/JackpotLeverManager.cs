using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class JackpotLeverManager : MonoBehaviour
    {
        public JackpotManager jackpotManager;
        public Animator leverAnimator;
        int pullTrigger = Animator.StringToHash("Pulled");

        public void PullLever()
        {
            leverAnimator.SetTrigger(pullTrigger);
            jackpotManager.PullLever();
        }
    }
}
