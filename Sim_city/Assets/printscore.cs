using UnityEngine;
using UnityEngine.UI;

public class printscore : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoretext;
    public gamelogic gamelogic;
    void Start()
    {
        scoretext.text = gamelogic.getscore().ToString();
    }
}
