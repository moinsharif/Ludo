using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnInvite : MonoBehaviour
{
    public Sprite invited;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeImage() {
        this.GetComponent<Image>().sprite = invited;
    }

}
