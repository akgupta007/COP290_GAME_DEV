using UnityEngine;
using UnityEngine.UI;

public class printPopulation : MonoBehaviour
{
    // Start is called before the first frame update
    public Text moneytext;

    private void Update()
    {
        moneytext.text = FindObjectOfType<gamelogic>().getpop().ToString();
    }
}
