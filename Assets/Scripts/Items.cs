using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item")]



public class Items : ScriptableObject
{
    public string objectName;
    public Sprite sprite;
    public int quantity;
    public int increaseBy;
    public bool stackable;

    public enum ItemType
    {

        HEALTHPOTION,
        ENERGYPOTION

    }

    public ItemType itemType;

}
