using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Managers.ScenesManagers
{
    public class LanguageOptionSelectManager : MonoBehaviour
    {
        public ScenesManager scenesManager;
        public PlayerStatusManager playerStatusManager;


        public void SetCulture(string culture)
        {
            var playerData = PlayerStatusManager.PlayerDataInstance;
            playerData.SetCurrentLanguage(culture);

            playerStatusManager.SavePlayerStatus(playerData);

            LanguageService.ReloadDictionary();

            scenesManager.LoadMainMenu();
        }
    }
}
