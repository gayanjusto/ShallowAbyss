using GooglePlayGames;
using System;
using UnityEngine;

namespace Assets.Scripts.Services.SocialServices
{
    public static class GoogleGamePlayService
    {
        private static string leaderBoardId = "CgkIzevPya4DEAIQAQ";

        public static void LogIn()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
                if (!PlayerIsAuthenticated())
                    PlayGamesPlatform.Instance.Authenticate((bool success) => { });
        }

        public static void LogIn(Action callBack)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                    callBack();
            });
        }
        public static void ShowLeaderBoard()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderBoardId);
        }

        public static void PostScore(int score)
        {
            Social.ReportScore(score, leaderBoardId, (bool success) => { });
        }

        public static void PostScore(int score, Action callBack)
        {
            Social.ReportScore(score, leaderBoardId, (bool success) =>
            {
                if (success)
                    callBack();
            });
        }

        public static bool PlayerIsAuthenticated()
        {
            return PlayGamesPlatform.Instance.IsAuthenticated();
        }

        public static void LogOut()
        {
            PlayGamesPlatform.Instance.SignOut();
        }
    }
}
