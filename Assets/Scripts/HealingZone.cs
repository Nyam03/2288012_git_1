using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public int healingAmount = 10; // 회복량
    public float healingInterval = 1f; // 회복 간격

    private float nextHealTime = 0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && Time.time >= nextHealTime)
            {
                nextHealTime = Time.time + healingInterval;
                playerHealth.Heal(healingAmount); // 체력 회복
            }
        }
    }
}
