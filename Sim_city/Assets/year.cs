using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class year : MonoBehaviour
{
    // Start is called before the first frame update
    public Text t;

    private void Update()
    {
        t.text = FindObjectOfType<gamelogic>().getyear().ToString();
    }
}
