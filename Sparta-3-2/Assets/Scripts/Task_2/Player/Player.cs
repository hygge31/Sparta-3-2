using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int playerLevel = 1;
    public int attackDamage = 40;
    public int attackDamageIncrease;
    public int shield = 25;
    public int shieldIncrease;
    public int health = 70;
    public int healthIncrease;
    public int critcal = 25;
    public int critcalIncrease;
    public int money = 10000;

    public int currentEXP = 0;
    public int maxEXP;

}
