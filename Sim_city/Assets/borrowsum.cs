using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class borrowsum : MonoBehaviour
{
    public Text t;
    // Update is called once per frame
    void Update()
    {
        t.text = FindObjectOfType<gamelogic>().loantext();
    }
}
