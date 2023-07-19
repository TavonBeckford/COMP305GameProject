using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyWander : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;             // The speed at which the enemy moves
    [SerializeField]
    private float maxDistance = 3f;           // The maximum distance the enemy can travel in either direction
    [SerializeField]
    private float idleTime = 2f;              // The time the enemy waits before wandering again

    private float startXPos;                  // The starting X position of the enemy
    private float targetXPos;                 // The target X position for the enemy to move towards
    private SpriteRenderer spriteRenderer;    // Reference to the sprite renderer component

    private bool isIdling;                    // Indicates if the enemy is currently idling
    private float idleTimer;                  // Timer to control the idle period

    public Animator animator;
    private void Start()
    {
        startXPos = transform.position.x;
        SetNewTargetPosition();

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start with idling
        isIdling = true;
        idleTimer = idleTime;
    }

    private void Update()
    {
        if (isIdling)
        {
            Idle();
        }
        else
        {
            Wander();
        }
    }

    private void Idle()
    {
        idleTimer -= Time.deltaTime;
        animator.SetFloat("Speed", 0f);
        animator.SetBool("isIdle", true);

        if (idleTimer <= 0f)
        {
            // Idle period is over, start wandering
            isIdling = false;
            animator.SetBool("isIdle", false);
        }
    }

    private void Wander()
    {
        // Move the enemy towards the target position
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetXPos, transform.position.y), moveSpeed * Time.deltaTime);
        animator.SetFloat("Speed", moveSpeed);

        // Check if the enemy has reached the target position
        if (Mathf.Abs(transform.position.x - targetXPos) < 0.1f)
        {
            // Set a new target position
            SetNewTargetPosition();

            // Start idling
            isIdling = true;
            idleTimer = idleTime;
        }

        

        // Flip the sprite based on the movement direction
        if (targetXPos < transform.position.x)
        {
            spriteRenderer.flipX = true;  // Flip the sprite horizontally if moving left
        }
        else if (targetXPos > transform.position.x)
        {
            spriteRenderer.flipX = false; // Do not flip the sprite if moving right
        }
    }

    private void SetNewTargetPosition()
    {
        // Calculate a new target position within the specified range
        targetXPos = Random.Range(startXPos - maxDistance, startXPos + maxDistance);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("Attack", false);
            animator.SetBool("isFar", false);
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false; 
            }
            GetComponent<EnemyCombat>().enabled = true;
            GetComponent<EnemyWander>().enabled = false;
            GetComponent<EnemyCombat>().enabled = true;
            GetComponent<EnemyWander>().enabled = false;
        }
    }

}
