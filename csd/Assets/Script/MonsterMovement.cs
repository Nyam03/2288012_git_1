using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public List<Transform> waypoints; // ��������Ʈ ����Ʈ
    public float stoppingDistance = 0.1f; // ���� �Ÿ�

    private NavMeshAgent navMeshAgent; // NavMeshAgent ����
    private int currentWaypointIndex = 0; // ���� ��������Ʈ �ε���

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (waypoints.Count == 0) return;

        // ��ǥ�� �����ߴ��� Ȯ��
        if (navMeshAgent.remainingDistance <= stoppingDistance && !navMeshAgent.pathPending)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position); // ���� ��������Ʈ�� �̵�
            currentWaypointIndex++;
        }
        else
        {
            Destroy(gameObject); // ��� ��������Ʈ�� �Ϸ��� ��� ���� ����
        }
    }
}
