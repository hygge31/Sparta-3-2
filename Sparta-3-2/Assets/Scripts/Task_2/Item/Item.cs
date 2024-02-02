using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Weapon,
    Shield,
    Consum
}
[SerializeField]
[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public ItemType itemType;
    public Sprite icon;

    [Header("Type Weapon")]
    public int damage;
    public int critical;
    [Header("Type Shield")]
    public int shield;    
    [Header("Type Consum")]
    public int healAmount;
    public int amount = 1;
    [Header("ETC")]
    public int health;
    public int price;
    

    public Item Copy()
    {
        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.id = id;
        newItem.itemName = itemName;
        newItem.itemType = itemType;
        newItem.icon = icon;
        newItem.healAmount = healAmount;
        newItem.amount = amount;
        newItem.price = price;

        return newItem;


    }

}
