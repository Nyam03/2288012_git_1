using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject vehicle;
    public Camera playerCamera;
    public Camera vehicleCamera;
    private bool isInVehicle = false; // 차량 탑승 상태
    private bool isVehicleSpawned = false; // 차량 스폰 여부

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (vehicleCamera != null)
        {
            AudioListener vehicleAudioListener = vehicleCamera.GetComponent<AudioListener>();
            if (vehicleAudioListener != null)
            {
                vehicleAudioListener.enabled = false;
            }
        }
    }

    void Update()
    {
        // F 키로 탑승 및 하차
        if (Input.GetKeyDown(KeyCode.F) && isVehicleSpawned && IsNearVehicle())
        {
            ToggleVehicleState();
        }
    }

    public void SpawnVehicle(GameObject newVehicle)
    {
        // 차량 생성 로직
        vehicle = newVehicle;
        isVehicleSpawned = true;

        // 차량 카메라 찾기
        vehicleCamera = vehicle.GetComponentInChildren<Camera>();
        if (vehicleCamera != null)
        {
            vehicleCamera.enabled = false;
            AudioListener vehicleAudioListener = vehicleCamera.GetComponent<AudioListener>();
            if (vehicleAudioListener != null)
            {
                vehicleAudioListener.enabled = false;
            }
        }

        PrometeoCarController carController = vehicle.GetComponent<PrometeoCarController>();
        if (carController != null)
        {
            carController.enabled = false;
        }

        VehicleSoundManager soundManager = vehicle.GetComponent<VehicleSoundManager>();
        if (soundManager != null)
        {
            AudioClip engineClip = Resources.Load<AudioClip>("Sounds/EngineSound");

            soundManager.SetAudioClips(engineClip);
        }
    }

    bool IsNearVehicle()
    {
        if (vehicle == null || player == null) return false;

        // 차량 근처 3m 이내, 탑승 중이면 true
        float distance = Vector3.Distance(player.transform.position, vehicle.transform.position);
        return isInVehicle || distance < 3f;
    }

    void ToggleVehicleState()
    {
        isInVehicle = !isInVehicle;
        VehicleSoundManager soundManager = vehicle.GetComponent<VehicleSoundManager>();

        if (isInVehicle)
        {
            // 차량 탑승 상태
            player.SetActive(false);
            playerCamera.enabled = false;

            if (vehicleCamera != null)
            {
                vehicleCamera.enabled = true;
                AudioListener vehicleAudioListener = vehicleCamera.GetComponent<AudioListener>();
                if (vehicleAudioListener != null)
                {
                    vehicleAudioListener.enabled = true; // 차량 카메라의 Audio Listener 활성화
                }
            }

            soundManager?.SetVehicleState(true);

            PrometeoCarController carController = vehicle.GetComponent<PrometeoCarController>();
            if (carController != null)
            {
                carController.enabled = true;
            }
        }
        else
        {
            // 차량 하차 상태
            Vector3 exitPosition = vehicle.transform.position + (vehicle.transform.right * 2) + (Vector3.up * 1f);
            player.transform.position = exitPosition;

            player.SetActive(true);
            playerCamera.enabled = true;

            if (vehicleCamera != null)
            {
                vehicleCamera.enabled = false;
                AudioListener vehicleAudioListener = vehicleCamera.GetComponent<AudioListener>();
                if (vehicleAudioListener != null)
                {
                    vehicleAudioListener.enabled = false; // 차량 카메라의 Audio Listener 비활성화
                }
            }

            soundManager?.SetVehicleState(false);

            PrometeoCarController carController = vehicle.GetComponent<PrometeoCarController>();
            if (carController != null)
            {
                carController.enabled = false;
            }
        }
    }
}
