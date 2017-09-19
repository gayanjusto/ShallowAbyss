using System.Globalization;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FirstLoadLanguageSelectorManager : MonoBehaviour
    {

        public ScenesManager scenesManager;

        public GameObject englishBtn, portugueseBtn;

        private void Start()
        {
            if (PlayerStatusService.HasPlayerDataFile())
            {
                scenesManager.LoadMainMenu();
            }else
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
