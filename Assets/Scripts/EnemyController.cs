using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Animator anim;

    private string WALK_ANIMATION = "isWalking";
    private string ATTACK_ANIMATION = "isNearPlayer";
    private string IDLE_ANIMATION = "isNotNearPlayer";

    public float moveSpeed;
    public float distance; // Desired distance between the object and the player
    private GameObject PlayerObject;


    private void Awake()
    {
       
    }

    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        // Calculate the direction from the object to the player
        Vector2 direction = PlayerObject.transform.position - transform.position;

        float currentDistance = direction.magnitude;

        // Check if the current distance is greater than the desired distance
        if (currentDistance > distance) {

            Vector2 targetPosition = (Vector2)PlayerObject.transform.position - direction.normalized * distance;

            // Move the object towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            anim.SetBool(WALK_ANIMATION, true);

            anim.SetBool(ATTACK_ANIMATION, false);
        }
        else
        {
            anim.SetBool(ATTACK_ANIMATION, true);
            //anim.SetTrigger(IDLE_ANIMATION);
            anim.SetBool(WALK_ANIMATION, false);
        }


        // Flip the enemy's scale based on the player's scale
        if (transform.localScale.x * direction.x < 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
