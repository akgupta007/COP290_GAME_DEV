using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taxupdate : MonoBehaviour
{
    // Start is called before the first frame update
    public Text t;
    private float f;

    public void add()
    {
        f = float.Parse(t.text);
        f += 1;
        t.text = f.ToString();
    }
    public void sub()
    {
        f = float.Parse(t.text);
        f -= 1;
        t.text = f.ToString();
    }
}
