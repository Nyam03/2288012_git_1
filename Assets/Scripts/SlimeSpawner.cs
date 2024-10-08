using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject[] slimePrefabs; // 슬라임 프리팹 배열
    public int slimeCount = 100;    // 슬라임 수
    public float spawnRadius = 300f; // 소환할 범위
    public Transform mapCenter;   // 맵의 중심
    public float minY = 18f;      // 슬라임 최소 Y 좌표
    public float maxNavMeshDistance = 40f; // NavMesh에서 가까운 표면을 찾기 위한 거리

    void Start()
    {
        SpawnSlimes();
    }

    void SpawnSlimes()
    {
        for (int i = 0; i < slimeCount; i++)
        {
            Vector3 randomPosition;

            // Y 좌표가 minY 이상일 때까지 반복해서 랜덤 위치 생성
            do
            {
                randomPosition = mapCenter.position + Random.insideUnitSphere * spawnRadius;
                randomPosition.y = mapCenter.position.y; // 기본 높이를 맵의 중심 높이로 맞춤
            }
            while (randomPosition.y < minY); // Y 좌표가 18 이하이면 다시 랜덤 위치 생성

            // NavMesh에서 표면 찾기
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, maxNavMeshDistance, NavMesh.AllAreas))
            {
                randomPosition = hit.position; // NavMesh 표면 위치로 업데이트
            }
            else
            {
                // 만약 NavMesh 표면을 찾지 못했다면 해당 슬라임 소환을 생략 (필요시 처리)
                Debug.LogWarning("NavMesh 못찾음");
                continue;
            }

            // 랜덤 슬라임 프리팹 선택
            int randomSlimeIndex = Random.Range(0, slimePrefabs.Length);
            GameObject selectedSlimePrefab = slimePrefabs[randomSlimeIndex];

            // 슬라임 소환
            GameObject slime = Instantiate(selectedSlimePrefab, randomPosition, Quaternion.identity);

            // 슬라임의 Waypoints 설정 (필요시)
            EnemyAi enemyAi = slime.GetComponent<EnemyAi>();
            if (enemyAi != null)
            {
                enemyAi.waypoints = GenerateRandomWaypoints(randomPosition); // 랜덤 waypoints 생성
            }
        }
    }

    // 랜덤한 Waypoints 생성하는 함수
    Transform[] GenerateRandomWaypoints(Vector3 origin)
    {
        Transform[] waypoints = new Transform[2]; // 두 개의 Waypoint만 예시로 생성

        for (int i = 0; i < waypoints.Length; i++)
        {
            GameObject waypoint = new GameObject("Waypoint" + i);
            waypoint.transform.position = origin + Random.insideUnitSphere * 10f; // 10 유닛 내 랜덤한 위치
            waypoint.transform.position = new Vector3(waypoint.transform.position.x, origin.y, waypoint.transform.position.z); // 높이 조정
            waypoints[i] = waypoint.transform;
        }

        return waypoints;
    }
}
