using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.UI;

public class MerchantItemBox : Util
{
    public Item item;

    public Image icon;
    public TMPro.TextMeshProUGUI itemNameText;
    public TMPro.TextMeshProUGUI itemInfoText;   
    public TMPro.TextMeshProUGUI priceText;
    public TMPro.TextMeshProUGUI message;
    public GameObject panel;

    public Button infoBtn;
    public Button buyBtn;


    private void Awake()
    {
        infoBtn.onClick.AddListener(() => OnClickInfoBtn(item));
        buyBtn.onClick.AddListener(() =>  OnClickBuyBtn(item));
    }

    public void SetMerchantItemBox(Item item)
    {
        this.item = item;
        icon.sprite = item.icon;
        itemNameText.text = item.itemName;
        priceText.text = GetThousandCommaText(item.price);
        
    }



    void OnClickInfoBtn(Item item)
    {
        Task_2_UIManager.Instance.OnItemInfoUiNoConfirmBtn(item);
    }


    public void OnClickBuyBtn(Item item)
    {
        if (GameManager.Instance.player.money < item.price)
        {
            Color color = Color.red;
            MerchantContainer.Instance.Message(color, "보유 금액이 부족합니다.");
        }
        else
        {
            GameManager.Instance.player.money -= item.price;
            InventoryManager.Instance.AddItem(item, 1);
            Task_2_UIManager.Instance.CallOnStatusChange();
            Color color = Color.blue;
            MerchantContainer.Instance.Message(color, "구입 완료.");
            panel.SetActive(true);
        }


    }
    





}
