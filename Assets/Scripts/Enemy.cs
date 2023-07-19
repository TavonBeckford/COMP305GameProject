using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private GameObject[] itemReference;

    [SerializeField] private GameObject spawnedItem;

    public int damage;
    public int health;
    public int maxHealth= 100;

    public Rigidbody2D rb;

    private int randomIndex;

    [SerializeField] private Healthbar healthbar;

    private void Start()
    {
        healthbar.UpdateHealthBar(maxHealth, health);
    }


    

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        //play hurt animation
        animator.SetTrigger("Hurt");
        animator.SetBool("Attack", false);

        if (health <= 0)
        {
            GetComponent<EnemyCombat>().enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Die();
            
        }
        healthbar.UpdateHealthBar(maxHealth, health);

    }

    void Die()
    {
        Debug.Log("Enemy Died");

        GameObject parentGameObject = gameObject;
        GameObject childGameObject = parentGameObject.transform.Find("Healthbar").gameObject;

        // Disable the child GameObject
        childGameObject.SetActive(false);

        animator.SetBool("isDead", true);


        //GetComponent<CircleCollider2D>().enabled = false;

        //Disable enemy
        //GetComponent<Collider2D>().enabled = false;


        StartCoroutine(removeEnemy());
    }


    private void OnDestroy()
    {
        int randomIndex = Random.Range(0, itemReference.Length);
        spawnedItem = itemReference[randomIndex];
        Vector3 spawnPosition = transform.position + new Vector3(1f, -0.5f, 0f); // Offset the spawn position by 1 unit to the right
        Instantiate(spawnedItem, spawnPosition, Quaternion.identity);
    }


    IEnumerator removeEnemy()
    {
        
        yield return new WaitForSeconds(2);
        Destroy(gameObject);


    }

}
