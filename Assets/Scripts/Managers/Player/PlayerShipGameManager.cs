using UnityEngine;

namespace Assets.Scripts.Managers.Player
{
    public class PlayerShipGameManager : PlayerShipManager
    {
        public AudioSource musicAudioSource;
        public SpriteRenderer dummyDeadPlayerSpriteRenderer;
        protected override void Start()
        {
            base.Start();
            ChangeMusic();
            SetDummyDeadPlayerSprite();
        }

        void SetDummyDeadPlayerSprite()
        {
            dummyDeadPlayerSpriteRenderer.sprite = GetAttachedShipScript().shipImage;
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
