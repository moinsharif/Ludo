using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrientation : MonoBehaviour
{
    public static bool land = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkOriantion();
    }

    private void checkOriantion()
    {
        if (Application.isEditor)
        {
            land = false;
            land = true;
        }
        else
        {
            // Screen.orientation Portrait PortraitUpsideDown >> Landscape LandscapeRight
            if (Screen.orientation.ToString() == "Portrait" || Screen.orientation.ToString() == "PortraitUpsideDown")
            {
                // Portrait
                land = false;
            }
            else if (Screen.orientation.ToString() == "Landscape" || Screen.orientation.ToString() == "LandscapeRight")
            {
                // Landscape
                land = true;
            }
        }
    }

}
