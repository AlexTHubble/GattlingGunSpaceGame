using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class SoundManagerScript : Singleton<SoundManagerScript>
    {
        [SerializeField]
        List<AudioClip> musicList;
        [SerializeField]
        List<AudioClip> FXList;
        [SerializeField]
        List<AudioClip> batManFX;
        [SerializeField]
        bool muteMusic = false;
        [SerializeField]
        float musicVolume = 1;
        [SerializeField]
        AudioClip defaultSong;
        [SerializeField]
        bool loopSong = true;
        [SerializeField]
        bool startSongOnStart = true;

        [SerializeField]
        float fxDelay = 0.5f;

        bool delayOver = false;
        float currentDelayTime = 0f;

        private AudioSource music;
        private AudioSource fx;
        private AudioSource delayedFx;
        private AudioSource batman;

        void Awake()
        {
            onLoadFunction();
            if (startSongOnStart)
            {
                if (loopSong)
                    music.loop = true;

                music.clip = defaultSong;
                music.Play();
            }

        }

        private void OnLevelWasLoaded(int level)
        {
            onLoadFunction();
        }

        void onLoadFunction()
        {
            var audioSources = GetComponents<AudioSource>();
            music = audioSources[0];
            fx = audioSources[1];
            delayedFx = audioSources[2];
            batman = audioSources[3];
            music.volume = musicVolume;
        }


        public void setSong(string songToSet)
        {
            AudioClip song = findSong(songToSet);
            if (song != null)
            {
                music.clip = song;
            }
        }

        public void playFX(string fxToPlay)
        {

            AudioClip newFX = findFX(fxToPlay);

            delayOver = true;

            if (newFX != null)
            {
                fx.volume = 1f;
                fx.clip = newFX;
                fx.Play();
            }

        }

        public void playFX(string fxToPlay, float volume)
        {
            AudioClip newFX = findFX(fxToPlay);

            if (newFX != null)
            {
                fx.volume = volume;
                fx.clip = newFX;
                fx.Play();
            }
        }

        public void playFXWithDelay(string fxToPlay)
        {
            if (!delayOver)
            {
                AudioClip newFX = findFX(fxToPlay);

                delayOver = true;
                currentDelayTime = Time.time + fxDelay;

                if (newFX != null)
                {
                    delayedFx.volume = 1f;
                    delayedFx.clip = newFX;
                    delayedFx.Play();
                }
            }
            else if (currentDelayTime <= Time.time)
            {
                delayOver = false;
            }

        }

        public void playFxWithDelay(string fxToPlay, float volume)
        {

            if (!delayOver)
            {
                AudioClip newFX = findFX(fxToPlay);

                delayOver = true;
                currentDelayTime = Time.time + fxDelay;

                if (newFX != null)
                {
                    delayedFx.volume = volume;
                    delayedFx.clip = newFX;
                    delayedFx.Play();
                }
            }
            else if (currentDelayTime <= Time.time)
            {
                delayOver = false;
            }
        }


        public void playRandomBatmanFX(float volume)
        {
            int randIndex = Random.Range(0, batManFX.Count);

            batman.volume = volume;
            batman.clip = batManFX[randIndex];
            batman.Play();
        }

        public void playRandomBatmanFX()
        {
            int randIndex = Random.Range(0, batManFX.Count);

            Debug.Log(randIndex);

            batman.clip = batManFX[randIndex];
            batman.Play();
        }

        private AudioClip findSong(string key)
        {
            foreach (AudioClip song in musicList)
            {
                if (song.name == key)
                    return song;
            }

            Debug.Log("Invalid song, setting to default...");
            return defaultSong;
        }

        private AudioClip findFX(string key)
        {
            foreach (AudioClip song in FXList)
            {
                if (song.name == key)
                    return song;
            }

            Debug.Log("Invalid FX, " + key + " not found");
            return null;
        }


    }

}
