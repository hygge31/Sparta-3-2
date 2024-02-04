using System;
using System.Collections;
using System.Collections.Generic;
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
    public TMPro.TextMeshProUGUI attackText;
    public TMPro.TextMeshProUGUI shieldText;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI critcalText;
    public TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI expText;

    public event Action OnStatusChange;
    public event Action OnStatusChangeEXP;

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