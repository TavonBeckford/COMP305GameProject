using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int hitPoints;
    public int maxHitPoints = 100;
    public int energyPoints;
    public int maxEnergy;
    public int attackPoints;
    public int specialAttackPoints;

    void Start()
    {
        hitPoints = maxHitPoints;
    }

    

}
