using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatusInfoBox : MonoBehaviour
{
    public Image icon;
    public TMPro.TextMeshProUGUI statusInfoText;
    public TMPro.TextMeshProUGUI statusText;


    public void SetInfo(Sprite _icon,string status,int num)
    {
        icon.sprite = _icon;
        statusInfoText.text = status;
        statusText.text = num.ToString();
    }

}
