using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Shield,
    Consum
}
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public ItemType type;
    public Sprite icon;

    [Header("Type Weapon")]
    public int damage;
    public int critical;
    [Header("Type Shield")]
    public int shield;    
    [Header("Type Consum")]
    public int healAmount;

    [Header("ETC")]
    public int health;
    public int price;

}
