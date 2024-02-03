using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public Item item;
    public GameObject eq;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => OnClickEvent(item));
    }


    void OnClickEvent(Item item) // item status container 
    {

        InventoryManager.Instance.OnItemInfoUI(item);
        
    }
}
