using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task_2_UIManager : Util
{
   
    public static Task_2_UIManager Instance;
    Player player;

    [Header("Player Status")]
    public TMPro.TextMeshProUGUI playerLevelText;
    public TMPro.TextMeshProUGUI playerNameText;
    public Slider playerExpBar;
    [Space(10)]
    public TMPro.TextMeshProUGUI attackText;
    public TMPro.TextMeshProUGUI attackIncreaseText;
    [Space(10)]
    public TMPro.TextMeshProUGUI shieldText;
    public TMPro.TextMeshProUGUI shieldIncreaseText;
    [Space(10)]
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI healthIncreaseText;
    [Space(10)]
    public TMPro.TextMeshProUGUI critcalText;
    public TMPro.TextMeshProUGUI critcalIncreaseText;

    [Space(10)]
    public TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI expText;

    public event Action OnStatusChange;
    public event Action OnStatusChangeEXP;

    [Header("UI")]
    public ItemInfoUI itemInfoUi;
    public ItemConsumInfoUI itemConsumInfoUI;


    [Header("Merchant")]
    float resetTime = 300;
    public float curTime;
    public bool changeMerchantItem;

    private void Awake()
    {
        Instance = this;

        OnStatusChange += ChangeUIPlayerStatus;
        OnStatusChangeEXP += ChangeUIPlayerEXP;
    }


    private void Start()
    {
        player = GameManager.Instance.player;
        playerNameText.text = player.playerName;
        playerLevelText.text = player.playerLevel.ToString();
        playerExpBar.maxValue = player.maxEXP;
        playerExpBar.value = 0;
        ChangeUIPlayerStatus();
        ChangeUIPlayerEXP();

        StartCoroutine(ItemResetTimer());
    }
    public void CallOnStatusChange()
    {
        OnStatusChange?.Invoke();
    }
    public void CallOnStatusChangeEXP()
    {
        OnStatusChangeEXP?.Invoke();
    }




    void ChangeUIPlayerStatus()
    {
        attackText.text = player.attackDamage.ToString();
        shieldText.text = player.shield.ToString();
        healthText.text = player.health.ToString();
        critcalText.text = player.critcal.ToString();
        moneyText.text = GetThousandCommaText(player.money);

        List<ItemStatus> playerIncreaseStautsList = player.GetPlayerIncreaseStatus();

        foreach(ItemStatus status in playerIncreaseStautsList)
        {
            
                switch (status.statusName)
                {
                    case "damage":
                        if(status.status < 0)
                        {
                            attackIncreaseText.color = Color.red;
                            attackIncreaseText.text = status.status.ToString();
                        }
                        else if(status.status > 0)
                        {
                            attackIncreaseText.color = Color.blue;
                            attackIncreaseText.text = $"+ {status.status}";
                        }
                        else
                        {
                            attackIncreaseText.text = " ";
                        }
                        break;
                    case "critical":
                        if (status.status < 0)
                        {
                            critcalIncreaseText.color = Color.red;
                            critcalIncreaseText.text = status.status.ToString();
                        }
                        else if (status.status > 0)
                        {
                            critcalIncreaseText.color = Color.blue;
                            critcalIncreaseText.text = $"+ {status.status}";
                        }
                        else
                        {
                            critcalIncreaseText.text = " ";
                        }
                        break;
                    case "shield":
                        if (status.status < 0)
                        {
                            shieldIncreaseText.color = Color.red;
                            shieldIncreaseText.text = status.status.ToString();
                        }
                        else if (status.status > 0)
                        {
                            shieldIncreaseText.color = Color.blue;
                            shieldIncreaseText.text = $"+ {status.status}";
                        }
                        else
                        {
                            shieldIncreaseText.text = " ";
                        }
                        break;
                    case "health":
                        if (status.status < 0)
                        {
                            healthIncreaseText.color = Color.red;
                            healthIncreaseText.text = status.status.ToString();
                        }
                        else if (status.status > 0)
                        {
                            healthIncreaseText.color = Color.blue;
                            healthIncreaseText.text = $"+ {status.status}";
                        }
                        else
                        {
                            healthIncreaseText.text = " ";
                        }
                        break;
                }
            }
        
    }

    void ChangeUIPlayerEXP()
    {
        expText.text = $"{player.currentEXP} / {player.maxEXP}";
        ChangeUIPlayerEXPSlider(player.currentEXP,player.maxEXP);
    }

    void ChangeUIPlayerEXP(int exp) //Get EXP
    {
        player.currentEXP += exp;
        if (player.currentEXP <= player.maxEXP) //Level Up
        {
            player.currentEXP -= player.maxEXP;
            player.playerLevel++;
            player.maxEXP *= (int)(player.maxEXP * 0.5f);
            ChangeUIPlayerEXPSlider(player.currentEXP, player.maxEXP);
            playerLevelText.text = player.playerLevel.ToString();
            CallOnStatusChangeEXP();
        }
        else
        {
            CallOnStatusChangeEXP();
        }
        
    }

    void ChangeUIPlayerEXPSlider(int curExt,int maxExp)
    {
        playerExpBar.maxValue = maxExp;
        playerExpBar.value = curExt;
    }




    public void OnItemInfoUI(Item item) // Open Iteminfo Ui and Set item info.
    {
        itemInfoUi.SetItemInfo(item);
        itemInfoUi.gameObject.SetActive(true);
    }
    public void OnItemInfoUiNoConfirmBtn(Item item)
    {
        itemInfoUi.SetItemInfo(item);
        itemInfoUi.UnActiveConfirmBtn();
        itemInfoUi.gameObject.SetActive(true);
    }

    public void OnItemConsumInfoUI(Item item)
    {
        itemConsumInfoUI.SetItemInfo(item);
        itemConsumInfoUI.gameObject.SetActive(true);
    }



    IEnumerator ItemResetTimer()
    {
        curTime = resetTime;
        while (true)
        {
            if (curTime <= 0)
            {
                curTime = resetTime;
                changeMerchantItem = true;
            }
           
            curTime--;
            yield return new WaitForSeconds(1f);
        }
    }

}


public struct ItemStatus
{
    public string statusName;
    public int status;

    public ItemStatus(string n,int s)
    {
        statusName = n;
        status = s;
    }
}