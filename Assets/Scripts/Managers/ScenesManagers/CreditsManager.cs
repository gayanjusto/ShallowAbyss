using UnityEngine;

namespace Assets.Scripts.Managers.ScenesManagers
{
    public class CreditsManager : MonoBehaviour
    {
        public GameObject creditsPanel;

        public void ShowCredits()
        {
            creditsPanel.SetActive(true);
        }

        public void HideCredits()
        {
            creditsPanel.SetActive(false);
        }
    }
}
