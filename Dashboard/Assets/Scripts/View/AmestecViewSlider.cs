
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmestecViewSlider : MonoBehaviour
{
    [SerializeField] private Image _sliderMaskFill;
    [SerializeField] private TMP_Text procentajText;
    public void SetFillAmountWithPercentage(float fillAmount) {
        //TODO: din % in grame ex: 100g/500g
        _sliderMaskFill.fillAmount = fillAmount;
        procentajText.text = (fillAmount * 100).ToString() + "%";
    }
}
