using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask enemyLayers;
    public LayerMask bossLayers;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;
    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }

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
        return Time.time >= lastAttackTime + attackCooldown;
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        int attackDamage = GetWeaponDamage();

        // 범위 내 적을 찾음
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);
        Collider[] hitBoss = Physics.OverlapSphere(transform.position, attackRange, bossLayers);

        // 슬라임 공격
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

    int GetWeaponDamage()
    {
        // 현재 무기 데미지 반환
        if (weaponManager != null && weaponManager.weapons.Count > weaponManager.currentWeaponIndex)
        {
            return weaponManager.weapons[weaponManager.currentWeaponIndex].damage;
        }
        return 10;
    }
}