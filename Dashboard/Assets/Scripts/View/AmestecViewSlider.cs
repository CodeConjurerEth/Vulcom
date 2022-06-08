
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmestecViewSlider : MonoBehaviour
{
    [SerializeField] private Image _sliderMaskFill;
    [SerializeField] private TMP_Text barText;
    public void SetFillAmountWithText(float cantitate, double cantitateInitiala) {
        float fillAmount = Mathf.Clamp(cantitate,0f, (float)cantitateInitiala)
                               / (float)cantitateInitiala;
        
        _sliderMaskFill.fillAmount = fillAmount;
        barText.text = cantitate.ToString("n2") + " g / " + cantitateInitiala.ToString("n2") + " g"  ;
    }

    public void RefreshSliderView()
    {
        _sliderMaskFill.fillAmount = 1;
        barText.text = "";
    }
}
