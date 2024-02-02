using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUi : MonoBehaviour
{
    public GameObject btnContainer;
    public GameObject statusContainer;
    public GameObject inventoryContainer;



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
        if (statusContainer.activeSelf)
        {
            inventoryContainer.SetActive(false);
            btnContainer.SetActive(true);
        }
        else
        {
            inventoryContainer.SetActive(true);
            btnContainer.SetActive(false);
        }
    }

}
