﻿using System.IO;
using System.Net;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class NetworkConnectionService
    {
        static string GetHtmlFromUri(string resource)
        {
            string html = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
            try
            {
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                    if (isSuccess)
                    {
                        using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                        {
                            //We are limiting the array to 80 so we don't have
                            //to parse the entire html document feel free to 
                            //adjust (probably stay under 300)
                            char[] cs = new char[10];
                            reader.Read(cs, 0, cs.Length);
                            foreach (char ch in cs)
                            {
                                html += ch;
                            }
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            return html;
        }

        static string RequestConn()
        {
            return GetHtmlFromUri("http://www.google.com");
        }
        public static bool HasInternetConnection()
        {
            return  RequestConn() != string.Empty;
        }
    }
}
