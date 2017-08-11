using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadMainMenu()
    {
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
}
