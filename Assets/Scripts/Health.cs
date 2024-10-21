using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;       // 최대 체력
    private int currentHealth;        // 현재 체력

    public Text healthText;           // 체력 표시를 위한 UI 텍스트
    public Image heartImagePrefab;    // 하트 UI 이미지 (Image 컴포넌트)
    public Transform heartContainer;  // 하트들이 표시될 부모 오브젝트 (World Space Canvas)

    private List<Image> hearts = new List<Image>();  // 하트 이미지 리스트

    void Start()
    {
        currentHealth = maxHealth;   // 시작할 때 최대 체력으로 초기화

        // 컨테이너가 null일 경우, 슬라임 자식에서 Canvas를 자동으로 찾음
        if (heartContainer == null)
        {
            heartContainer = GetComponentInChildren<Canvas>().transform;
            if (heartContainer == null)
            {
                Debug.LogError("No Canvas found for health hearts!");
            }
        }

        UpdateHealthUI();            // 초기 UI 업데이트
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
        UpdateHealthUI();            // 체력 변경 시 UI 업데이트
        UpdateHearts();              // 하트 UI 업데이트
    }

    // 체력 UI 텍스트 업데이트 함수
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();  // 텍스트에 현재 체력 반영
        }
    }

    // 하트 UI 업데이트 함수
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

        // 기존 하트 이미지 삭제
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        // 현재 체력에 맞게 하트 생성 (10당 하트 1개)
        int heartCount = Mathf.CeilToInt(currentHealth / 10f);
        for (int i = 0; i < heartCount; i++)
        {
            Image heart = Instantiate(heartImagePrefab, heartContainer);
            hearts.Add(heart);  // 하트 리스트에 추가
            Debug.Log("Heart instantiated successfully.");
        }
    }

    // 캐릭터가 죽었을 때 처리
    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);  // 오브젝트 삭제 또는 비활성화
    }
}
