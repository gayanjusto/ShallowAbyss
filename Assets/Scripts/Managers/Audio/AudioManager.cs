﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Managers.Audio
{
    public class AudioManager : MonoBehaviour
    {
        AudioSource audioSource;

        private void Start()
        {

            DontDestroyOnLoad(this);
            audioSource = GetComponent<AudioSource>();
            PlayMusic();
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }

        public void PlayMusic()
        {
            audioSource.Play();
        }

    }
}