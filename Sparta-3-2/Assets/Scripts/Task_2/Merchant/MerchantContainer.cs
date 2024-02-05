using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.Progress;

public class MerchantContainer : MonoBehaviour
{
    public static MerchantContainer Instance;

    private void Awake()
    {
        Instance = this;
    }


    [Header("Merchant Item")]
    public List<Item> merchantItemList = new List<Item>();
    public List<Item> merchantConsumItemList = new List<Item>();
    public GameObject merchantItemBox;
    public Transform content;



    [Header("Timer")]
    public TMPro.TextMeshProUGUI timeText;

    public TMPro.TextMeshProUGUI message;


    private void Start()
    {
        MerchantItemCleanup();
    }



    private void Update()
    {
        ItemResetTimer();
        
        if (Task_2_UIManager.Instance.changeMerchantItem)
        {
            Task_2_UIManager.Instance.changeMerchantItem = false;
            MerchantItemCleanup();
        }
    }



    void ItemResetTimer()
    {
        float curTime = Task_2_UIManager.Instance.curTime;
        int min = Mathf.FloorToInt(curTime / 60);
        int sec = Mathf.FloorToInt(curTime % 60);
        timeText.text = string.Format("{0:D2} : {1:D2}", min, sec);

       
    }

    public void TestBtn()
    {
        Task_2_UIManager.Instance.curTime = 300;
        MerchantItemCleanup();
    }


    public void MerchantItemCleanup() //text cod, marchent Logic
    {
        if (merchantItemList.Count != 0)
        {
            foreach (Transform item in content)
            {
                Destroy(item.gameObject);
            }

            MixUpList();

            for (int i = 0; i < 5; i++)
            {
               
                MerchantItemBox newMerchantItemBox = Instantiate(merchantItemBox, content).GetComponent<MerchantItemBox>();
                newMerchantItemBox.SetMerchantItemBox(merchantItemList[i]);


            }

        }

    }



    void MixUpList()
    {
        for (int i = 0; i < merchantItemList.Count-1; i++)
        {
            System.Random random = new System.Random();
            int randomIdx = random.Next(i, merchantItemList.Count);
            Item itemA = merchantItemList[randomIdx];
            Item term = merchantItemList[i];
            merchantItemList[i] = itemA;
            merchantItemList[randomIdx] = term;
        }
    }


    public void Message(Color color,string sentence)
    {
        StartCoroutine(MessageCO(color,sentence));
    }
    IEnumerator MessageCO(Color color, string sentence)
    {
        message.color = color;
        message.text = sentence;

        yield return new WaitForSeconds(1f);
        message.text = "";
    }


}




