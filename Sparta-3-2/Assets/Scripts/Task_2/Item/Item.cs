using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//0 ~ 10 consum
//100 ~ 199 shield
//200 ~ 299 weapon



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


    public string itemInfo;

    public Queue queue = new Queue();


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
        newItem.itemInfo = itemInfo;

        return newItem;

    }

    public List<ItemStatus> GetItemStatus()
    {
        List<ItemStatus> list = new List<ItemStatus>();
        if(damage != 0) { list.Add(new ItemStatus("damage", damage)); }
        if (critical != 0) { list.Add(new ItemStatus("critical", critical)); }
        if (shield != 0) { list.Add(new ItemStatus("shield", shield)); }
        if (healAmount != 0) { list.Add(new ItemStatus("healAmount", healAmount)); }
        if (health != 0) { list.Add(new ItemStatus("health", health)); }

        return list;

    }

    public int GetPrice()
    {
        return price;
    }

}
