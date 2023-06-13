using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 5f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    public Enemy enemy;

    private Rigidbody2D rigBody;
    private BoxCollider2D boxCol;
    private Animator anim;

    private bool isGrounded = true;
    private bool isAttacking = false;


    private SpriteRenderer spriteRenderer;

    private string RUN_ANIMATION = "isRunning";

    private string HURT_ANIMATION = "isHurt";

    private string TRANS_ANIMATION = "isTransit";

    private string JUMP_ANIMATION = "isJumping";

    private string ATTACK1_ANIMATION = "isAttacking";
    private string STOP_ANIMATION = "notAttacking";

    private bool isHurt = false;

    private string GROUND_TAG = "Ground";

    // Start is called before the first frame update

    private void Awake()
    {
        rigBody = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        PlayerAttacking();
    }

    private void FixedUpdate()
    {
        
    }

    void PlayerMoveKeyboard()
    {

        movementX = Input.GetAxisRaw("Horizontal");
        //Debug.Log(movementX);

        //movementY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        //moving to the right side
        if(movementX > 0f) {
            anim.SetBool(RUN_ANIMATION, true);
            spriteRenderer.flipX = false;
            
        }
        else if(movementX < 0f) // moving to the left side
        {
            anim.SetBool(RUN_ANIMATION, true);
            spriteRenderer.flipX = true;
        }
        else
        {
            anim.SetBool(RUN_ANIMATION, false);
        }
    }


    void PlayerAttacking()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Trigger the attack animation
            anim.SetTrigger(ATTACK1_ANIMATION);
            isAttacking = true;

        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            // Stop the attack animation by resetting the trigger parameter
            anim.SetTrigger(STOP_ANIMATION);
            isAttacking = false;
        }

    }
    public void TakeDamage()
    {
        // Trigger the hurt animation
        //isHurt = true;
        anim.Play("Hurt");
        //anim.SetBool(HURT_ANIMATION, true);
    }

    

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded){
            isGrounded = false;
            rigBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool(JUMP_ANIMATION, true);
        }
         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMechanics playerMechanics = gameObject.GetComponent<PlayerMechanics>();
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded=true;
            anim.SetBool(JUMP_ANIMATION, false);
        }

        if (collision.gameObject.tag == "Enemy" && isAttacking)
        {

            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(playerMechanics.attackPoints);
        }


    }
}
