using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUi : MonoBehaviour
{
    public GameObject btnContainer;
    public GameObject statusContainer;
    public GameObject merchantContainer;


    public GameObject inventoryContainer;
    public GameObject itemInfoUI;


    public void ToggleStatusContainer()
    {
        if (statusContainer.activeSelf)
        {
            statusContainer.SetActive(false);
            btnContainer.SetActive(true);
        }
        else
        {
            statusContainer.SetActive(true);
            btnContainer.SetActive(false);
        }
    }

    public void ToggleInventoryContainer()
    {
        if (inventoryContainer.activeSelf)
        {
            itemInfoUI.SetActive(false);
            inventoryContainer.SetActive(false);
            btnContainer.SetActive(true);
        }
        else
        {
            inventoryContainer.SetActive(true);
            btnContainer.SetActive(false);
        }
    }


    public void ToggleMerchantContainer()
    {
        if (merchantContainer.activeSelf)
        {
            merchantContainer.SetActive(false);
            inventoryContainer.SetActive(false);
            btnContainer.SetActive(true);
        }
        else
        {
            merchantContainer.SetActive(true);
            inventoryContainer.SetActive(true);
            btnContainer.SetActive(false);
        }
    }



}
