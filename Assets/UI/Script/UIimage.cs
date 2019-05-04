using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIimage : MonoBehaviour
{
    public Slider m_Slider;
    public CanvasGroup m_image;
    public CanvasGroup m_images;

    public float value = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Slider.value >= 0.25f && m_Slider.value <= 0.7f)
            m_image.alpha = m_Slider.value * 2;
        else
            m_image.alpha = 0;
        if (m_Slider.value > 0.70f)
            m_images.alpha = (m_Slider.value - 0.5f) * 2;
        else
            m_images.alpha = 0;

        value = Mathf.Clamp(value, 0, 1);
    }
}
