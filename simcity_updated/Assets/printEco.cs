using UnityEngine;
using UnityEngine.UI;

public class printEco : MonoBehaviour
{
    // Start is called before the first frame update
    public Text moneytext;

    private void Update()
    {
        moneytext.text = FindObjectOfType<gamelogic>().getEco().ToString();
    }
}
