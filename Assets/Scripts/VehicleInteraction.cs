using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject vehicle;
    public Camera playerCamera;
    public Camera vehicleCamera;
    private bool isInVehicle = false; // ���� ž�� ����
    private bool isVehicleSpawned = false; // ���� ���� ����

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
    }

    void Update()
    {
        // F Ű�� ž�� �� ����
        if (Input.GetKeyDown(KeyCode.F) && isVehicleSpawned && IsNearVehicle())
        {
            ToggleVehicleState();
        }
    }

    public void SpawnVehicle(GameObject newVehicle)
    {
        // ���� ���� ����
        vehicle = newVehicle;
        isVehicleSpawned = true;

        // ���� ī�޶� ã��
        vehicleCamera = vehicle.GetComponentInChildren<Camera>();
        if (vehicleCamera != null)
        {
            vehicleCamera.enabled = false;
        }

        PrometeoCarController carController = vehicle.GetComponent<PrometeoCarController>();
        if (carController != null)
        {
            carController.enabled = false;
        }
    }

    bool IsNearVehicle()
    {
        if (vehicle == null || player == null) return false;

        // �÷��̾�� ���� �� �Ÿ� Ȯ��
        float distance = Vector3.Distance(player.transform.position, vehicle.transform.position);
        return distance < 3f; // 3 ���� �̳��� ���� F Ű�� ž�� ����
    }

    void ToggleVehicleState()
    {
        isInVehicle = !isInVehicle;

        if (isInVehicle)
        {
            // ���� ž�� ����
            player.SetActive(false);
            playerCamera.enabled = false;
            if (vehicleCamera != null) vehicleCamera.enabled = true;

            PrometeoCarController carController = vehicle.GetComponent<PrometeoCarController>();
            if (carController != null)
            {
                carController.enabled = true;
            }
        }
        else
        {
            // ���� ���� ����
            Vector3 exitPosition = vehicle.transform.position + (vehicle.transform.right * 2) + (Vector3.up * 1f);
            player.transform.position = exitPosition;

            player.SetActive(true);
            playerCamera.enabled = true;
            if (vehicleCamera != null) vehicleCamera.enabled = false;

            PrometeoCarController carController = vehicle.GetComponent<PrometeoCarController>();
            if (carController != null)
            {
                carController.enabled = false;
            }
        }
    }
}
