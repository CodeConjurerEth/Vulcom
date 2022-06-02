using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IstoricTxt : MonoBehaviour
{
    private TMP_Text _text;

    private void SetTxt(string text) {
        _text.text = text;
    }
}