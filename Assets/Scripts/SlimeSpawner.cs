using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject[] slimePrefabs; // ������ ������ �迭
    public int slimeCount = 100;    // ������ ��
    public float spawnRadius = 300f; // ��ȯ�� ����
    public Transform mapCenter;   // ���� �߽�
    public float minY = 18f;      // ������ �ּ� Y ��ǥ
    public float maxNavMeshDistance = 40f; // NavMesh���� ����� ǥ���� ã�� ���� �Ÿ�

    void Start()
    {
        SpawnSlimes();
    }

    void SpawnSlimes()
    {
        for (int i = 0; i < slimeCount; i++)
        {
            Vector3 randomPosition;

            // Y ��ǥ�� minY �̻��� ������ �ݺ��ؼ� ���� ��ġ ����
            do
            {
                randomPosition = mapCenter.position + Random.insideUnitSphere * spawnRadius;
                randomPosition.y = mapCenter.position.y; // �⺻ ���̸� ���� �߽� ���̷� ����
            }
            while (randomPosition.y < minY); // Y ��ǥ�� 18 �����̸� �ٽ� ���� ��ġ ����

            // NavMesh���� ǥ�� ã��
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, maxNavMeshDistance, NavMesh.AllAreas))
            {
                randomPosition = hit.position; // NavMesh ǥ�� ��ġ�� ������Ʈ
            }
            else
            {
                // ���� NavMesh ǥ���� ã�� ���ߴٸ� �ش� ������ ��ȯ�� ���� (�ʿ�� ó��)
                Debug.LogWarning("NavMesh ��ã��");
                continue;
            }

            // ���� ������ ������ ����
            int randomSlimeIndex = Random.Range(0, slimePrefabs.Length);
            GameObject selectedSlimePrefab = slimePrefabs[randomSlimeIndex];

            // ������ ��ȯ
            GameObject slime = Instantiate(selectedSlimePrefab, randomPosition, Quaternion.identity);

            // �������� Waypoints ���� (�ʿ��)
            EnemyAi enemyAi = slime.GetComponent<EnemyAi>();
            if (enemyAi != null)
            {
                enemyAi.waypoints = GenerateRandomWaypoints(randomPosition); // ���� waypoints ����
            }
        }
    }

    // ������ Waypoints �����ϴ� �Լ�
    Transform[] GenerateRandomWaypoints(Vector3 origin)
    {
        Transform[] waypoints = new Transform[2]; // �� ���� Waypoint�� ���÷� ����

        for (int i = 0; i < waypoints.Length; i++)
        {
            GameObject waypoint = new GameObject("Waypoint" + i);
            waypoint.transform.position = origin + Random.insideUnitSphere * 10f; // 10 ���� �� ������ ��ġ
            waypoint.transform.position = new Vector3(waypoint.transform.position.x, origin.y, waypoint.transform.position.z); // ���� ����
            waypoints[i] = waypoint.transform;
        }

        return waypoints;
    }
}
