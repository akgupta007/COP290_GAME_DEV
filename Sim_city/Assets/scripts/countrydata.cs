﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countrydata : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject grandchild;

    public void counting(int i, float k, int c)
    {
        Debug.Log(i);
        grandchild = this.gameObject.transform.GetChild(i).GetChild(c).gameObject;
        Text publiccount = grandchild.GetComponent(typeof(Text)) as Text;
       // float f = float.Parse(publiccount.text);
        //f += k;
        publiccount.text = k.ToString();
    }

    public void counting(int i, int k, int c)
    {
        Debug.Log(i);
        grandchild = this.gameObject.transform.GetChild(i).GetChild(c).gameObject;
        Text publiccount = grandchild.GetComponent(typeof(Text)) as Text;
        publiccount.text = k.ToString();
    }
}
