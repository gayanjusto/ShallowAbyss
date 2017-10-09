using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Managers.Audio
{
    public class AudioSourcesManager : MonoBehaviour
    {
        public AudioSource[] musicSources;
        public AudioSource[] sfxSources;

        private void Start()
        {
            AudioDataService.CheckMusicOn(musicSources);
            AudioDataService.CheckSfxOn(sfxSources);
        }

        public AudioSource[] GetSfxSources()
        {
            return sfxSources;
        }
        public AudioSource GetMainMusic()
        {
            return GameObject.Find("MainMusicAudioManager").GetComponent<AudioSource>();
        }
    }
}
