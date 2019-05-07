using UnityEngine;
using UnityEngine.UI;

public class printscore : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoretext;

    private void Update()
    {
        scoretext.text = FindObjectOfType<gamelogic>().getscore().ToString();
    }
}
