using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnLocation;
    public float respawnTime = 600f; // 10��
    public GameObject bossHealthUIPrefab; // ���� ü�¹� UI ������
    public Canvas canvas; // UI ĵ����

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
                // UI ���� �� ĵ������ �ùٸ��� �߰�
                GameObject bossHealthUI = Instantiate(bossHealthUIPrefab, canvas.transform);
                RectTransform rectTransform = bossHealthUI.GetComponent<RectTransform>();

                // RectTransform �ʱ�ȭ
                if (rectTransform != null)
                {
                    rectTransform.localScale = Vector3.one;
                    rectTransform.anchoredPosition = Vector3.zero; 
                    rectTransform.localPosition = new Vector3(0, 400, 0);
                }

                Slider healthBarSlider = bossHealthUI.GetComponent<Slider>();
                if (healthBarSlider != null)
                {
                    bossHealth.healthBarUI = healthBarSlider; // ���� ü�¹� ����
                }
            }
        }
    }

    void HandleBossDeath()
    {
        // ������ ������ �̺�Ʈ ���� ����
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
