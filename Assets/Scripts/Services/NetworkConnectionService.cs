using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class NetworkConnectionService
    {
        static IEnumerator CheckNetworkAccess(Action<bool> callBack)
        {
            WWW www = new WWW("http://google.com");
            yield return www;
            if (www.error != null)
            {
                callBack(true);
            }
            else
            {
                callBack(true);
            }
        }


        public static bool HasNetwork()
        {

            bool result = false;
            CheckNetworkAccess((isConnected) => {
                result = isConnected;
            });

            return result;
        }
    }
}
