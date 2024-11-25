using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public int healingAmount = 10; // ȸ����
    public float healingInterval = 1f; // ȸ�� ���� (��)

    private float nextHealTime = 0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������ ������ ��
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && Time.time >= nextHealTime)
            {
                nextHealTime = Time.time + healingInterval; // ���� ȸ�� �ð� ����
                playerHealth.Heal(healingAmount); // ü�� ȸ�� (������ Heal �޼��� ���)
            }
        }
    }
}
