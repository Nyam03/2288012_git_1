using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnLocation;
    public float respawnTime = 600f; // 10��

    private GameObject currentBoss;

    void Start()
    {
        SpawnBoss();
    }

    void SpawnBoss()
    {
        currentBoss = Instantiate(bossPrefab, spawnLocation.position, Quaternion.identity);

        // BossHealth�� OnBossDeath �̺�Ʈ ����
        BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.OnBossDeath += HandleBossDeath;
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
