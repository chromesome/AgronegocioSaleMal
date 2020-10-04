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
    }

    public void CheckForNewFires()
    {
        Debug.Log("SOnido fuego!!!");
        fireObjects = GameObject.FindGameObjectsWithTag("Fire");
        Debug.Log("Que pasa aca? fireObjects = " + fireObjects.Length);
        if (fireObjects.Length != currentFires)
        {
            Debug.Log("sdfgkjhadfgjklsdfg");
            currentFires = fireObjects.Length;
            UpdateFireLoopSound();
        }
    }

    public void UpdateFireLoopSound()
    {
        Debug.Log("SOnido fuego!!! UpdateFireLoopSound");
        if (fireObjects.Length >= FIRE_LEVEL_4)
        {
            audioSource.clip = fireAudioClips[3];
        }
        else if (fireObjects.Length >= FIRE_LEVEL_3)
        {
            audioSource.clip = fireAudioClips[2];
        }
        else if (fireObjects.Length >= FIRE_LEVEL_2)
        {
            audioSource.clip = fireAudioClips[1];
        }
        else if (fireObjects.Length >= FIRE_LEVEL_1)
        {
            audioSource.clip = fireAudioClips[0];
        }
        else if (fireObjects.Length <= 0)
        {
            Debug.Log("Stop Llega aca ???");
            audioSource.Stop();
        }
        audioSource.Play();
    }
}
