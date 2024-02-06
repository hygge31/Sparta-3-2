using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantConsumItemBox : Util
{
    public Item item;

    public Image icon;
    public TMPro.TextMeshProUGUI itemNameText;
    public TMPro.TextMeshProUGUI priceText;

    public Button buyBtn;

    private void Awake()
    {
        buyBtn.onClick.AddListener(() => OnClickBuyBtn(item));
    }

    public void SetMerchantItemBox(Item item)
    {
        this.item = item;
        icon.sprite = item.icon;
        itemNameText.text = item.itemName;
        priceText.text = GetThousandCommaText(item.price);
    }

    void OnClickBuyBtn(Item item)
    {
        Task_2_UIManager.Instance.OnItemConsumInfoUI(item);
    }

}
