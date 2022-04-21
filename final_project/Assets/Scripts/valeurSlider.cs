using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class valeurSlider : MonoBehaviour
{
    public GameObject slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText()
    {
        this.GetComponent<TMP_InputField>().text = slider.GetComponent<Slider>().value.ToString();
    }

    public void setSlider()
    {
        slider.GetComponent<Slider>().value = (float)Convert.ToDouble(this.GetComponent<TMP_InputField>().text);
    }
}
