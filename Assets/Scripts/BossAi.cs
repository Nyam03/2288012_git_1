using UnityEngine;
using UnityEngine.AI;

public class BossAi : MonoBehaviour
{
    public Face faces;
    public GameObject SmileBody;
    public AudioSource audioSource;
    public AudioClip attackSound;
    public SlimeAnimationState currentState;

    public Animator animator;
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public bool isBoss;
    public float detectionRange = 15f;
    public float attackRange = 8f;
    public int attackDamage = 15;
    public float attackCooldown = 3f;
    public float rotationSpeed = 4f;

    private GameObject player;
    private int m_CurrentWaypointIndex;
    private float lastAttackTime;
    private bool move;
    private Material faceMaterial;
    private Vector3 originPos;
    public int damType;

    public enum WalkType { Patroll, ToOrigin }
    private WalkType walkType;

    void Start()
    {
        originPos = transform.position;
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        player = GameObject.FindGameObjectWithTag("Player");

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 동적 추가
        }
        audioSource.playOnAwake = false;
        audioSource.volume = 0.1f;

        /*
        if (!isBoss && waypoints.Length > 0)
        {
            walkType = WalkType.Patroll;
            //WalkToNextDestination();
        }
        else
        {
            currentState = SlimeAnimationState.Idle;
            agent.isStopped = true;
        }
        */
    }

    void Update()
    {
        if (isBoss)
        {
            DetectAndAttackPlayer();
        }
        else
        {
            //HandlePatrol();
        }
    }

    // 플레이어 탐지, 공격 
    void DetectAndAttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            LookAtPlayer();

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                currentState = SlimeAnimationState.Idle;
            }
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            currentState = SlimeAnimationState.Attack;
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            PlayAttackSound();
        }
    }

    void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.clip = attackSound;
            audioSource.Play();
        }
    }

    // 플레이어를 바라보는 메서드
    void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    /*
    void HandlePatrol()
    {
        if (waypoints.Length == 0) return;

        switch (currentState)
        {
            case SlimeAnimationState.Walk:
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    currentState = SlimeAnimationState.Idle;
                    Invoke(nameof(WalkToNextDestination), 2f);
                }
                break;

            case SlimeAnimationState.Idle:
                StopAgent();
                break;
        }
    }

    public void WalkToNextDestination()
    {
        if (waypoints.Length == 0) return;
        currentState = SlimeAnimationState.Walk;
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // 이동 중지
    private void StopAgent()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0);
    }
    */

    // 애니메이션 종료 이벤트
    public void AlertObservers(string message)
    {
        if (message.Equals("AnimationAttackEnded"))
        {
            currentState = SlimeAnimationState.Idle;
        }
    }
}
