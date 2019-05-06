using UnityEngine;
using UnityEngine.UI;

public class printmoney : MonoBehaviour
{
    // Start is called before the first frame update
    public Text moneytext;

    private void Update()
    {
        moneytext.text = FindObjectOfType<gamelogic>().getmoney().ToString();
    }
}
