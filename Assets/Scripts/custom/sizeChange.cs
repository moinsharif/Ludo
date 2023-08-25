using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeChange : MonoBehaviour
{
    private float retio;

    public float x = 0;
    public float y = 0;

    public float aminx = 0;
    public float aminy = 0;

    public float amaxx = 0;
    public float amaxy = 0;

    public float apivotx = 0;
    public float apivoty = 0;
    // Start is called before the first frame update
    void Start()
    {
        retio = (float)Screen.height / Screen.width;
        //setSize(this.GetComponent<RectTransform>(), this.transform.parent.GetComponent<RectTransform>());
        //this.GetComponent<RectTransform>().anchorMin = new Vector2(0.242f, 0.1666111f);
        //this.GetComponent<RectTransform>().anchorMax = new Vector2(0.7577813f, 0.8335139f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setSize(RectTransform rectTransform1, RectTransform rectTransform2)
    {
        rectTransform1.anchoredPosition = new Vector2(x, y);
        rectTransform1.anchorMin = new Vector2(aminx, aminy);
        rectTransform1.anchorMax = new Vector2(amaxx, amaxy);
        rectTransform1.pivot = new Vector2(apivotx, apivoty);
    }
}
