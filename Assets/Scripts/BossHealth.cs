using UnityEngine;
using UnityEngine.UI;
using System;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 300;
    private int currentHealth;

    public Slider healthBarUI;
    public Transform player;
    public float displayRange = 20f;

    // 보스 죽음 이벤트 정의
    public event Action OnBossDeath;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        healthBarUI.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckDisplayHealthBar();
    }

    void CheckDisplayHealthBar()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= displayRange)
        {
            healthBarUI.gameObject.SetActive(true);
        }
        else
        {
            healthBarUI.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarUI != null)
        {
            healthBarUI.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        OnBossDeath?.Invoke();  // 보스가 죽었을 때 이벤트 호출
        Debug.Log("Boss Died");
        Destroy(gameObject);
        healthBarUI.gameObject.SetActive(false);
    }
}
