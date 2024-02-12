using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Biter : MonoBehaviour, IEnemyEffect
{
    [SerializeField] Animator animator;
    [SerializeField] float detectionRadius;
    [SerializeField] float speed;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float biterWidth;
    [SerializeField] float slowDownMultiplier;
    ISubmarine playerSub;
    EnemyState enemyState = EnemyState.Idle;
    Vector3 relativeAttackPosition;
    int health = 100;
    public float SlowdownMultiplier => slowDownMultiplier;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        agent.stoppingDistance = 0;
        playerSub = player.GetComponent<ISubmarine>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyState == EnemyState.Chase)
        {
            agent.SetDestination(player.position + relativeAttackPosition);
            spriteRenderer.flipX = (player.position + relativeAttackPosition).x - biterWidth > transform.position.x;
        }

        if (enemyState == EnemyState.Attack)
        {
            return;
        }

        if(enemyState == EnemyState.Chase)
        {
            Vector2 target = player.position + relativeAttackPosition;
            Vector2 current = transform.position;            
            if((target - current).magnitude <= 0.05f)
            {
                transform.parent = player;
                transform.position = player.position + relativeAttackPosition;
                agent.enabled = false;
                enemyState = EnemyState.Attack;
                playerSub.AddAttachedEnemy(this);
                //change animation to attack
                animator.SetTrigger("Bite");
            }

            return;
        }

        if(Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer))
        {
            agent.enabled = true;
            enemyState = EnemyState.Chase;
            relativeAttackPosition = playerSub.GetRelativeAttackPosition();
            //change animation to chase
        }
    }

    public void DamagePlayer(int damage)
    {
        playerSub.Damage(damage);
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            HandleDeath();
        }
    }
    void HandleDeath()
    {
        playerSub.RemoveAttachedEnemy(this);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        if(player == null)
        {
            return;
        }
        Gizmos.DrawSphere(player.position + relativeAttackPosition, 0.1f);
    }

    enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }
}
