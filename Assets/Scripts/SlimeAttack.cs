using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public int attackDamage = 5;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    void OnCollisionEnter(Collision collision)
    {
        //�÷��̾� ����
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null && Time.time >= lastAttackTime + attackCooldown)
            {
                playerHealth.TakeDamage(attackDamage);
                lastAttackTime = Time.time; //���� �ð� ���
            }
        }
    }
}

