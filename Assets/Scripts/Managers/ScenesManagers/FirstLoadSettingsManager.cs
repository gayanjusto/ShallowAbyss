using Assets.Scripts.Services;
using System.Globalization;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FirstLoadSettingsManager : MonoBehaviour
    {

        public ScenesManager scenesManager;

        public GameObject englishBtn, portugueseBtn;
        public AudioSource musicAudioSource;

        private void Awake()
        {
            if (!AudioDataService.HasAudioData())
            {
                AudioDataService.CreateInitialLoad();
            }
        }

        private void Start()
        {
            if (AudioDataService.HasMusicOn())
            {
                musicAudioSource.Play();
            }

            if (PlayerStatusService.HasPlayerDataFile())
            {
                scenesManager.LoadMainMenu();
            }
            else
            {
                englishBtn.SetActive(true);
                portugueseBtn.SetActive(true);
            }
        }

        public void SetCulture(string culture)
        {
            var playerData = PlayerStatusService.LoadPlayerStatus();
            playerData.SetCurrentLanguage(culture);

            PlayerStatusService.SavePlayerStatus(playerData);

            scenesManager.LoadMainMenu();
        }
    }
}
