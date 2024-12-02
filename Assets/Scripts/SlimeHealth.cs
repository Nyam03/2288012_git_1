using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public int coinCount = 1;

    public GameObject coinPrefab;
    public List<Image> hearts;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    // 체력 감소 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHearts();
    }

    // 슬라임 하트 UI 업데이트
    void UpdateHearts()
    {
        int activeHearts = Mathf.CeilToInt(currentHealth / 10f);  // 체력 10당 하트 1개 활성화

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < activeHearts)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        //죽으면 코인 생성
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 coinPosition = transform.position + new Vector3(0, 1f, 0);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
