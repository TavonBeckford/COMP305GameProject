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

    private void Start()
    {
        healthbar.UpdateHealthBar(maxHitPoints, hitPoints);
        energybar.UpdateEnergyBar(maxEnergy, energyPoints);
        quantityText.text = "Health: " + hitPoints.ToString();
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
    private void AdjustEnergyPoints(int amount)
    {
        if (energyPoints < maxEnergy)
        {

            energyPoints += amount;
        }
        energybar.UpdateEnergyBar(maxEnergy, energyPoints);
        print($"Adjusted energy points by {amount}. New Value: {energyPoints}");
    }

    public void TakeDamage(int dmg)
    {

        hitPoints -= dmg;
        //gameObject.GetComponent<Player>().TakeDamage();
        healthbar.UpdateHealthBar(maxHitPoints, hitPoints);
        quantityText.text = "Health: " + hitPoints.ToString();
        if (hitPoints <=0 )
        {
            healthbar.UpdateHealthBar(0, 0);
            Destroy(gameObject);
        }

    }

    
}
