using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.ScenesManagers
{
    public class SettingsManager : MonoBehaviour
    {
        public ScenesManager scenesManager;
        public Image audioBtnImage;

        public Sprite[] audioSprites;

        public void SetCulture(string culture)
        {
            var playerData = PlayerStatusService.LoadPlayerStatus();
            playerData.SetCurrentLanguage(culture);

            PlayerStatusService.SavePlayerStatus(playerData);

            LanguageService.ReloadDictionary();

            scenesManager.LoadMainMenu();
        }

        public void ChangeAudio()
        {
            if (AudioListener.volume == 0)
                AudioListener.volume = 1;
            else
                AudioListener.volume = 0;

            audioBtnImage.sprite = audioSprites[(int)AudioListener.volume];
        }
    }
}
