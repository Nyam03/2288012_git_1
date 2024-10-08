using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // ���� ������
    public float spawnInterval = 3f; // ���� ����
    public Transform[] waypoints; // ���Ͱ� �̵��� ��������Ʈ

    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMonster()
    {
        GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        MonsterMovement monsterMovement = monster.GetComponent<MonsterMovement>();
        if (monsterMovement != null)
        {
            monsterMovement.waypoints = new List<Transform>(waypoints); // ��������Ʈ ����Ʈ ����
        }
    }
}
