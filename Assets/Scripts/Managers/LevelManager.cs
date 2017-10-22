using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public Text levelWarner;
        public Animator levelWarnerAnimator;
        int showLevelWarnerHash = Animator.StringToHash("Show");
        string showLevelWarnAnimName = "LevelWarnerAnim";
        int currentLevel;
        int timeToChangeLevel;
        private void Start()
        {
            IncreaseLevel();
        }

        IEnumerator CheckAnimationHasEnded()
        {
            while(AnimatorIsPlaying() &&
                levelWarnerAnimator.GetCurrentAnimatorStateInfo(0).IsName(showLevelWarnAnimName))
            {
                yield return new WaitForSeconds(.5f);
            }


            levelWarnerAnimator.SetBool(showLevelWarnerHash, false);
            levelWarnerAnimator.enabled = false;
            levelWarner.enabled = false;
            StopCoroutine(CheckAnimationHasEnded());
        }

        bool AnimatorIsPlaying()
        {
            return levelWarnerAnimator.GetCurrentAnimatorStateInfo(0).length >
                   levelWarnerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        public void IncreaseLevel()
        {

            levelWarner.enabled = true;
            levelWarnerAnimator.enabled = true;

            currentLevel++;
            levelWarner.text = string.Format("LEVEL - {0}", currentLevel);
            levelWarnerAnimator.SetTrigger(showLevelWarnerHash);
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

    }
}
