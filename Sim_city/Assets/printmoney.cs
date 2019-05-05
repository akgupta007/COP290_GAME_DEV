using UnityEngine;
using UnityEngine.UI;

public class printmoney : MonoBehaviour
{
    // Start is called before the first frame update
    public Text moneytext;
    public gamelogic gamelogic;
    void Start()
    {
        moneytext.text = gamelogic.getmoney().ToString();

    }
    private void Update()
    {
        moneytext.text = gamelogic.getmoney().ToString();

    }
    public void print(float money)
    {
        moneytext.text = money.ToString();
        
    }
}
