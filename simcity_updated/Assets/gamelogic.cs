using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamelogic : MonoBehaviour
{
    // Start is called before the first frame update
    
    static private float score;
    static private float money;

    static private string[] indicators = { "hospital", "toilet", "monument","bank","police", "recycle", "education", "electricity", "roadinfra", "water", "communication", "park", "sewage" };
    static private float[] devCost = { 1100000, 500000, 1600000, 500000, 900000, 600000, 500000, 650000, 1800000, 700000, 500000, 200000, 1100000 };
    static private float[] capacity = { 15000, 5000, 100000, 0, 25000, 12000, 15000, 22000, 18000, 15000, 10000, 12000, 18000 };
    static private int[] counts = { 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static private float[] net_investment = { 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static private int[] level = { 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1 };
    static private float[] maintainance = {  110000, 50000,160000,0, 90000, 50000, 50000, 65000, 180000, 70000, 50000, 20000, 110000 };
    static private int[] employment_inc = { 5000, 1000, 1000, 0, 5000, 12000, 5000, 3000, 6000, 1000, 4000, 2000, 3000 };
    static private int[] employment_count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static private int private_business = 30000;
    static private float[] salary = { 40000, 7000, 20000, 20000, 25000, 32000, 20000, 52000, 15000, 12000, 15000, 10000, 12000 };
    static private float total_population = 80000;
    static private float economy = 0;
    static private float HDI = 0;
    static private float pollution = 0;
    static private float debt = 0;
    static private float birth_rate = 0.7f;
    static private float death_rate = 0.5f;
    static private float residences = 0;
    static private float average_income = 0;
    static private float tax_collection = 0;
    static private float employed_population = 30000;

    static private float deficit = 0;
    static private float bankrate = 15;
    static private float borrowsum = 1000000;
    static private float year = 0;
    static private float taxrate = 15;
    static private string message = "hello";

    public void loan()
    {
        deficit += borrowsum;
        money += borrowsum;
        message = borrowsum.ToString() + " money borrowed from the bank.";
        bankrate += 0.1f;
        borrowsum += 100000;
    }

    public float taxrateupdate()
    {
        return taxrate;
    }

    public float bankrateupdate()
    {
        return bankrate;
    }

    public float deficitupdate()
    {
        return deficit;
    }

    public string loantext()
    {
        return "+ $" + borrowsum.ToString("0");
    }

    private void Start()
    {
        score = 100;
        money = 7000000;
        message = "game has started. Hint: Try not to ignore essential things in year 1.";
    }

    public float value(Text text)
    {
        int i = 0;
        for (i = 0; i < 13; i++)
        {
            if (string.Compare(text.text, indicators[i]) == 0)
            {
                break;
            }
        }
        return devCost[i];
    }

    public float getmoney()
    {
        return money;
    }

    public float getEco()
    {
        return economy;
    }

    public float getHDI()
    {
        return HDI;
    }

    public float getPollution()
    {
        return pollution;
    }

    public float getDebt()
    {
        return debt;
    }


    public float getpop()
    {
        return total_population;
    }

    public float getscore()
    {
        return score;
    }


    public void level_update(Text text)
    {
        int i = 0;
        Debug.Log(text.text);
        for (i = 0; i < 12; i++)
        {
            if (string.Compare(text.text, indicators[i]) == 0)
            {
                break;
            }
        }
        //salary_update
        //employment_update
        //capacity_increment
        //maintainance update small change
        //level increment
    }

    public void buy(Text text)
    {
        int i = 0;
        Debug.Log(text.text);
        for( i  = 0; i<12; i++)
        {
            if (string.Compare(text.text, indicators[i]) == 0)
            {
                break;
            }
        }
        float change = devCost[i];
        if (money < change)
        {
            message = "you have no money. borrow for bank or increment year with your chnage in tax rate.";
            return;
        }
        //devCost[i] += 100000;
        
        money -= change;
        counts[i] += 1;
        employed_population += employment_inc[i];
        employment_count[i] += employment_inc[i];
        if (i == 3) deficit -= devCost[i];
        FindObjectOfType<countrydata>().counting(i,counts[i]);
        //FindObjectOfType<countrydata>().counting(i, change / 100000);
        if (i != 3) message = "money spent on " + indicators[i];
        else message = "money returned to the bank";
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }
    public float getyear()
    {
        return year;
    }

    public string givemessage()
    {
        return message;
    }

    public void yearchange(Text t)
    {
        float incr = float.Parse(t.text);
        //salary increment
        //pollution
        //private_business update
        //hdi calculation
        //
        death_rate = 0.3f + 0.2f * Mathf.Exp(-(counts[0] * 0.2f) - (counts[4] * 0.09f));
        birth_rate = 0.3f + 0.4f * Mathf.Exp(-(counts[6] * 0.2f));
        total_population += ((float) (birth_rate - death_rate)) * total_population;
        tax_collection = 0;
        for (int i = 0; i<13; i++)
        {
            tax_collection += (float)0.00002 * taxrate * employment_count[i] * salary[i];
        }
        money += tax_collection;
        taxrate += incr;
        year += 1;
        if (year > 9)
        {
            message = "game has finished";
            int temp = FindObjectOfType<Canvas>().transform.GetChildCount();
            FindObjectOfType<Canvas>().transform.GetChild(temp - 1).gameObject.SetActive(true);
            return;
        }
        message = "year changed to " + year + ". price of everything has increased/decreased according to your previous choices.";
    }

}