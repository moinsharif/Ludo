using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAuto : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        check();
    }

    private void check()
    {
        text.text = Input.deviceOrientation + "\n" + Screen.orientation + "\n" + Input.deviceOrientation;
    }
}
