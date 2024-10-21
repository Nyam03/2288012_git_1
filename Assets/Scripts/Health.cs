using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;       // �ִ� ü��
    private int currentHealth;        // ���� ü��

    public Text healthText;           // ü�� ǥ�ø� ���� UI �ؽ�Ʈ
    public Image heartImagePrefab;    // ��Ʈ UI �̹��� (Image ������Ʈ)
    public Transform heartContainer;  // ��Ʈ���� ǥ�õ� �θ� ������Ʈ (World Space Canvas)

    private List<Image> hearts = new List<Image>();  // ��Ʈ �̹��� ����Ʈ

    void Start()
    {
        currentHealth = maxHealth;   // ������ �� �ִ� ü������ �ʱ�ȭ

        // �����̳ʰ� null�� ���, ������ �ڽĿ��� Canvas�� �ڵ����� ã��
        if (heartContainer == null)
        {
            heartContainer = GetComponentInChildren<Canvas>().transform;
            if (heartContainer == null)
            {
                Debug.LogError("No Canvas found for health hearts!");
            }
        }

        UpdateHealthUI();            // �ʱ� UI ������Ʈ
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
        UpdateHealthUI();            // ü�� ���� �� UI ������Ʈ
        UpdateHearts();              // ��Ʈ UI ������Ʈ
    }

    // ü�� UI �ؽ�Ʈ ������Ʈ �Լ�
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();  // �ؽ�Ʈ�� ���� ü�� �ݿ�
        }
    }

    // ��Ʈ UI ������Ʈ �Լ�
    void UpdateHearts()
    {
        if (heartImagePrefab == null)
        {
            Debug.LogError("heartImagePrefab is not assigned!");
            return;
        }
        if (heartContainer == null)
        {
            Debug.LogError("heartContainer is not assigned!");
            return;
        }

        // ���� ��Ʈ �̹��� ����
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        // ���� ü�¿� �°� ��Ʈ ���� (10�� ��Ʈ 1��)
        int heartCount = Mathf.CeilToInt(currentHealth / 10f);
        for (int i = 0; i < heartCount; i++)
        {
            Image heart = Instantiate(heartImagePrefab, heartContainer);
            hearts.Add(heart);  // ��Ʈ ����Ʈ�� �߰�
            Debug.Log("Heart instantiated successfully.");
        }
    }

    // ĳ���Ͱ� �׾��� �� ó��
    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);  // ������Ʈ ���� �Ǵ� ��Ȱ��ȭ
    }
}
