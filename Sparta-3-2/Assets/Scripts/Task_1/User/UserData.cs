using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string userID;
    public string userName;
    public string passward;
    public int bankAccount = 50000;
    public int myWallet = 100000;
    
    
    public void SetInit(string ID,string name,string passward)
    {
        userID = ID;
        userName = name;
        this.passward = passward;
    }
    
 



}
