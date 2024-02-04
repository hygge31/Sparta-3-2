using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    
    public List<Item> items = new List<Item>(100); //todo
    
    public GameObject itemHolder;
    public Transform content;


    public TMPro.TextMeshProUGUI currentIneventroyCapacityText;
    public event Action OnChangeCurentInventoryCapacity;

    [Header("Status Info")]
    public ItemInfoUI statusInfoUI;




    private void Awake()
    {
        Instance = this;
        OnChangeCurentInventoryCapacity += ChangeCurrentInventoryCapacityText;
    }

    private void Start()
    {
        InventoryinventoryCleanup();
        ChangeCurrentInventoryCapacityText();
    }

    public void CallOnChangeCurentInventoryCapacity()
    {
        OnChangeCurentInventoryCapacity?.Invoke();
    }


    public void InventoryinventoryCleanup() //text cod, marchent Logic
    {
        if(items.Count != 0)
        {
            foreach(Transform item in content)
            {
                Destroy(item.gameObject);
            }

            foreach (Item itemSO in items)
            {
                if(itemSO.itemType == ItemType.Consum)
                {
                    GameObject item = Instantiate(itemHolder, content);

                    item.AddComponent<ItemController>();
                    item.GetComponent<ItemController>().item = itemSO;
                    
                    var itemIcon = item.transform.Find("Icon").GetComponent<Image>();
                    itemIcon.sprite = itemSO.icon;
                    if (itemSO.amount > 1)
                    {
                        Transform curItemTransform = item.transform.Find("Amount");
                        curItemTransform.Find("AmountText").GetComponent<TMPro.TextMeshProUGUI>().text = itemSO.amount.ToString();

                        if (!curItemTransform.gameObject.activeSelf)
                        {
                            curItemTransform.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    CreateItem(itemSO);
                }
               
            }

            //marchent item info, -> Ex)  itemController OnClickEenet
        }
        
    }

    void CreateItem(Item itemSO)
    {
        GameObject item = Instantiate(itemHolder, content);
        item.AddComponent<ItemController>();
        item.GetComponent<ItemController>().item = itemSO;
        var itemIcon = item.transform.Find("Icon").GetComponent<Image>();
        itemIcon.sprite = itemSO.icon;
    }


    void ChangeCurrentInventoryCapacityText()
    {
        int curInventoryCapacity = items.Count;
        currentIneventroyCapacityText.text = curInventoryCapacity.ToString();
    }

    public void AddItem(Item item, int amount)
    {
        if(item.itemType == ItemType.Consum)
        {
            if (items.Contains(item))
            {
                ChangeItemAmountUI(item, amount);
            }
            else
            {
                items.Add(item);
                CreateItem(item);
                CallOnChangeCurentInventoryCapacity();
            }
        }
        else
        {
            Item newItem = item.Copy();
            items.Add(newItem);
            CreateItem(newItem);
        }

        
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        foreach(Transform tItem in content)
        {
            Item curItem = tItem.GetComponent<ItemController>().item;
            if(curItem == item)
            {
                Destroy(tItem.gameObject);
            }
        }
        CallOnChangeCurentInventoryCapacity();
    }



    public void ChangeItemAmountUI(Item _item , int amount) //Item Type == Consum, increase Amount. if consum item amount less 0, then Remove item.
    {

        foreach(Transform curTransformItem in content)
        {
            if (curTransformItem.GetComponent<ItemController>())
            {
                Item curItem = curTransformItem.GetComponent<ItemController>().item;
                if (curItem == _item)
                {
                    Transform curItemTransform = curTransformItem.Find("Amount");
                    TMPro.TextMeshProUGUI amountText = curItemTransform.Find("AmountText").GetComponent<TMPro.TextMeshProUGUI>();

                    int curAmount = int.Parse(amountText.text);
                    curAmount += amount;
                    if(curAmount <= 0)
                    {
                        items.Remove(curItem);
                        Destroy(curTransformItem.gameObject);
                    }
                    else if(curAmount >1)
                    {
                        amountText.text = curAmount.ToString();
                        curItemTransform.gameObject.SetActive(true);
                    }else if(curAmount == 1)
                    {
                        amountText.text = curAmount.ToString();
                        curItemTransform.gameObject.SetActive(false);
                    }
                    
                }
            }
           
        }

    }

    public void SetEquipItem(Item item,bool trueAndFalse)
    {
        //장착 또는 해제 처리.

        //먼저 해당 아이템 트렌스폼으로 찾기.
        // 장창 해제인지 장착인지 확인후 처리.
    }



    
    public void TestBtn()
    {
        AddItem(items[7], 1);
    }

    public void TestBtn2()
    {
        if (items[7] == items[items.Count - 1])
        {
            Debug.Log("true");
        }
        else { Debug.Log("fasle"); }

    }




}
