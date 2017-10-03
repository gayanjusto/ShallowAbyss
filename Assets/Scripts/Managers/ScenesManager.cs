using Assets.Scripts.Services.AdMob;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    static ScenesManager instance;
    GameObject audioManager;
    static AudioSource selectAudioSource;

    private void Awake()
    {
        selectAudioSource = GameObject.Find("SelectAudio").GetComponent<AudioSource>();
        selectAudioSource.clip.LoadAudioData();
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }

    }
    public void LoadMainMenu()
    {
        PlaySelectAudio();

        RemoveAds();
        StopAllCoroutines();
        PlayMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadShipSelection()
    {
        PlaySelectAudio();

        SceneManager.LoadScene("ShipSelector");
    }

    public void LoadNewGame()
    {
        PlaySelectAudio();

        RemoveAds();

        StopMusic();

        SceneManager.LoadScene("LabScene");
    }

    public void LoadShop()
    {
        PlaySelectAudio();

        SceneManager.LoadScene("Shop");
    }

    public void LoadSettingsMenu()
    {
        PlaySelectAudio();

        SceneManager.LoadScene("LanguageSelection");
    }

    public void LoadJackpot()
    {
        PlaySelectAudio();

        SceneManager.LoadScene("Jackpot");
    }

    public void Quit()
    {
        PlaySelectAudio();

        Application.Quit();
    }

    void PlayMusic()
    {
        if (!audioManager)
        {
            audioManager = GameObject.Find("AudioManager");
        }
        AudioSource audioSource = audioManager.GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
            audioManager.GetComponent<AudioSource>().Play();
    }

    void StopMusic()
    {
        if (!audioManager)
        {
            audioManager = GameObject.Find("AudioManager");
        }
        audioManager.GetComponent<AudioSource>().Stop();
    }

    void PlaySelectAudio()
    {
        if (selectAudioSource == null)
        {
            selectAudioSource = GameObject.Find("SelectAudio").GetComponent<AudioSource>();
        }
        selectAudioSource.Play();
    }

    void RemoveAds()
    {
        AdMobService.RemoveBannerAd();
    }
}
