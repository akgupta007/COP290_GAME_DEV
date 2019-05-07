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
    static public float[] devCost = { 1100000, 100000, 10000000, 500000, 900000, 600000, 500000, 650000, 1800000, 700000, 500000, 200000, 1100000 };
    static private float[] capacity = { 15000, 5000, 100000, 25000, 25000, 12000, 15000, 22000, 18000, 15000, 10000, 12000, 18000 };
    static private int[] counts = { 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //static private float[] net_investment = { 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static private int[] level = { 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1 };
    static public float[] level_cost = { 1100000, 100000, 10000000, 500000, 900000, 600000, 500000, 650000, 1800000, 700000, 500000, 200000, 1100000 };
    static public float[] maintainance = {  110000, 10000,1000000,50000, 90000, 50000, 50000, 65000, 180000, 70000, 50000, 20000, 110000 };
    //static private int[] employment_inc = { 5000, 1000, 1000, 3000, 5000, 12000, 5000, 3000, 6000, 1000, 4000, 2000, 3000 };
    //static private int[] employment_count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //static private int private_business = 30000;
    //static private float[] salary = { 40000, 7000, 20000, 20000, 25000, 32000, 20000, 52000, 15000, 12000, 15000, 10000, 12000 };
    static private float total_population = 80000;
    static private float economy = 0;
    static private float HDI = 0;
    static private float health = 0;
    static private float pollution = 0;
    static private float debt = 0;
    //static private float birth_rate = 0.7f;
    //static private float death_rate = 0.5f;
    //static private float residences = 0;
    static private float average_income = 0;
    static private float tax_collection = 0;
    static private float employed_population = 30000;
    static private float net_maintainance = 0;
    static private float deficit = 0;
    static private float bankrate = 16;
    static private float borrowsum = 1000000;
    static private float repaysum = 500000;
    static private float year = 0;
    static private float taxrate = 15;
    static private string message = "hello";
    static private float loanlimit = 0;
    static private float old_population = 80000;

    static private float[] ecweight = {0.5f, 0.25f, 2, 2, 1, 0.75f, 3, 2, 2.5f, 1.5f, 1, 0, 0};
    static private float[] healthweight = { 5, 3, 0, 0, 0, 0, 1, 0.5f, 0, 0.5f, 2, 2, 0.5f };
    static private float[] pollweight = { 1, 8, 0, 0, 0, 5, 2, 0.5f, 0, 0, 1, 1, 5 };

    public void updatepopulation()
    {
        float br = 0.3f;
        float imr = 3f / taxrate;
        float hcap = capacity[0] * counts[0];
        hcap *= 100 / total_population;
        if (hcap > 1) hcap = 1;
        float ecap = capacity[6] * counts[6];
        ecap *= 100 / total_population;
        if (ecap > 1) ecap = 1;
        br *= Mathf.Exp(-hcap) + Mathf.Exp(-ecap);
        total_population += (br + imr) * total_population;
    }

    public float caleconomy()
    {
        float f = 0;
        float x = 0;
        float h=0;
        float ih=0;
        float p=0;
        float ip=0;
        for(int t = 0; t < 13; t++)
        {
            x = capacity[t] * counts[t];
            if (x > total_population)
            {
                f += total_population * ecweight[t];
                h += total_population * healthweight[t];
                p += total_population * pollweight[t];
            }
            else
            {
                f += x * ecweight[t];
                h += x * healthweight[t];
                p += x * pollweight[t];
            }
            ih += total_population * healthweight[t];
            ip += total_population * pollweight[t];
        }
        f /= taxrate;
        f /= 20;
        health = h / ih;
        pollution = p / ip;
        pollution *= 100;
        pollution = 100 - pollution;
        return f * old_population;
    }

    public void calculateHDI()
    {
        float lit = 0;
        lit += capacity[6] * counts[6] / total_population;
        if (lit > 1) lit = 1;
        average_income = economy / total_population;
        float ec = Mathf.Log(average_income) - Mathf.Log(100);
        ec /= Mathf.Log(80000) - Mathf.Log(100);

        HDI = Mathf.Pow(lit * health * ec, 0.333f);

    }

    public void calculatescore()
    {
        score = economy;
        score *= HDI;
        score *= (100 - pollution) / 100;

        score *= Mathf.Exp(-0.0125f * debt);
    }

    public void loan()
    {
        if (deficit + borrowsum > loanlimit)
        {
            message = "you have exceeded your loanlimit";
            return;
        }
        deficit += borrowsum;
        money += borrowsum;
        message = borrowsum.ToString() + " money borrowed from the bank.";
       // bankrate += 0.1f;
       // borrowsum += 100000;
    }

    public void repay()
    {
        if (deficit < repaysum)
        {
            message = "don't have that much loan";
            return;
        }
        else if (money < repaysum)
        {
            message = "don't have money to repay";
            return;
        }
        deficit -= repaysum;
        money -= repaysum;
        message = repaysum.ToString() + " returned to the bank";
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
        return deficit/1000;
    }

    public string loantext()
    {
        return "+ $" + borrowsum.ToString("0");
    }

    public string repaytext()
    {
        return "- $" + repaysum.ToString("0");
    }

    private void Start()
    {
        score = 0;
        money = 10000000;
        loanlimit = money/2;
        message = "game has started. Hint: Try not to ignore essential things in year 1.";
        for (int i = 0; i < 13; i++)
        {
            FindObjectOfType<countrydata>().counting(i, Mathf.Ceil((total_population / capacity[i]) - counts[i]) , 2);
        }
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
        return money/1000;
    }

    public float getEco()
    {
        return economy/1000000;
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
        return score/1000;
    }

    public float getCount(int pos)
    {
        return counts[pos] ;
    }


    public void level_update(Text text)
    {
        int i = 0;
        Debug.Log(text.text);
        for (i = 0; i < 13; i++)
        {
            if (string.Compare(text.text, indicators[i]) == 0)
            {
                break;
            }
        }
        float totalcost = 0;

        totalcost = maintainance[i] * counts[i];
        totalcost += level_cost[i];
       
        if (totalcost < money)
        {
            money -= totalcost;
            level[i] += 1;
            FindObjectOfType<countrydata>().counting(i, level[i], 1);
            message = text.text + " level updated to " + level[i];
            capacity[i] += 0.1f * capacity[i];
            level_cost[i] += 0.2f * level_cost[i];
            devCost[i] += 0.2f * devCost[i];
            maintainance[i] += 0.2f * maintainance[i];
            FindObjectOfType<countrydata>().counting(i, Mathf.Ceil((total_population / capacity[i]) - counts[i]), 2);
        }
        else
        {
            message = "You don't have enough money for lavel update";
        }
        //salary_update
        //employment_update
        //capacity_increment
        //maintainance update small change
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
        if (money < change)
        {
            message = "you have no money. borrow for bank or increment year with your change in tax rate.";
            return;
        }
        //devCost[i] += 100000;
        
        money -= change;
        counts[i] += 1;

        FindObjectOfType<countrydata>().counting(i,counts[i], 0);
        FindObjectOfType<countrydata>().counting(i, Mathf.Ceil((total_population / capacity[i]) - counts[i]), 2);
        //FindObjectOfType<countrydata>().counting(i, change / 100000);
        if (i != 3) message = "money spent on " + indicators[i];
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

    public float getMaintain(int pos)
    {
        return maintainance[pos];
    }

    public float getLevelCost(int pos)
    {
        return level_cost[pos];
    }

    public float getPurchase(int pos)
    {
        return devCost[pos];
    }

    public void yearchange(Text t)
    {
        year += 1;

        old_population = total_population;

        //update population

        updatepopulation();

        for (int i = 0; i < 13; i++)
        {
            devCost[i] += 0.2f * devCost[i];
            level_cost[i] += 0.2f * level_cost[i];
            maintainance[i] += 0.2f * maintainance[i];
            capacity[i] -= 0.1f * capacity[i];
        }

        economy = caleconomy();

        //death_rate = 0.3f + 0.2f * Mathf.Exp(-(counts[0] * 0.2f) - (counts[4] * 0.09f));
        //birth_rate = 0.3f + 0.4f * Mathf.Exp(-(counts[6] * 0.2f));
        //total_population += ((float)(birth_rate - death_rate)) * total_population;

        //calculate economy,health,pollution


        calculateHDI();


        net_maintainance = 0;
        for (int k = 0; k < 13; k++)
        {
            net_maintainance += counts[k] * maintainance[k];
        }

        //recover tax collection
        tax_collection = economy*taxrate/100;
        money += tax_collection;
        money -= net_maintainance;
        loanlimit = money/2;

        //update deficit and debt
        deficit += bankrate * deficit / 100;
        debt = deficit * 100 / loanlimit;

        calculatescore();


        //update tax rate
        float incr = float.Parse(t.text);
        taxrate += incr;

        if (taxrate > 25) taxrate = 25;
        else if (taxrate < 10) taxrate = 10;

        //bank rate update
        int x = Random.Range(-2, 3);
        bankrate += x;
        if (bankrate < 12) bankrate = 12;
        else if (bankrate > 20) bankrate = 20;


        for (int i = 0; i < 13; i++)
        {
            FindObjectOfType<countrydata>().counting(i, Mathf.Ceil((total_population / capacity[i]) - counts[i]), 2);
        }

        if (year > 6)
        {
            message = "game has finished";
            int temp = FindObjectOfType<Canvas>().transform.GetChildCount();
            FindObjectOfType<Canvas>().transform.GetChild(temp - 1).gameObject.SetActive(true);
            return;
        }
        message = "year changed to " + year + ". price of everything has increased according to your previous choices.";
    }

}