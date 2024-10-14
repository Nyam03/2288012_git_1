using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public int attackDamage = 5;  // 슬라임이 줄 데미지
    public float attackCooldown = 1f; // 공격 간격
    private float lastAttackTime = 0f;

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 대상이 플레이어라면
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null && Time.time >= lastAttackTime + attackCooldown)
            {
                playerHealth.TakeDamage(attackDamage); // 플레이어에게 데미지
                lastAttackTime = Time.time; // 마지막 공격 시간 기록
            }
        }
    }
}

