using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> musicAudioClips;
    public AudioSource audioSource;

    public const int MUSIC_CHILL = 0;
    public const int MUSIC_MID = 1;
    public const int MUSIC_FULERO = 2;

    void Start()
    {
        PlayMusicLoop(MUSIC_CHILL);
    }

    public void PlayMusicLoop(int musicIndex)
    {
        audioSource.clip = musicAudioClips[musicIndex];
        audioSource.loop = true;
        audioSource.Play();
    }
}
