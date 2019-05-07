using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class repaysum : MonoBehaviour
{
    // Start is called before the first frame update
    public Text t;
    private void Update()
    {
        t.text = FindObjectOfType<gamelogic>().repaytext();
    }
}
