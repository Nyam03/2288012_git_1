using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;  // 공격 시 줄 데미지
    public float attackRange = 2f; // 공격 범위
    public LayerMask enemyLayers;  // 적의 레이어 설정 (슬라임의 레이어를 지정)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 시 공격
        {
            Attack();
        }
    }

    void Attack()
    {
        // 공격 범위 내에 있는 적을 찾음
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);

        // 찾은 적에게 데미지를 줌
        foreach (Collider enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage); // 데미지 적용
            }
        }
    }

    // 시각적으로 공격 범위를 보여주는 함수 (개발 중 확인용)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

