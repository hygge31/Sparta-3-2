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
                Destroy(item);
            }

            foreach (Item itemSO in items)
            {
                if(itemSO.itemType == ItemType.Consum)
                {
                    GameObject item = Instantiate(itemHolder, content);
                    var itemIcon = item.transform.Find("Icon").GetComponent<Image>();
                    itemIcon.sprite = itemSO.icon;
                    if (itemSO.amount > 1)
                    {
                        item.transform.Find("Amount").gameObject.SetActive(true);
                        item.transform.Find("Amount").GetComponent<TMPro.TextMeshProUGUI>().text = itemSO.amount.ToString();
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



    public void ChangeItemAmountUI(Item _item , int amount) //Item Type == Consum
    {
        foreach(Transform itemTransform in content)
        {
            Debug.Log(itemTransform.gameObject.GetComponent<Item>().id);
            //if(item.gameObject.GetComponent<Item>().id == _item.id)
            //{
            //    Debug.Log("Find");
            //    //Item curItem = item.GetComponent<Item>().Copy();
            //    //curItem.amount+=amount;
            //    //item.transform.Find("Amount").GetComponent<TMPro.TextMeshProUGUI>().text = curItem.amount.ToString();
            //    //_item = curItem;
            //}

        }
    }

    public void TestClick()
    {
        ChangeItemAmountUI(items[0], 1);
    }


}
