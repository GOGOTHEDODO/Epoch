using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChasePlayer : MonoBehaviour
{
    public float detectionRadius = 8f;
    public float wanderRadius = 5f;
    public float wanderInterval = 3f;

    private float wanderTimer;
    private bool isWandering = false;
    private GameObject currentWanderTarget;
    private Transform player;
    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        aiPath.canMove = false;

        tryDetectPlayer();
    }

    void tryDetectPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if(distance <= detectionRadius)
        {
            destinationSetter.target = player;
            aiPath.canMove = true;
            isWandering = false;

            if(currentWanderTarget != null)
            {
                Destroy(currentWanderTarget);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            if(destinationSetter.target != player)
            {
                destinationSetter.target = player;
                aiPath.canMove = true;
                
                if(currentWanderTarget != null)
                {
                    Destroy(currentWanderTarget);
                }
            }
            isWandering = false;
            
        }
        else
        {
            WanderLogic();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void WanderLogic()
    {
        wanderTimer -= Time.deltaTime;

        if(!isWandering || wanderTimer <= 0f)
        {
            isWandering = true;
            wanderTimer = wanderInterval;

            Vector2 RandomDir = Random.insideUnitCircle.normalized * wanderRadius;
            Vector3 wanderTarg = transform.position + new Vector3(RandomDir.x, RandomDir.y, 0f);

            destinationSetter.target = null;
            aiPath.canMove = true;

            currentWanderTarget = new GameObject("WanderTarget");
            currentWanderTarget.transform.position = wanderTarg;
            destinationSetter.target = currentWanderTarget.transform;

            Destroy(currentWanderTarget, wanderInterval);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsChasingPlayer())
        {
            if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                wanderTimer = 0f;
            }
            wanderTimer = 0f;
        }
    }

    bool IsChasingPlayer()
    {
        return destinationSetter.target != null && destinationSetter.target == player;
    }
}
