using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{

    public class PauseManager : MonoBehaviour
    {
        public GameObject pausePanel;
        public DashManager dashManager;
        public Button pauseButton;
        public AudioSource musicAudioSource;

        public bool isPaused;
        ScenesManager scenesManager;
        const int unpausedValue = 1;
        const int pausedValue = 0;

        private void Start()
        {
            scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
            pauseButton.gameObject.SetActive(true);
        }
        public void PauseGame()
        {

            if (isPaused)
            {
                UnpauseGame();
                return;
            }

            pausePanel.SetActive(true);
            Time.timeScale = pausedValue;
            isPaused = !isPaused;
            dashManager.DisableButtonInteraction();
            musicAudioSource.Pause();
        }

        public void UnpauseGame()
        {
            Time.timeScale = unpausedValue;

            //Remove the pause panel
            pausePanel.SetActive(false);

            isPaused = false;
            dashManager.EnableButtonInteraction();
            musicAudioSource.UnPause();
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = unpausedValue;
            scenesManager.LoadMainMenu();
        }


    }
}
