
using UnityEngine;
using UnityEngine.UI;

public class taxrate : MonoBehaviour
{
    public Text t;
    // Update is called once per frame
    void Update()
    {
        t.text = FindObjectOfType<gamelogic>().taxrateupdate().ToString();
    }
}
