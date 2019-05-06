using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zoomObj;
    public void switch_back()
    {
        zoomObj.SetActive(false);
    }
}
