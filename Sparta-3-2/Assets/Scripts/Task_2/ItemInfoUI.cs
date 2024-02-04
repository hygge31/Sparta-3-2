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

    public GameObject inventoryContainerUI;
    bool inInventoryUI;

    private void Awake()
    {
        cencelBtn.onClick.AddListener(() => Cencel());
        confirmBtn.onClick.AddListener(() => Confirm());
    }

    public void SetItemInfo(Item item)
    {

        Check_InInventoryUi();

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

    void Check_InInventoryUi()
    {
        if (inventoryContainerUI.activeSelf)
        {
            confirmBtn.gameObject.SetActive(true);
            inInventoryUI = true;
        }
        else
        {
            confirmBtn.gameObject.SetActive(false);
            inInventoryUI = false;
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
        if(inInventoryUI)
        {
            if (curItem.itemType == ItemType.Consum)
            {
                message.text = "아이템을 사용하시겠습니까?";
            }
            else
            {
                message.text = "장비를 장착하시겠습니까?";
            }
        }
        else
        {
            message.text = "";
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


    void Cencel()
    {
        gameObject.SetActive(false);
    }

    void Confirm()
    {
        if(curItem.itemType == ItemType.Consum)
        {
            //heal player HP, and Check excced player maxhp
            InventoryManager.Instance.ChangeItemAmountUI(curItem, -1);
            gameObject.SetActive(false);

        }
        else
        {
            //스위치 웨폰인지 아머인지.
            //플레이어의 장착했는지 안했는지 확인,
            //장착안했으면 바로 아이템 적용 ,인벤토리매니저의 장착 처리.

            //플레이어가 이미 장착하고 있는경우에만 플레이어에게 파라미터 전달.


            //현재 커런트아이템과 플레이어의 장착 아이템이 같으면 플레이어 스텟마이너스
            //바로 인벤토리매니저의 장착 해제 로직으로.
        }
    }


}
