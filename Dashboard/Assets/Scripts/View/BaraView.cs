using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;

public class BaraView : MonoBehaviour
{
    private TMP_Text _idText;
    private TMP_Text _baraNameText;
    private TMP_Text _formaText;
    
    private TMP_Text _diametruText;
    private TMP_Text _lungimeBaraText;
    private TMP_Text _laturaSuprafataText;
    private TMP_Text _lungimeSuprafataText;
    private TMP_Text _latimeSuprafataText;
    private TMP_Text _laturaHexagonText;
    private TMP_Text _kgText;
    
    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
    }
    
    public void SetValuesInView(Bara bara)
    {
        var aria = -1d;
        switch (bara.Forma) {
            case (int)Bara.Forme.Cerc:
                _formaText.text = "Cerc";
                aria = Bara.GetAriaCerc(bara.Diametru / 2);
                _diametruText.text = bara.Diametru.ToString();
                
                break;
            case (int)Bara.Forme.Patrat:
                _formaText.text = "Patrat";
                aria = Bara.GetAriaPatrat(bara.LaturaSuprafataPatrat);
                _laturaSuprafataText.text = bara.LaturaSuprafataPatrat.ToString();
                
                break;
            case (int)Bara.Forme.Dreptunghi:
                _formaText.text = "Dreptunghi";
                aria = Bara.GetAriaDreptunghi(bara.LungimeSuprafata, bara.LatimeSuprafata);
                _lungimeSuprafataText.text = bara.LungimeSuprafata.ToString();
                _latimeSuprafataText.text = bara.LatimeSuprafata.ToString();
                
                break;
            case (int)Bara.Forme.Hexagon:
                _formaText.text = "Hexagon";
                aria = Bara.GetAriaHexagon(bara.LaturaHexagon);
                _laturaHexagonText.text = bara.LaturaHexagon.ToString();
                
                break;
            default:
                throw new Exception("Bara " + bara.Name + " does not have a Forma assigned");
        }
        _idText.text = bara.Id.ToString();
        _baraNameText.text = bara.Name;
        if (Math.Abs(aria - (-1d)) > 0.00000000001d) {              // 11 
            var kg = aria * bara.LungimeBara * bara.TipMetal.Densitate;
            _kgText.text =  kg.ToString();
        }
    }
    

    private void AssignChildTextToPrivateFields()
    {
        if (!transform.GetChild(0).TryGetComponent(out _idText)) {
            throw new Exception("Cannot find ID GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(1).TryGetComponent(out _baraNameText)) {
            throw new Exception("Cannot find amestecName GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(2).TryGetComponent(out _formaText)) {
            throw new Exception("Cannot find forma GameObject or TMP_Text Component");
        }
    }
}