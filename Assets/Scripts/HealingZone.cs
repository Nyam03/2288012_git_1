using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public int healingAmount = 10; // 회복량
    public float healingInterval = 1f; // 회복 간격 (초)

    private float nextHealTime = 0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 구역에 들어왔을 때
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && Time.time >= nextHealTime)
            {
                nextHealTime = Time.time + healingInterval; // 다음 회복 시간 갱신
                playerHealth.Heal(healingAmount); // 체력 회복 (수정된 Heal 메서드 사용)
            }
        }
    }
}
