using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [Header("Item Info Container")]
    public Image icon;
    public TMPro.TextMeshProUGUI  itemNameText;
    public TMPro.TextMeshProUGUI itemInfoText;

    [Header("Item Status Container")]
    public GameObject itemStatusInfoBox;
    public Transform itemStatusInfoBoxContainer;

    [Header("Rect Transform")]
    public RectTransform itemInfoUIRect;
    public RectTransform itemStatusContainerRect;
    int itemInfoUiRectHeight = 333;
    int itemStatusContainerHeight = 100;

    List<ItemStatus> list = new List<ItemStatus>();

    [Header("Stauts Sprite")]
    public Sprite damageSprite;
    public Sprite shieldSprite;
    public Sprite criticalSprite;
    public Sprite healthSprite;


    public void SetItemInfo(Item item)
    {
        icon.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemInfoText.text = item.itemInfo;
        list = item.GetItemStatus();

        ChangeRectSize();
        CreateItemStatusInfoBox();

    }
    

    void ChangeRectSize()
    {
        if(list.Count > 2)
        {
            itemInfoUIRect.sizeDelta = new Vector2(itemInfoUIRect.sizeDelta.x, 533);
            itemStatusContainerRect.sizeDelta = new Vector2(itemStatusContainerRect.sizeDelta.x, 200);
        }
        else
        {
            itemInfoUIRect.sizeDelta = new Vector2(itemInfoUIRect.sizeDelta.x, itemInfoUiRectHeight);
            itemStatusContainerRect.sizeDelta = new Vector2(itemStatusContainerRect.sizeDelta.x, itemStatusContainerHeight);
        }

    }


    void CreateItemStatusInfoBox()
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject infoBox = Instantiate(itemStatusInfoBox, itemStatusInfoBoxContainer);
            ItemStatus itemStatus = list[i];
            switch (itemStatus.statusName)
            {
                case "damage":
                    infoBox.GetComponent<ItemStatusInfoBox>().SetInfo(damageSprite, itemStatus.statusName, itemStatus.status);
                    break;
                case "critical":
                    infoBox.GetComponent<ItemStatusInfoBox>().SetInfo(criticalSprite, itemStatus.statusName, itemStatus.status);
                    break;
                case "shield":
                    infoBox.GetComponent<ItemStatusInfoBox>().SetInfo(shieldSprite, itemStatus.statusName, itemStatus.status);
                    break;
                case "health":
                    infoBox.GetComponent<ItemStatusInfoBox>().SetInfo(healthSprite, itemStatus.statusName, itemStatus.status);
                    break;
            }

            
        }
    }

}
