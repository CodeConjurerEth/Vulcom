
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmestecViewSlider : MonoBehaviour
{
    private TMP_Text _text;
    private Image _sliderImage;

    private void Awake()
    {
        AssignGameObjects();
        
    }

    private void Start()
    {
        AssignGameObjects();
    }

    private void OnEnable()
    {
        AssignGameObjects();
    }

    public void SetValues(double textValue, float fillAmount)
    {
        _text.SetText(textValue.ToString() + "g");
        _sliderImage.fillAmount = fillAmount;
    }

    private void AssignGameObjects()
    {
        if (!transform.GetChild(0).TryGetComponent(out _text)) {
            throw new Exception("Cannot Find AmestecViewBara child TMP_Text GameObject or Text Component");
        }
        if (!transform.GetChild(1).GetChild(0).TryGetComponent(out _sliderImage)) {
            throw new Exception("Cannot find Mask child of Slider or Image Component");
        }
    }
}
