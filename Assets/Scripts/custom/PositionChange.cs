using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChange : MonoBehaviour
{
    public Vector3 landPosition;
    public Vector3 portPosition;
    // Start is called before the first frame update
    void Start()
    {
        positionChange();
    }

    // Update is called once per frame
    void Update()
    {
        positionChange();
    }

    private void positionChange()
    {
        if (CheckOrientation.land)
        {
            transform.position = landPosition;
        }
        else
        {
            transform.position = portPosition;
        }
    }
}
