using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Managers;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class LanguageService
    {
        static LanguageDictionary _ld;

        public static LanguageDictionary GetLanguageDictionary()
        {
            if(!_ld.isLoaded)
            {
                _ld = LoadDictionary();
            }

            return _ld;
        }

        public static void ReloadDictionary()
        {
            _ld = LoadDictionary();
        }
        static LanguageDictionary LoadDictionary()
        {
            var currentCulture = PlayerStatusManager.PlayerDataInstance.GetCurrentLanguage();
            LanguageDictionary ld = new LanguageDictionary();
            ld.isLoaded = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                string filePath = string.Format("{0}/Language_{1}.json", Application.streamingAssetsPath, currentCulture);

            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            //we have to remove 3 bytes from the reader head and parse it into string
            string jsonString;
            jsonString = System.Text.Encoding.UTF8.GetString(reader.bytes, 3, reader.bytes.Length - 3);

            ld = JsonUtility.FromJson<LanguageDictionary>(jsonString);
            ld.isLoaded = true;
            }
            return ld;
        }
        public static string GetTeste()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            LanguageDictionary ld = new LanguageDictionary();
            ld.isLoaded = false;
            try
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    string filePath = string.Format("{0}/Language_{1}.json", Application.streamingAssetsPath, currentCulture);

                    WWW reader = new WWW(filePath);
                    while (!reader.isDone) { }

                    return reader.text;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


            return "";
        }
    }
}
