using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ScoreCounterManager : MonoBehaviour
    {
        public float score;
        public Text uiText;

        private void Update()
        {
            score += Time.deltaTime;
            UpdateScore();
        }

        public void DecreaseScore()
        {
            score--;
            uiText.text = string.Format("Score: {0}", Mathf.Floor(score));
        }

        public void ZeroScore()
        {
            this.score = 0;
            UpdateScore();
        }

        void UpdateScore()
        {
            uiText.text = string.Format("Score: {0}", Mathf.Floor(score));
        }
    }
}
