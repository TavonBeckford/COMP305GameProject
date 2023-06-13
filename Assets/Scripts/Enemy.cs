using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject[] itemReference;

    [SerializeField] private GameObject spawnedItem;

    public int damage;
    public int health;
    public int maxHealth= 100;
    public PlayerMechanics playerMechanics;

    private int randomIndex;

    [SerializeField] private Healthbar healthbar;

    private void Start()
    {
        healthbar.UpdateHealthBar(maxHealth, health);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            playerMechanics = collision.gameObject.GetComponent<PlayerMechanics>();
            playerMechanics.TakeDamage(damage);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        
        if (health <= 0)
        {

            Destroy(gameObject);
        }
        healthbar.UpdateHealthBar(maxHealth, health);

    }


    private void OnDestroy()
    {
        int randomIndex = Random.Range(0, itemReference.Length);
        spawnedItem = itemReference[randomIndex];
        Vector3 spawnPosition = transform.position + new Vector3(1f, -0.5f, 0f); // Offset the spawn position by 1 unit to the right
        Instantiate(spawnedItem, spawnPosition, Quaternion.identity);
    }
    
}
