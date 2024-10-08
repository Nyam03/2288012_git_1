using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public List<Transform> waypoints; // 웨이포인트 리스트
    public float stoppingDistance = 0.1f; // 도착 거리

    private NavMeshAgent navMeshAgent; // NavMeshAgent 참조
    private int currentWaypointIndex = 0; // 현재 웨이포인트 인덱스

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (waypoints.Count == 0) return;

        // 목표에 도달했는지 확인
        if (navMeshAgent.remainingDistance <= stoppingDistance && !navMeshAgent.pathPending)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position); // 다음 웨이포인트로 이동
            currentWaypointIndex++;
        }
        else
        {
            Destroy(gameObject); // 모든 웨이포인트를 완료한 경우 몬스터 삭제
        }
    }
}
