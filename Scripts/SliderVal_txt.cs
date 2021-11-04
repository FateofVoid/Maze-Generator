using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//updates the text object attached to slider.
public class SliderVal_txt : MonoBehaviour
{
    Text sliderValueText;
    // Start is called before the first frame update
    void Start()
    {
        sliderValueText = GetComponent<Text>();
    }

    // Update is called once per frame
    public void TextUpdate(float value)
    {
        sliderValueText.text = value.ToString();
    }
}
