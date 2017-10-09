using Assets.Scripts.Managers.Audio;
using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.ScenesManagers
{
    public class SettingsManager : MonoBehaviour
    {
        public ScenesManager scenesManager;
        public Image audioBtnImage;
        public AudioSource selectAudioSource;
        public AudioSourcesManager audioSourcesManager;
        public Button musicBtn, sfxBtn;

        private void Start()
        {
            ChangeButton(musicBtn, !AudioDataService.HasMusicOn());
            ChangeButton(sfxBtn, !AudioDataService.HasSfxOn());
        }

        void ChangeButton(Button btn, bool status)
        {
            btn.transform.GetChild(0).gameObject.SetActive(status);
        }
        public void SetCulture(string culture)
        {
            selectAudioSource.Play();

            var playerData = PlayerStatusService.LoadPlayerStatus();
            playerData.SetCurrentLanguage(culture);

            PlayerStatusService.SavePlayerStatus(playerData);

            LanguageService.ReloadDictionary();

            scenesManager.LoadMainMenu();
        }

        public void ChangeMusic()
        {
            var musicAudioSource = audioSourcesManager.GetMainMusic();
            if (AudioDataService.HasMusicOn())
            {
                ChangeButton(musicBtn, true);
                AudioDataService.SetMusic(false);
            }
            else
            {
                ChangeButton(musicBtn, false);
                AudioDataService.SetMusic(true);
            }
            AudioDataService.CheckMusicOn(new[] { musicAudioSource });
            selectAudioSource.Play();

        }

        public void ChangeSfx()
        {
            if (AudioDataService.HasSfxOn())
            {
                ChangeButton(sfxBtn, true);
                AudioDataService.SetSfx(false);
            }
            else
            {
                ChangeButton(sfxBtn, false);
                AudioDataService.SetSfx(true);
            }
            AudioDataService.CheckSfxOn(audioSourcesManager.GetSfxSources());
            selectAudioSource.Play();
        }
    }
}
