using UnityEngine;

namespace Assets.Scripts.Managers.Player
{
    public class PlayerShipGameManager : PlayerShipManager
    {
        public AudioSource musicAudioSource;

        protected override void Start()
        {
            base.Start();
            ChangeMusic();
        }

        void ChangeMusic()
        {
            var ship = base.GetAttachedShipScript();

            if (!string.IsNullOrEmpty(ship.musicPath))
            {
                var music = Resources.Load<AudioClip>(ship.musicPath);
                musicAudioSource.clip = music;
                musicAudioSource.Play();
            }

        }
    }
}
