using Firebase.Analytics;
using UnityEngine;

namespace Assets.Scripts.Services.FireBase
{
    public static class AnalyticsService
    {
        public static void LogEvent(string eventName)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return;

            FirebaseAnalytics.LogEvent(eventName);
        }

        public static void LogEvent(string eventName, string paramName, int value)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return;

            FirebaseAnalytics.LogEvent(eventName, paramName, value);
        }

        public static void LogEvent(string eventName, string paramName, double value)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return;

            FirebaseAnalytics.LogEvent(eventName, paramName, value);
        }
    }
}
