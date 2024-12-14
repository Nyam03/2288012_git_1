using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeSound : MonoBehaviour
{
    public AudioClip underwater;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.3f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySubmergedSound();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopSubmergedSound();
        }
    }

    void PlaySubmergedSound()
    {
        if (underwater != null && audioSource != null)
        {
            audioSource.clip = underwater;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopSubmergedSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
