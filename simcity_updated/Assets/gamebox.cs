﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamebox : MonoBehaviour
{
    // Start is called before the first frame update
    public Text t;

    // Update is called once per frame
    void Update()
    {
        t.text = FindObjectOfType<gamelogic>().givemessage();
    }
}
