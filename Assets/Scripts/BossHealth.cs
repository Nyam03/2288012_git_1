using UnityEngine;
using UnityEngine.UI;
using System;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 300;
    private int currentHealth;

    public Slider healthBarUI; // 동적으로 연결될 체력바 UI
    public Transform player;
    public float displayRange = 20f;

    // 보스 죽음 이벤트 정의
    public event Action OnBossDeath;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (healthBarUI != null)
        {
            healthBarUI.gameObject.SetActive(false); // 체력바 초기 비활성화
        }

        // 플레이어 자동 할당
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // "Player" 태그를 가진 오브젝트 찾기
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }


    void Update()
    {
        CheckDisplayHealthBar();
    }

    void CheckDisplayHealthBar()
    {
        if (player == null || healthBarUI == null)
        {
            return;
        }

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
        if (healthBarUI != null)
        {
            Destroy(healthBarUI.gameObject);
        }
    }
}
