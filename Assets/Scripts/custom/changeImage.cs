using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImage : MonoBehaviour
{
    Image m_Image;
    public Sprite m_Sprite;
    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Image.sprite = m_Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
