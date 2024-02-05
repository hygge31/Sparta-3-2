using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int playerLevel = 1;

    public int attackDamage = 40;
    public int attackDamageIncrease=0;
    public int shield = 25;
    public int shieldIncrease=0;
    public int health = 70;
    public int healthIncrease=0;
    public int critcal = 25;
    public int critcalIncrease=0;


    public int curHp;
    public int maxHp;


    public int money = 10000;

    public int currentEXP = 0;
    public int maxEXP;


    public void SetPlayerStatus(Item item)
    {
        List<ItemStatus> itemStatusList = new List<ItemStatus>();
        itemStatusList = item.GetItemStatus();

    }



    public Item weaponEquip;
    public Item shieldEquip;




    public List<ItemStatus> GetPlayerIncreaseStatus()
    {
        List<ItemStatus> list = new List<ItemStatus>();
        list.Add(new ItemStatus("damage", attackDamageIncrease));
        list.Add(new ItemStatus("critical", critcalIncrease)); 
        list.Add(new ItemStatus("shield", shieldIncrease)); 
        list.Add(new ItemStatus("health", healthIncrease)); 

        return list;

    }

    void SetPlayerStatus(List<ItemStatus> list,bool trueAndFalse)
    {
        if (trueAndFalse)
        {
            foreach (ItemStatus status in list)
            {
                switch (status.statusName)
                {
                    case "damage":
                        attackDamageIncrease += status.status;
                        break;
                    case "critical":
                        critcalIncrease += status.status;
                        break;
                    case "shield":
                        shieldIncrease += status.status;
                        break;
                    case "health":
                        healthIncrease += status.status;
                        break;
                }

            }
        }
        else
        {
            foreach (ItemStatus status in list)
            {
                switch (status.statusName)
                {
                    case "damage":
                        attackDamageIncrease += status.status *(-1);
                        break;
                    case "critical":
                        critcalIncrease += status.status * (-1);
                        break;
                    case "shield":
                        shieldIncrease += status.status * (-1);
                        break;
                    case "health":
                        healthIncrease += status.status * (-1);
                        break;
                }

            }
        }

        Task_2_UIManager.Instance.CallOnStatusChange();
    }


    public void SetEquipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                if(item == weaponEquip)
                {
                    SetPlayerStatus(item.GetItemStatus(), false);
                    InventoryManager.Instance.SetEquipItem(item);
                    weaponEquip = null;
                }
                else if(weaponEquip == null)
                {
                    weaponEquip = item;
                    SetPlayerStatus(item.GetItemStatus(), true);
                    InventoryManager.Instance.SetEquipItem(item);
                }
                else
                {
                    SetPlayerStatus(weaponEquip.GetItemStatus(), false);
                    InventoryManager.Instance.SetEquipItem(weaponEquip);
                    SetPlayerStatus(item.GetItemStatus(), true);
                    InventoryManager.Instance.SetEquipItem(item);
                    weaponEquip = item;
                }
                break;
            case ItemType.Shield:
                if (item == shieldEquip)
                {
                    SetPlayerStatus(item.GetItemStatus(), false);
                    InventoryManager.Instance.SetEquipItem(item);
                    shieldEquip = null;
                }
                else if (shieldEquip == null)
                {
                    shieldEquip = item;
                    SetPlayerStatus(item.GetItemStatus(), true);
                    InventoryManager.Instance.SetEquipItem(item);
                }
                else
                {
                    SetPlayerStatus(shieldEquip.GetItemStatus(), false);
                    InventoryManager.Instance.SetEquipItem(shieldEquip);
                    SetPlayerStatus(item.GetItemStatus(), true);
                    InventoryManager.Instance.SetEquipItem(item);
                    shieldEquip = item;
                }
                break;
        }

    }


}
