using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sizeChange : MonoBehaviour
{
//    public Text text;
    private float retio;
    public float adjust;

 //   public float x = 0;
 //   public float y = 0;
    public float aminx = 0.2633514f;
    public float aminy = 0.25f;
    public float amaxx = 0.7366487f;
    public float amaxy = 0.75f;
 //   public float apivotx = 0;
 //   public float apivoty = 0;

//    public float x = 0;
//    public float y = 0;
    public float portAminx = 0;
    public float portAminy = 0.25f;
    public float portAmaxx = 1f;
    public float portAmaxy = 0.75f;
//    public float portApivotx = 0;
//    public float portApivoty = 0;
    // Start is called before the first frame update
    void Start()
    {
        retio = (float)Screen.height / Screen.width;

//        Screen.autorotateToPortrait = true;
//        Screen.autorotateToPortraitUpsideDown = true;
//        Screen.autorotateToLandscapeLeft = true;
//        Screen.autorotateToLandscapeRight = true;
//        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    // Update is called once per frame
    void Update()
    {
        checkOriantion();
    }

    private void setLand()
    {
        transform.GetComponent<RectTransform>().anchorMin = new Vector2(aminx, aminy);
        transform.GetComponent<RectTransform>().anchorMax = new Vector2(amaxx, amaxy);
//        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
//        transform.GetComponent<RectTransform>().pivot = new Vector2(apivotx, apivoty);
    }

    private void setPort()
    {

        transform.GetComponent<RectTransform>().anchorMin = new Vector2(portAminx, portAminy);
        transform.GetComponent<RectTransform>().anchorMax = new Vector2(portAmaxx, portAmaxy);
//        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
//        transform.GetComponent<RectTransform>().pivot = new Vector2(portApivotx, portApivoty);
    }

    private void checkOriantion() {
        if (CheckOrientation.land) {
            setLand();
        }
        else
        {
            setPort();
        }
    }

}
