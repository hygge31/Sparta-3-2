using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int playerLevel = 1;

    public int attackDamage = 40;
    public int attackDamageIncrease;
    public int shield = 25;
    public int shieldIncrease;
    public int health = 70;
    public int healthIncrease;
    public int critcal = 25;
    public int critcalIncrease;


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

    public List<ItemStatus> GetPlayerIncreaseStatus()
    {
        List<ItemStatus> list = new List<ItemStatus>();
        if (attackDamageIncrease != 0) { list.Add(new ItemStatus("damage", attackDamageIncrease)); }
        if (critcalIncrease != 0) { list.Add(new ItemStatus("critical", critcalIncrease)); }
        if (shieldIncrease != 0) { list.Add(new ItemStatus("shield", shieldIncrease)); }
        if (healthIncrease != 0) { list.Add(new ItemStatus("health", healthIncrease)); }

        return list;

    }

    public void SetPlayerStatus(List<ItemStatus> list)
    {
        foreach(ItemStatus status in list)
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

        Task_2_UIManager.Instance.CallOnStatusChange();
    }


    public void SetEquipItem(Item item)
    {
        //asd
    }


}
