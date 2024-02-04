using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
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


    public void InventoryinventoryCleanup()
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
                    //todo
                    
                        item.AddComponent<ItemController>();
                        item.GetComponent<ItemController>().item = itemSO;
                    //todo
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
                    GameObject item = Instantiate(itemHolder, content);
                    item.AddComponent<ItemController>();
                    item.GetComponent<ItemController>().item = itemSO;
                    var itemIcon = item.transform.Find("Icon").GetComponent<Image>();
                    itemIcon.sprite = itemSO.icon;
                }
               
            }
        }
        
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
                InventoryinventoryCleanup();
                CallOnChangeCurentInventoryCapacity();
            }
        }
        else
        {
            items.Add(item);
            InventoryinventoryCleanup();
            CallOnChangeCurentInventoryCapacity();
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
    }



    public void ChangeItemAmountUI(Item _item , int amount) //Item Type == Consum, increase Amount. if consum item amount less 0, then Remove item.
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == _item.id)
            {
                Item newItemOS = ScriptableObject.CreateInstance<Item>();
                newItemOS = _item.Copy();
                newItemOS.amount += amount;

                if(newItemOS.amount <= 0)
                {
                    items.RemoveAt(i);
                    RemoveItem(_item);
                }
                else { items[i] = newItemOS; }

            }
        }
    }


    






}
