using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biter : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float detectionRadius;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float speed;
    bool playerFound;
    ISubmarine player;
    Vector2 attackTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerFound)
        {            
            SearchForPlayer();
            return;
        }

    }

    void SearchForPlayer()
    {
        /*var player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (player == null)
        {
            return;
        }
        Vector2 playerDirection = (player.transform.position - transform.position).normalized;
        if (Physics2D.Raycast(transform.position, playerDirection, detectionRadius).collider != null)
        {
            playerFound = true;
            this.player = player.GetComponent<ISubmarine>();
            attackTarget = this.player.GetAttackPosition();
        }*/
    }
}
