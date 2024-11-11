using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnLocation;
    public float respawnTime = 600f; // 10분

    private GameObject currentBoss;

    void Start()
    {
        SpawnBoss();
    }

    void SpawnBoss()
    {
        currentBoss = Instantiate(bossPrefab, spawnLocation.position, Quaternion.identity);

        // BossHealth의 OnBossDeath 이벤트 구독
        BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.OnBossDeath += HandleBossDeath;
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
