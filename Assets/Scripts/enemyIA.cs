using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyIA : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ConstsScriptableObject consts;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float minRoamDistance = 2f;
    private float maxRoamDistance = 7f;
    public float health;

    private float lookRadius = 10f;
    private float stoppingTargetDistance = 2f;
    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        health = consts.MaxHealth;
        minRoamDistance = consts.minRoamDistance;
        maxRoamDistance = consts.maxRoamDistance;
        lookRadius = consts.lookRadius;
        stoppingTargetDistance = consts.StoppingTargetDistance;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }
    public void Damage(float damage) {
        if ((health -= damage) <= 0)
            Destroy(gameObject);
    }
    void Update()
    {
        //check if Player is around
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.stoppingDistance = stoppingTargetDistance;
            agent.SetDestination(target.position);
            roamPosition = Vector3.zero;            //not wandering now, just following target

            if(distance <= agent.stoppingDistance)
            {
                // Attack the target
                animator.SetTrigger(consts.Attack1Name);
                CombatSystem.ChooseAttack(consts.Attack1Type, consts.Damage, this.transform.position, this.transform.rotation, consts.AttackRange1, LayerMask.GetMask("Player"));
                // Face the target
                FaceTarget();
            }
        }
        else
        {
            agent.stoppingDistance = 0f;
            if (roamPosition.Equals(Vector3.zero) || agent.velocity.Equals(Vector3.zero))
            {
                roamPosition = GetRoamingPosition();
            }

            // Wander around
            agent.SetDestination(roamPosition);

            float reachedPositionDistance = 0.5f;
            if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
            {
                //Reached Roam Position
                roamPosition = GetRoamingPosition();
            }

        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(minRoamDistance, maxRoamDistance);
    }

    private static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
