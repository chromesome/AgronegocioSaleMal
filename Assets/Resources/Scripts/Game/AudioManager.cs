using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> fireAudioClips;

    const int FIRE_LEVEL_1 = 1;
    const int FIRE_LEVEL_2 = 3;
    const int FIRE_LEVEL_3 = 5;
    const int FIRE_LEVEL_4 = 8;

    public AudioSource audioSource;
    GameObject[] fireObjects;
    int currentFires;

    void Start()
    {
        audioSource.loop = true;
        currentFires = 0;
        InvokeRepeating("CheckForNewFires", 0f, 1f);
    }

    public void CheckForNewFires()
    {
        fireObjects = GameObject.FindGameObjectsWithTag("Fire");
        if (fireObjects.Length != currentFires)
        {
            currentFires = fireObjects.Length;
            UpdateFireLoopSound();
        }
    }

    public void UpdateFireLoopSound()
    {
        bool playFire = false;
        if (fireObjects.Length >= FIRE_LEVEL_4)
        {
            audioSource.clip = fireAudioClips[3];
            playFire = true;
        }
        else if (fireObjects.Length >= FIRE_LEVEL_3)
        {
            audioSource.clip = fireAudioClips[2];
            playFire = true;
        }
        else if (fireObjects.Length >= FIRE_LEVEL_2)
        {
            audioSource.clip = fireAudioClips[1];
            playFire = true;
        }
        else if (fireObjects.Length >= FIRE_LEVEL_1)
        {
            audioSource.clip = fireAudioClips[0];
            playFire = true;
        }
        else if (fireObjects.Length <= 0)
        {
            playFire = false;
        }
        if (playFire == true)
        {
            audioSource.Play();
        }
        else if (audioSource.isPlaying == true)
        {
            audioSource.Stop();
        }
    }
}
