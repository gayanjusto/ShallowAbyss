using System.Globalization;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class LanguageSelectorManager : MonoBehaviour
    {

        public ScenesManager scenesManager;

        public void SetCulture(string culture)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            scenesManager.LoadMainMenu();
        }
    }
}
