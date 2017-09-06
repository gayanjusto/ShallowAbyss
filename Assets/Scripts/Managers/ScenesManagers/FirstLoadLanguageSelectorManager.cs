using System.Globalization;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FirstLoadLanguageSelectorManager : MonoBehaviour
    {

        public ScenesManager scenesManager;
        public PlayerStatusManager playerStatusManager;

        public GameObject englishBtn, portugueseBtn;

        private void Start()
        {
            if (PlayerStatusManager.HasPlayerDataFile())
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
            var playerData = PlayerStatusManager.PlayerDataInstance;
            playerData.SetCurrentLanguage(culture);

            playerStatusManager.SavePlayerStatus(playerData);

            scenesManager.LoadMainMenu();
        }
    }
}
