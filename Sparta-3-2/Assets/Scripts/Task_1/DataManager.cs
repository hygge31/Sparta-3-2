using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string userId;
    public UserData user;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    //
 
    public void DataSave(string id,string name,string passward)
    {
        UserData userData = new UserData();
        userData.SetInit(id,name,passward);
        string json = JsonUtility.ToJson(userData);
        Debug.Log(json);
        PlayerPrefs.SetString(userData.userID, json);
    }


    public void DataSave(UserData userData,string userId)
    {
        string json = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(userId, json);
    }

    public bool CheckPassward(UserData user,string passward)
    {
        if(user.passward == passward)
        {
            this.user = user;
            return true;
        }
        else
        {
            return false;
        }
    }

}
