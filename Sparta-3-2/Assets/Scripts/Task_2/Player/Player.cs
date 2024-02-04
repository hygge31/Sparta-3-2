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
                    weaponEquip = null;
                }
                else if(weaponEquip == null)
                {
                    weaponEquip = item;
                    SetPlayerStatus(item.GetItemStatus(), true);
                }
                else
                {
                    SetPlayerStatus(weaponEquip.GetItemStatus(), false);
                    SetPlayerStatus(item.GetItemStatus(), true);
                    weaponEquip = item;
                }

                break;
            
        }

        //웨폰의 경우
        //웨폰이 착용중이라면 현재 착용죽인 아이템을 벗고 -> - 스텟, 인벤토리매니저 아이템 장착 해제
        //새로운 아이템 장착. -> +스텟, 인벤토리 매니저 아이템 장,
        //아머의 경우

    }


}
