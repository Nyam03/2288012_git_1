using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;  // ���� �� �� ������
    public float attackRange = 2f; // ���� ����
    public LayerMask enemyLayers;  // ���� ���̾� ���� (�������� ���̾ ����)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� Ŭ�� �� ����
        {
            Attack();
        }
    }

    void Attack()
    {
        // ���� ���� ���� �ִ� ���� ã��
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);

        // ã�� ������ �������� ��
        foreach (Collider enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage); // ������ ����
            }
        }
    }

    // �ð������� ���� ������ �����ִ� �Լ� (���� �� Ȯ�ο�)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

