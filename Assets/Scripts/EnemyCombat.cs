using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Transform enemyTransform;
    public Animator anim;


    public BoxCollider2D Collider1;

    public float moveSpeed;
    public float distance; // Desired distance between the object and the player


    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public PlayerMechanics playerMechanics;

    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


        if (enemyTransform == null)
        {
            // If the player is not found, stop the update function
            return;
        }

        // Calculate the direction from the object to the player
        Vector2 direction = enemyTransform.position - transform.position;

        // Zero out the vertical component of the direction to move only horizontally
        direction.y = 0;

        float currentDistance = direction.magnitude;

        // Check if the current distance is greater than the desired distance
        if (currentDistance > distance)
        {
            
            Vector2 targetPosition = (Vector2)enemyTransform.position - direction.normalized * distance;

            // Move the object towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            anim.SetBool("isFar", true);
            anim.SetFloat("Speed", 4f);
        }
        else
        {
            Attack();
            anim.SetFloat("Speed", 0f);
        }


        if (currentDistance >=10f)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<EnemyWander>().enabled = true;
            GetComponent<EnemyCombat>().enabled = false;
        }

            // Flip the enemy's scale based on the player's scale
            if (transform.localScale.x * direction.x < 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            enemyTransform = other.gameObject.transform;
            playerMechanics = other.gameObject.GetComponent<PlayerMechanics>();
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            anim.SetBool("Attack", true);
            //Detect enemies in range
            

            nextAttackTime = Time.time + 1f / attackRate;

            Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);


            foreach (Collider2D player in hitplayer)
            {
                int enemyAttackPower = gameObject.GetComponent<Enemy>().damage;
                Debug.Log(enemyAttackPower);
                //playerMechanics.TakeDamage(gameObject.GetComponent<Enemy>().damage);
                playerMechanics.TakeDamage(enemyAttackPower);

            }
        }
        else
        {
            anim.SetBool("Attack", false);
        }


        if (playerMechanics.hitPoints<=0) {
            anim.SetBool("Attack", false);
            anim.SetBool("isIdle", true);
            GetComponent<EnemyCombat>().enabled = false;
            GetComponent<EnemyWander>().enabled = false;
        }

    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
