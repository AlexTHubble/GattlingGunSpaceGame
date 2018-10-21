using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class SoundManagerScript : MonoBehaviour
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

        private AudioSource music;
        private AudioSource fx;

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

        public void playRandomBatmanFX(float volume)
        {
            int randIndex = Random.Range(0, batManFX.Count);

            fx.volume = volume;
            fx.clip = batManFX[randIndex];
            fx.Play();
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
