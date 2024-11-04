using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealth : MonoBehaviour
{
    public int maxHealth = 30;       // �ִ� ü��
    private int currentHealth;        // ���� ü��
    public int coinCount = 1;         //���� ����

    public GameObject coinPrefab;
    public List<Image> hearts;

    void Start()
    {
        currentHealth = maxHealth;   // ������ �� �ִ� ü������ �ʱ�ȭ
        UpdateHearts();              // �ʱ� ��Ʈ UI ������Ʈ
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
        UpdateHearts();              // ��Ʈ UI ������Ʈ
    }

    // ������ ��Ʈ UI ������Ʈ
    void UpdateHearts()
    {
        int activeHearts = Mathf.CeilToInt(currentHealth / 10f);  // ü�� 10�� ��Ʈ 1�� Ȱ��ȭ

        // ��� ��Ʈ�� ��Ȱ��ȭ�� ��, ���� ü�¿� �°� Ȱ��ȭ
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < activeHearts)
            {
                hearts[i].gameObject.SetActive(true);   // �ʿ��� ��Ʈ�� Ȱ��ȭ
            }
            else
            {
                hearts[i].gameObject.SetActive(false);  // ������ ��Ʈ�� ��Ȱ��ȭ
            }
        }
    }

    // ĳ���Ͱ� �׾��� �� ó��
    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        //������ ���� ����
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 coinPosition = transform.position + new Vector3(0, 1f, 0);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }
        Destroy(gameObject);  // ������Ʈ ���� �Ǵ� ��Ȱ��ȭ
    }
}
