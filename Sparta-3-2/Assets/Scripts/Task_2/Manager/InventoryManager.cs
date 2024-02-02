using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    
    public List<Item> items = new List<Item>(); //todo


    public GameObject itemHolder;
    public Transform content;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InventoryinventoryCleanup();
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
                    var itemIcon = item.transform.Find("Icon").GetComponent<Image>();
                    itemIcon.sprite = itemSO.icon;
                    Debug.Log(itemSO.amount);
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
                    var itemIcon = item.transform.Find("Icon").GetComponent<Image>();
                    itemIcon.sprite = itemSO.icon;
                }
               
            }
        }
        
    }


    //public void AddItem(Item item,int amount)
    //{
    //    if (items.Contains(item))
    //    {
    //        if(item.itemType == ItemType.Consum)
    //        {
    //            ChangeItemAmountUI(item, amount);
    //        }
    //    }
    //    else
    //    {
    //        items.Add(item);
    //    }

        
    //    InventoryinventoryCleanup();
    //}

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        InventoryinventoryCleanup();
    }



    public void ChangeItemAmountUI(Item _item , int amount) //Item Type == Consum, increase Amount.
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == _item.id)
            {
                Item newItemOS = ScriptableObject.CreateInstance<Item>();
                newItemOS = _item.Copy();
                newItemOS.amount += amount;
                items[i] = newItemOS;
                InventoryinventoryCleanup();
            }
        }
    }

    public void TestClick()
    {
        ChangeItemAmountUI(items[0], 1);
    }


}
