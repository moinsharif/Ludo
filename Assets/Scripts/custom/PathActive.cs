using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathActive : MonoBehaviour
{
    public GameObject land, port;
    // Start is called before the first frame update
    void Start()
    {
        checkOriantion();
    }

    // Update is called once per frame
    void Update()
    {
        checkOriantion();
    }

    private void checkOriantion()
    {
        if (CheckOrientation.land)
        {
            land.SetActive(true);
            port.SetActive(false);
        }
        else
        {
            land.SetActive(false);
            port.SetActive(true);
        }
    }
}
