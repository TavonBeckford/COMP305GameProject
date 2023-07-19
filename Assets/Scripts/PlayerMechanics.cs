using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMechanics : Character
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private Energybar energybar;

    public TextMeshProUGUI quantityText;
    private bool isTakingDamage = false;

    public Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        healthbar.UpdateHealthBar(maxHitPoints, hitPoints);
        energybar.UpdateEnergyBar(maxEnergy, energyPoints);
        quantityText.text = "Health: " + hitPoints.ToString();
        rb = GetComponent<Rigidbody2D>();


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Item"))
        {
            Items hitObject = collision.gameObject.GetComponent<Consumables>().item;



            if (hitObject != null)
            {
                print($"Hit: {hitObject.objectName}");
                switch (hitObject.itemType)
                {
                    case Items.ItemType.ENERGYPOTION:
                        AdjustEnergyPoints(hitObject.increaseBy);
                        break;
                    case Items.ItemType.HEALTHPOTION:
                        AdjustHitPoints(hitObject.increaseBy);
                        break;
                }

                collision.gameObject.SetActive(false);
            }
        }



    }

    private void AdjustHitPoints(int amount)
    {
        if (hitPoints < maxHitPoints)
        {
            hitPoints += amount;
        }
        healthbar.UpdateHealthBar(maxHitPoints, hitPoints);
        quantityText.text = "Health: " + hitPoints.ToString();
        print($"Adjusted hit points by {amount}. New Value: {hitPoints}");
    }
    public void AdjustEnergyPoints(int amount)
    {
        if (energyPoints < maxEnergy)
        {

            energyPoints += amount;
        }
        energybar.UpdateEnergyBar(maxEnergy, energyPoints);
        print($"Adjusted energy points by {amount}. New Value: {energyPoints}");
    }


    public void ReduceEnergyPoints(int amount)
    {
       
        energyPoints -= amount;
        energybar.UpdateEnergyBar(maxEnergy, energyPoints);
        print($"Reduced energy points by {amount}. New Value: {energyPoints}");
    }


    public void TakeDamage(int dmg)
    {

        hitPoints -= dmg;
        Debug.Log("this is the dmg" + dmg);

        //play hurt animation
        animator.SetTrigger("Hurt");
        animator.SetBool("Attack", false);


        //gameObject.GetComponent<Player>().TakeDamage();
        healthbar.UpdateHealthBar(maxHitPoints, hitPoints);
        quantityText.text = "Health: " + hitPoints.ToString();
        Debug.Log("This is my hitpoint" + hitPoints.ToString());
        if (hitPoints <=0 )
        {
            healthbar.UpdateHealthBar(0, 0);
            Die();
        }

    }
    void Die()
    {
        Debug.Log("Player Died");


        animator.SetBool("isDead", true);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Movement>().enabled = false;
        GetComponent<PlayerMechanics>().enabled = false;

        //Disable enemy
        //GetComponent<Collider2D>().enabled = false;
    }



}
