using UnityEngine;
using UnityEngine.UI;

public class printHDI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text moneytext;

    private void Update()
    {
        moneytext.text = FindObjectOfType<gamelogic>().getHDI().ToString("F3");
    }
}
