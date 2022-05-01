using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;
using UnityEngine.UI;

public class AmestecViewData : MonoBehaviour 
{
    
    [Header("ONLY ADD THIS TO PREFAB AMESTECVIEWDATA")]
    private Amestec _amestec;
    
    private TMP_Text _amestecNameText;
    private TMP_Text _lotText;
    private TMP_Text _dataAchizitie;
    private TMP_Text _grameText;
    private TMP_Text _culoareText;
    private TMP_Text _presaProfilText;
    
    private TMP_Text _cantitateInitiala;
    private TMP_Text _dataExpirare;

    private int maxStringLength = 9; //

    public Amestec GetAmestec() { return _amestec; }

    private void Start() 
    {
        AssignChildTextToPrivateFields();
    }

    public void SetAmestecValuesInView(Amestec amestec)
    {
        _amestec = amestec;
        
        _amestecNameText.text = amestec.Name;
        _lotText.text = amestec.Lot;
        
        var grameString = amestec.Grame.ToString() + " g";
        if (grameString.Length > maxStringLength)
            grameString = grameString.Substring(0, maxStringLength);
        _grameText.text = grameString;

        _culoareText.text = amestec.Culoare;
        _presaProfilText.text = amestec.PresaProfil;
        _dataAchizitie.text = amestec.DataAchizitie;
        _dataExpirare.text = amestec.DataExpirare;
    }

    private void AssignChildTextToPrivateFields()
    {
        if (!transform.GetChild(0).TryGetComponent(out VerticalLayoutGroup verticalLayoutGroup)) {
            throw new Exception("Cannot find HorizontalLayoutGroup GameObject or HorizontalLayoutGroup Component");
        }
        var verticalLayoutGroupTransform = verticalLayoutGroup.transform;
        if (!verticalLayoutGroupTransform.GetChild(0).TryGetComponent(out _amestecNameText)) {
            throw new Exception("Cannot find NumeAmestec GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(1).TryGetComponent(out _lotText)) {
            throw new Exception("Cannot find LotText GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(2).TryGetComponent(out _grameText)) {
            throw new Exception("Cannot find Cantitate(g)_Curenta GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(3).TryGetComponent(out _culoareText)) {
            throw new Exception("Cannot find Culoare GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(4).TryGetComponent(out _presaProfilText)) {
            throw new Exception("Cannot find Presa/Profil GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(5).TryGetComponent(out _dataAchizitie)) {
            throw new Exception("Cannot find DataAchizitie GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(6).TryGetComponent(out _dataExpirare)) {
            throw new Exception("Cannot find DataExpirare GameObject or TMP_Text Component");
        }
    }
}