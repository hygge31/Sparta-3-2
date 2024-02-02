using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public string GetThousandCommaText(int num)
    {
        return string.Format("{0:#,###}", num);
    }


    public int GetThousandCommaText(string num)
    {
        if(int.TryParse(num.Replace(",",""),out int number))
        {
            return number;
        }
        else
        {
            return 0;
        }
    }
    
}
