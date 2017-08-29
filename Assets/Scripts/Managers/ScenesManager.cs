using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    static ScenesManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }
    public void LoadMainMenu()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadShipSelection()
    {
        SceneManager.LoadScene("ShipSelector");
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("LabScene");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LoadLanguageMenu()
    {
        SceneManager.LoadScene("LanguageSelection");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
