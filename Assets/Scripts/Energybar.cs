using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energybar : MonoBehaviour
{
    [SerializeField] private Image _energybarSprite;


    public void UpdateEnergyBar(float maxEnergy, float currentEnergy)
    {
        _energybarSprite.fillAmount = currentEnergy / maxEnergy;
    }

}
