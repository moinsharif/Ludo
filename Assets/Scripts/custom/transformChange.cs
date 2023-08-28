using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformChange : MonoBehaviour
{
    public Vector3 landScale;
    public Vector3 portScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scaleChange();
    }

    private void scaleChange()
    {
        if (CheckOrientation.land)
        {
            transform.localScale = landScale;
        }
        else
        {
            transform.localScale = portScale;
        }
    }
}
