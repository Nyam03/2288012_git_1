using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnLocation;
    public float respawnTime = 600f; // 10분
    public GameObject bossHealthUIPrefab; // 보스 체력바 UI 프리팹
    public Canvas canvas; // UI 캔버스

    private GameObject currentBoss;

    void Start()
    {
        SpawnBoss();
    }

    void SpawnBoss()
    {
        currentBoss = Instantiate(bossPrefab, spawnLocation.position, Quaternion.identity);

        BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.OnBossDeath += HandleBossDeath;

            if (bossHealthUIPrefab != null && canvas != null)
            {
                // UI 생성 시 캔버스에 올바르게 추가
                GameObject bossHealthUI = Instantiate(bossHealthUIPrefab, canvas.transform);
                RectTransform rectTransform = bossHealthUI.GetComponent<RectTransform>();

                // RectTransform 초기화
                if (rectTransform != null)
                {
                    rectTransform.localScale = Vector3.one;
                    rectTransform.anchoredPosition = Vector3.zero; 
                    rectTransform.localPosition = new Vector3(0, 400, 0);
                }

                Slider healthBarSlider = bossHealthUI.GetComponent<Slider>();
                if (healthBarSlider != null)
                {
                    bossHealth.healthBarUI = healthBarSlider; // 보스 체력바 연결
                }
            }
        }
    }

    void HandleBossDeath()
    {
        // 보스가 죽으면 이벤트 구독 해제
        BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.OnBossDeath -= HandleBossDeath;
        }

        Destroy(currentBoss);
        StartCoroutine(RespawnBossAfterDelay());
    }

    IEnumerator RespawnBossAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnBoss();
    }
}
