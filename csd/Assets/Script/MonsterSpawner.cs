using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 몬스터 프리팹
    public float spawnInterval = 3f; // 스폰 간격
    public Transform[] waypoints; // 몬스터가 이동할 웨이포인트

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
            monsterMovement.waypoints = new List<Transform>(waypoints); // 웨이포인트 리스트 설정
        }
    }
}
