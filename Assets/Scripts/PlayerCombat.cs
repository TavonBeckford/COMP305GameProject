using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;


    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Material ultimateMode;
    public Material normalMode;

    // Update is called once per frame
    void Update()
    {
        int currentEnergy = this.GetComponent<PlayerMechanics>().energyPoints;

        if (currentEnergy == 100)
        {
            this.GetComponent<SpriteRenderer>().material = ultimateMode;
        }



        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                nextAttackTime = Time.time + 1f/ attackRate;
            }
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
           
            if (currentEnergy == 100) {
                Ultimate();
                StartCoroutine(removeUltState());
            }
            
            this.GetComponent<PlayerMechanics>().ReduceEnergyPoints(100);

        }


    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        //Detect enemies in range
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        
        foreach (Collider2D enemy in hitenemies)
        {
            Debug.Log("Enemy hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(gameObject.GetComponent<PlayerMechanics>().attackPoints);

        }

    }

    void Ultimate()
    {
        animator.SetTrigger("Ultimate");

        //Detect enemies in range
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        foreach (Collider2D enemy in hitenemies)
        {
            Debug.Log("Enemy hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(gameObject.GetComponent<PlayerMechanics>().specialAttackPoints);

        }

    }

    IEnumerator removeUltState()
    {

        yield return new WaitForSeconds(2);

        this.GetComponent<SpriteRenderer>().material = normalMode;

    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
