using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSoundManager : MonoBehaviour
{
    public AudioClip defaultEngineClip;
    private AudioSource engineSoundSource;

    private Rigidbody carRigidbody;
    private bool isInVehicle = false;

    private float initialEnginePitch = 1f;
    private float maxEnginePitch = 2f;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();

        // AudioSource 초기화
        engineSoundSource = gameObject.AddComponent<AudioSource>();
        engineSoundSource.loop = true;
        engineSoundSource.playOnAwake = false;
        engineSoundSource.volume = 0.15f;

        // 기본 클립 설정
        SetAudioClips(defaultEngineClip);
    }

    public void SetAudioClips(AudioClip engineClip)
    {
        if (engineClip != null)
        {
            engineSoundSource.clip = engineClip;
        }
    }

    void Update()
    {
        if (isInVehicle)
        {
            UpdateEngineSound();
        }
        else
        {
            StopAllSounds();
        }
    }

    public void SetVehicleState(bool inVehicle)
    {
        isInVehicle = inVehicle;

        if (inVehicle)
        {
            StartEngineSound();
        }
        else
        {
            StopAllSounds();
        }
    }

    private void StartEngineSound()
    {
        if (engineSoundSource != null && engineSoundSource.clip != null)
        {
            engineSoundSource.Play();
        }
    }

    private void UpdateEngineSound()
    {
        if (engineSoundSource != null && engineSoundSource.isPlaying)
        {
            float speed = carRigidbody.velocity.magnitude;
            engineSoundSource.pitch = Mathf.Clamp(initialEnginePitch + (speed / 50f), initialEnginePitch, maxEnginePitch);
        }
    }

    private void StopAllSounds()
    {
        if (engineSoundSource != null)
        {
            engineSoundSource.Stop();
        }
    }
}
