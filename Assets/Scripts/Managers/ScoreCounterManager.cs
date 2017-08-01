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

            uiText.text = string.Format("Score: {0}", Mathf.Floor(score));
        }

    
    }
}
