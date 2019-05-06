using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class zoom : MonoBehaviour
{
    GraphicRaycaster raycaster;
    private string[] indicators = { "hospital", "toilet", "bank", "monument", "police", "recycle", "education", "electricity", "roadinfra", "water", "communication", "park", "sewage" };
    public GameObject zoomObj;
    public GameObject name_obj;
    public GameObject price_obj;
    public GameObject image_obj;
    public gamelogic gl;
    void Awake()
    {
        // Get both of the components we need to do this
        this.raycaster = GetComponent<GraphicRaycaster>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            /*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }*/

            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);
            Debug.Log("Hit " + results[0].gameObject.name);
            string zoom_obj = results[0].gameObject.name;
            int i = 0;
            for (i = 0; i < 13; i++)
            {
                Text ind = this.gameObject.transform.GetChild(i).GetChild(4).gameObject.GetComponent(typeof(Text)) as Text;
                if (string.Compare(zoom_obj, ind.text) == 0)
                {
                    zoomObj.SetActive(true);

                    name_obj = zoomObj.transform.GetChild(0).GetChild(4).gameObject;
                    Text name = name_obj.GetComponent(typeof(Text)) as Text;
                    name.text = zoom_obj;

                    price_obj = zoomObj.transform.GetChild(0).GetChild(6).gameObject;
                    Text price = price_obj.GetComponent(typeof(Text)) as Text;
                    Text price_upd = this.gameObject.transform.GetChild(i).GetChild(6).gameObject.GetComponent(typeof(Text)) as Text;
                    price.text = price_upd.text;

                    image_obj = zoomObj.transform.GetChild(0).GetChild(2).gameObject;
                    Image img = image_obj.GetComponent<Image>();
                    Image m_img = this.gameObject.transform.GetChild(i).GetChild(2).gameObject.GetComponent<Image>();
                    Sprite m_sprite = m_img.sprite;
                    img.sprite = m_sprite;
                    Debug.Log("SPRITE"+m_sprite);
                    break;
                }
            }
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            /*foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }*/
        }
    }

   
}
