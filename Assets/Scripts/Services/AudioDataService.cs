using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class AudioDataService
    {
        static string audioDataFilePath
        {
            get
            {
                return Application.persistentDataPath + "/audioData.dat";
            }
        }

        static void CheckAudioTypeOn(AudioSource[] sources, Func<bool> checkMethod)
        {
            if (checkMethod.Invoke())
            {
                for (int i = 0; i < sources.Length; i++)
                {
                    sources[i].enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < sources.Length; i++)
                {
                    sources[i].enabled = false;
                }
            }
        }

        public static void SetMusic(bool musicStatus)
        {
            var appReader = new ApplicationDataReader<AudioData>();
            var data = appReader.LoadData(audioDataFilePath);
            data.isMusicOn = musicStatus;
            appReader.SaveData(data, audioDataFilePath);
        }

        public static void SetSfx(bool sfxStatus)
        {
            var appReader = new ApplicationDataReader<AudioData>();
            var data = appReader.LoadData(audioDataFilePath);

            data.isSfxOn = sfxStatus;
            appReader.SaveData(data, audioDataFilePath);
        }

        public static bool HasMusicOn()
        {
            var appReader = new ApplicationDataReader<AudioData>();
            var data = appReader.LoadData(audioDataFilePath);


            return data.isMusicOn;
        }

        public static bool HasSfxOn()
        {
            var appReader = new ApplicationDataReader<AudioData>();
            var data = appReader.LoadData(audioDataFilePath);

            return data.isSfxOn;
        }

        public static void CheckMusicOn(AudioSource[] musicSources)
        {
            CheckAudioTypeOn(musicSources, HasMusicOn);
        }

        public static void CheckSfxOn(AudioSource[] sfxSources)
        {
            CheckAudioTypeOn(sfxSources, HasSfxOn);
        }

        public static void CreateInitialLoad()
        {
            var appReader = new ApplicationDataReader<AudioData>();
            AudioData data = new AudioData() { isMusicOn = true, isSfxOn = true };
            appReader.SaveData(data, audioDataFilePath);
        }

        public static bool HasAudioData()
        {
            var appReader = new ApplicationDataReader<AudioData>();

            return appReader.LoadData(audioDataFilePath) != null;
        }
    }
}
