using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamelogic : MonoBehaviour
{
    // Start is called before the first frame update
    
    static private float score;
    static private float money;

    static private string[] indicators = { "hospital", "toilet", "bank", "monument", "police", "recycle", "education", "electricity", "roadinfra", "water", "communication", "park", "sewage" };
    static private float[] devCost = { 1100000, 500000, -1000000, 1600000, 900000, 500000, 500000, 650000, 1800000, 700000, 500000, 200000, 1100000 };
    static private int[] counts = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static private float[] net_investment = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static private int[] level = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    static private float[] maintainance = {  11000, 5000, 0, 16000, 9000, 5000, 5000, 6500, 18000, 7000, 5000, 2000, 11000 };
    static private int[] employment_count = { 50, 1, 30, 10, 25, 12, 40, 45, 30, 15, 10, 2, 35 };
    static private float total_population = 4;
    static private float residences = 0;
    static private float average_income = 0;
    static private float tax_collection = 0;
    static private int employed_population = 0;

    private void Start()
    {
        score = 0;
        money = 10000000;
       FindObjectOfType<printmoney>().print(money);
    }

    public float getmoney()
    {
        return money;
    }

    public float getscore()
    {
        return score;
    }

    public void buy(Text text)
    {
        int i = 0;
        Debug.Log(text.text);
        for( i  = 0; i<13; i++)
        {
            if (string.Compare(text.text, indicators[i]) == 0)
            {
                break;
            }
        }
        float change = devCost[i];
        counts[i] += 1;
        employed_population += employment_count[i];
        money -= change;
        FindObjectOfType<printmoney>().print(money);
    }
    
}
