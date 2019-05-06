using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cardviz : MonoBehaviour
{
    public Text title;
    public Text value;

    public card card;

    private void Start()
    {
        loadCard(card);
    }

    public void loadCard(card c)
    {
        if (c == null) return;
        card = c;
        title.text = c.cardname;
        //value.text = c.cardvalue.ToString();
        value.text = FindObjectOfType<gamelogic>().value(title).ToString("0");
    }

    private void Update()
    {
        value.text = FindObjectOfType<gamelogic>().value(title).ToString("0");
    }
}

