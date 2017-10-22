using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using Assets.Scripts.Services.SocialServices;
using UnityEngine.UI;
using Assets.Scripts.Services;

namespace Assets.Scripts.Managers.SocialServices
{
    public class SocialManager : MonoBehaviour
    {
        public ScoreManager scoreManager;
        public Button socialBtn;

        private void Start()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                NetworkConnectionService.AttemptToActivateConnection();
            }

            if (Application.internetReachability != NetworkReachability.NotReachable
                && GoogleGamePlayService.PlayerIsAuthenticated())
            {
                socialBtn.gameObject.SetActive(true);
                PlayGamesPlatform.Activate();
            }
        }

        public void PostScoreAndShowLeaderBoard()
        {
            var finalScore = scoreManager.GetFinalScore();
            if(finalScore < HighScoreService.GetHighestScore())
            {
                finalScore = HighScoreService.GetHighestScore();
            }

            GoogleGamePlayService.PostScore(finalScore, () => ShowLeaderBoard());
        }

        public void ShowLeaderBoard()
        {
            GoogleGamePlayService.ShowLeaderBoard();
        }
    }
}
