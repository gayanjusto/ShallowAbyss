using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public float score;
        public Text uiText;
        public GameObject playerObj;
        public Vector3 redZoneStart;
        public PauseManager pauseManager;
        bool playerInRedZone;

        float previousScoreVal;
        float finalScore;

        private void Start()
        {
            Vector3 screenBottom = ScreenPositionService.GetBottomEdge(Camera.main);
            redZoneStart = new Vector3(screenBottom.x, 0, screenBottom.z);
            UpdateScore();
        }

        private void Update()
        {
            if (!pauseManager.isPaused)
            {
                if (playerObj.transform.position.y <= redZoneStart.y)
                {
                    playerInRedZone = true;
                }
                else
                {
                    playerInRedZone = false;
                }

                if (!playerInRedZone)
                {

                    score += .005f;
                }
                else
                {
                    score += Time.deltaTime;
                }

                if (Mathf.Floor(score) > previousScoreVal)
                    UpdateScore();
            }
        }

        public void IncreaseScore(int amount)
        {
            this.score += amount;
            UpdateScore();
        }
        public void DecreaseScore()
        {
            score--;
            uiText.text = string.Format("{0}", Mathf.Floor(score));
        }

        public void ZeroScore()
        {
            this.score = 0;
            UpdateScore();
        }

        void UpdateScore()
        {
            previousScoreVal = Mathf.Floor(score);
            uiText.text = string.Format("{0}", Mathf.Floor(previousScoreVal));
        }

        public bool PlayerInRedZone()
        {
            return playerInRedZone;
        }

        public int FinishAndGetFinalScore()
        {
            finalScore = score;
            return Mathf.FloorToInt(finalScore);
        }

        public int GetFinalScore()
        {
            return Mathf.FloorToInt(finalScore);
        }
    }
}
