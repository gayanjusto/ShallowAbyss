using UnityEngine;

namespace Assets.Scripts.Managers
{

    public class PauseManager : MonoBehaviour
    {
        public GameObject pausePanel;

        public float originalTimeScale;
        ScenesManager scenesManager;

        private void Start()
        {
            scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        }
        public void PauseGame()
        {
            pausePanel.SetActive(true);
            originalTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void UnpauseGame()
        {
            Time.timeScale = originalTimeScale;

            //Remove the pause panel
            pausePanel.SetActive(false);
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = originalTimeScale;
            scenesManager.LoadMainMenu();
        }
    }
}
