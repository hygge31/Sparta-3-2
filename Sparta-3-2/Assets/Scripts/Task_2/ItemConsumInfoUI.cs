using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemConsumInfoUI : MonoBehaviour
{
    public Item item;

    public Image icon;
    public TMPro.TextMeshProUGUI itemNameText;
    public TMPro.TextMeshProUGUI iteminfoText;
    public TMPro.TextMeshProUGUI itemHealAmountText;
    public TMPro.TextMeshProUGUI message;
    public TMPro.TextMeshProUGUI subMessage;

    public TMPro.TMP_InputField itemAmountInputField;

    public Button upBtn;
    public Button downBtn;
    public Button cencelBtn;
    public Button confirmBtn;


    private void Awake()
    {
        upBtn.onClick.AddListener(() => UpBtn());
        downBtn.onClick.AddListener(() => DownBtn());
        cencelBtn.onClick.AddListener(() => CancelBtn());
        confirmBtn.onClick.AddListener(() => Confirm());
    }


    public void SetItemInfo(Item _item)
    {
        item = _item;
        icon.sprite = _item.icon;
        itemNameText.text = _item.name;
        iteminfoText.text = _item.itemInfo;
        itemHealAmountText.text = _item.healAmount.ToString();
        itemAmountInputField.text = "1";


    }


    void UpBtn()
    {
        int num = int.Parse(itemAmountInputField.text);
        num++;
        itemAmountInputField.text = num.ToString();
        ChangeInputFieldText();
    }
    void DownBtn()
    {

        int num = int.Parse(itemAmountInputField.text);
        num--;
        if(num < 1)
        {
            itemAmountInputField.text = "1";
        }
        else
        {
            itemAmountInputField.text = num.ToString();
        }
        
        ChangeInputFieldText();
    }
    void CancelBtn()
    {
        gameObject.SetActive(false);
    }


    public void ChangeInputFieldText()
    {
        Color color = Color.yellow;
        int num = int.Parse(itemAmountInputField.text);
        Message(color, $"필요 코인 : {item.price * num}",message);
    }


    void Message(Color color,string meg, TextMeshProUGUI text)
    {
        text.color = color;
        text.text = meg;
    }

    IEnumerator ShowMeg(Color color,string mesg)
    {
        subMessage.color = color;
        subMessage.text = mesg;
        yield return new WaitForSeconds(0.5f);
        subMessage.text = "";
    }


    void Confirm()
    {
        int playerMoney = GameManager.Instance.player.money;
        int totalPrice = item.price * int.Parse(itemAmountInputField.text);
        if(playerMoney < totalPrice)
        {
            Color color = Color.red;
            StartCoroutine(ShowMeg(color, "보유 코인이 부족합니다."));
        }
        else
        {
            Color color = Color.blue;
            GameManager.Instance.player.money -= totalPrice;
            Task_2_UIManager.Instance.CallOnStatusChange();
            InventoryManager.Instance.AddItem(item, int.Parse(itemAmountInputField.text));
            StartCoroutine(ShowMeg(color, "구매 완료"));
            itemAmountInputField.text = "1";
         
        }
    }
}
