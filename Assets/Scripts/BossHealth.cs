using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 300;
    private int currentHealth;

    public GameObject coinPrefab;
    public int coinCount = 10;

    public GameObject vehiclePrefab;
    public Transform vehicleSpawnPoint;
    private static bool isMountSpawned = false;

    public Slider healthBarUI;
    public Transform player;
    public float displayRange = 20f;

    // 보스 죽음 이벤트 정의
    public event Action OnBossDeath;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (healthBarUI != null)
        {
            healthBarUI.gameObject.SetActive(false);
        }

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (vehicleSpawnPoint == null)
        {
            GameObject spawnPointObject = GameObject.Find("VehicleSpawn");
            if (spawnPointObject != null)
            {
                vehicleSpawnPoint = spawnPointObject.transform;
            }
        }

        if (!IsSceneRestarted())
        {
            isMountSpawned = false;
        }
    }

    void Update()
    {
        CheckDisplayHealthBar();
    }

    void CheckDisplayHealthBar()
    {
        if (player == null || healthBarUI == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= displayRange && currentHealth > 0)
        {
            healthBarUI.gameObject.SetActive(true);
        }
        else
        {
            healthBarUI.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarUI != null)
        {
            healthBarUI.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (healthBarUI != null)
        {
            healthBarUI.gameObject.SetActive(false);
        }

        OnBossDeath?.Invoke();  // 보스가 죽었을 때 이벤트 호출
        Debug.Log("Boss Died");

        // 차량 생성
        if (!isMountSpawned && vehiclePrefab != null && vehicleSpawnPoint != null)
        {
            GameObject spawnedVehicle = Instantiate(vehiclePrefab, vehicleSpawnPoint.position, vehicleSpawnPoint.rotation);
            isMountSpawned = true;

            VehicleInteraction vehicleInteraction = FindObjectOfType<VehicleInteraction>();
            if (vehicleInteraction != null)
            {
                vehicleInteraction.SpawnVehicle(spawnedVehicle); // 차량 연결
                Debug.Log("Vehicle spawned and assigned to VehicleInteraction.");
            }
            else
            {
                Debug.LogError("VehicleInteraction script not found in the scene.");
            }
        }
        Destroy(gameObject);
    }

    bool IsSceneRestarted()
    {
        return Time.timeSinceLevelLoad < 1f; // 씬 시작 1초 이내일 경우
    }
}
