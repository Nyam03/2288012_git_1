using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealth : MonoBehaviour
{
    public int maxHealth = 30;       // 최대 체력
    private int currentHealth;        // 현재 체력
    public int coinCount = 1;         //코인 개수

    public GameObject coinPrefab;
    public List<Image> hearts;

    void Start()
    {
        currentHealth = maxHealth;   // 시작할 때 최대 체력으로 초기화
        UpdateHearts();              // 초기 하트 UI 업데이트
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
        UpdateHearts();              // 하트 UI 업데이트
    }

    // 슬라임 하트 UI 업데이트
    void UpdateHearts()
    {
        int activeHearts = Mathf.CeilToInt(currentHealth / 10f);  // 체력 10당 하트 1개 활성화

        // 모든 하트를 비활성화한 후, 현재 체력에 맞게 활성화
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < activeHearts)
            {
                hearts[i].gameObject.SetActive(true);   // 필요한 하트는 활성화
            }
            else
            {
                hearts[i].gameObject.SetActive(false);  // 나머지 하트는 비활성화
            }
        }
    }

    // 캐릭터가 죽었을 때 처리
    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        //죽으면 코인 생성
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 coinPosition = transform.position + new Vector3(0, 1f, 0);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }
        Destroy(gameObject);  // 오브젝트 삭제 또는 비활성화
    }
}
