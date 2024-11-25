using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;       // �ִ� ü��
    private int currentHealth;        // ���� ü��

    public Text healthText;           // ü�� ǥ�ø� ���� UI �ؽ�Ʈ

    void Start()
    {
        currentHealth = maxHealth;   // ������ �� �ִ� ü������ �ʱ�ȭ
        UpdateHealthUI();            // �ʱ� UI ������Ʈ
    }

    public void Heal(int amount)
    {
        currentHealth += amount;    // ȸ������ŭ ü�� ����
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // �ִ� ü���� �ʰ����� �ʵ��� ����
        }
        UpdateHealthUI();            // ü�� ���� �� UI ������Ʈ
    }

    // ü�� ���� �Լ�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;    // ��������ŭ ü�� ����
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();                  // ü���� 0 ������ �� ���� ó��
        }
        UpdateHealthUI();            // ü�� ���� �� UI ������Ʈ
    }

    // ü�� UI �ؽ�Ʈ ������Ʈ �Լ�
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();  // �ؽ�Ʈ�� ���� ü�� �ݿ�
        }
    }

    // ĳ���Ͱ� �׾��� �� ó��
    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);  // ������Ʈ ���� �Ǵ� ��Ȱ��ȭ
    }
}
