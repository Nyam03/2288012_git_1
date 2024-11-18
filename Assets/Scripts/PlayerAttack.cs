using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 2f; //공격 범위
    public LayerMask enemyLayers;
    public LayerMask bossLayers;
    public float attackCooldown = 1f; // 공격 쿨다운
    private float lastAttackTime = 0f; // 마지막 공격 시간

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack())
            {
                Attack();
            }
        }
    }

    bool CanAttack()
    {
        // 현재 시간이 마지막 공격 시간 + 쿨다운 이후인지 확인
        return Time.time >= lastAttackTime + attackCooldown;
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        //범위 내 적을 찾음
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);
        Collider[] hitBoss = Physics.OverlapSphere(transform.position, attackRange, bossLayers);

        //슬라임 공격
        foreach (Collider enemy in hitEnemies)
        {
            SlimeHealth enemyHealth = enemy.GetComponent<SlimeHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
        foreach (Collider boss in hitBoss)
        {
            BossHealth bossHealth = boss.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
            }
        }
    }
}