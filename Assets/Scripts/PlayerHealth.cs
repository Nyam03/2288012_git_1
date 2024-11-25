using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;       // 최대 체력
    private int currentHealth;        // 현재 체력

    public Text healthText;           // 체력 표시를 위한 UI 텍스트

    void Start()
    {
        currentHealth = maxHealth;   // 시작할 때 최대 체력으로 초기화
        UpdateHealthUI();            // 초기 UI 업데이트
    }

    public void Heal(int amount)
    {
        currentHealth += amount;    // 회복량만큼 체력 증가
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // 최대 체력을 초과하지 않도록 제한
        }
        UpdateHealthUI();            // 체력 변경 시 UI 업데이트
    }

    // 체력 감소 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;    // 데미지만큼 체력 감소
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();                  // 체력이 0 이하일 때 죽음 처리
        }
        UpdateHealthUI();            // 체력 변경 시 UI 업데이트
    }

    // 체력 UI 텍스트 업데이트 함수
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();  // 텍스트에 현재 체력 반영
        }
    }

    // 캐릭터가 죽었을 때 처리
    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);  // 오브젝트 삭제 또는 비활성화
    }
}
