using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField]
    Item curItem;

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
    int itemInfoUiRectHeight = 363;
    int itemStatusContainerHeight = 100;

    List<ItemStatus> list = new List<ItemStatus>();

    [Header("Stauts Sprite")]
    public Sprite damageSprite;
    public Sprite shieldSprite;
    public Sprite criticalSprite;
    public Sprite healthSprite;
    public Sprite healAmountSprtie;

    [Header("Etc")]
    public TMPro.TextMeshProUGUI message;


    [Header("Button")]
    public Button cencelBtn;
    public Button confirmBtn;

    public GameObject merchantContainerUI;
    bool inMerchantContainerUI;

    private void Awake()
    {
        cencelBtn.onClick.AddListener(() => Cencel());
        confirmBtn.onClick.AddListener(() => Confirm());
    }

    public void SetItemInfo(Item item)
    {

        Check_MerchantUi();

        curItem = item;
        icon.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemInfoText.text = item.itemInfo;
        list = item.GetItemStatus();
        CleanUp();
        ChangeRectSize();
        CreateItemStatusInfoBox();
        ShowMessage();

    }

    void Check_MerchantUi()
    {
        if (!merchantContainerUI.activeSelf)
        {
            confirmBtn.gameObject.SetActive(true);
            inMerchantContainerUI = false;
        }
        else
        {
            confirmBtn.gameObject.SetActive(false);
            inMerchantContainerUI = true;
        }
    }

    void CleanUp()
    {
        foreach(Transform t in itemStatusInfoBoxContainer)
        {
            Destroy(t.gameObject);
        }
    }

    void ShowMessage()
    {
        if(!inMerchantContainerUI)
        {
            if (curItem.itemType == ItemType.Consum)
            {
                message.text = "아이템을 사용하시겠습니까?";
            }
            else
            {
                if (curItem != GameManager.Instance.player.weaponEquip && curItem != GameManager.Instance.player.shieldEquip)
                {
                    message.text = "장비를 장착하시겠습니까?";
                }
                else
                {
                    message.text = "장비를 해제하시겠습니까?";
                }

                
            }
        }
        else
        {
            message.text = "아이템을 파시겠습니까";
        }
        
    }


    void ChangeRectSize()
    {
        if(list.Count > 2)
        {
            itemInfoUIRect.sizeDelta = new Vector2(itemInfoUIRect.sizeDelta.x, 463);
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
                case "healAmount":
                    infoBox.GetComponent<ItemStatusInfoBox>().SetInfo(healAmountSprtie, itemStatus.statusName, itemStatus.status);
                    break;
            }

            
        }
    }

    public void UnActiveConfirmBtn()
    {
        confirmBtn.gameObject.SetActive(false);
        message.text = "";
    }

    void Cencel()
    {
        gameObject.SetActive(false);
    }

    void Confirm()
    {
        if (inMerchantContainerUI)
        {
            //판매
            if(curItem.itemType != ItemType.Consum)
            {
                if(GameManager.Instance.player.weaponEquip != curItem && GameManager.Instance.player.shieldEquip != curItem)
                {
                    GameManager.Instance.player.money += (int)(curItem.price * 0.7f);
                    InventoryManager.Instance.RemoveItem(curItem);
                }
                else
                {
                    GameManager.Instance.player.SetEquipItem(curItem);
                    GameManager.Instance.player.money += (int)(curItem.price * 0.7f);
                    InventoryManager.Instance.RemoveItem(curItem);
                }
            }
        }
        else if(curItem.itemType == ItemType.Consum)
        {
            //heal player HP, and Check excced player maxhp
            InventoryManager.Instance.ChangeItemAmountUI(curItem, -1);
            gameObject.SetActive(false);

        }   
        else
        {
            GameManager.Instance.player.SetEquipItem(curItem);
            gameObject.SetActive(false);
       
        }
    }


}
