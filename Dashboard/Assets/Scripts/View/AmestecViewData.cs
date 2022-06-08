using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;
using UnityEngine.UI;

public class AmestecViewData : MonoBehaviour
{

    [Header("ONLY ADD THIS TO PREFAB AMESTECVIEWDATA")] 
    [SerializeField] private GameObject AmestecViewSliderPrefab;
    [SerializeField] private GameObject Tmp_Text_Prefab;
    
    private Amestec _amestec;
    
    private TMP_Text _amestecNameText;
    private TMP_Text _lotText;
    private TMP_Text _duritateText;
    private TMP_Text _dataAchizitie;
    private TMP_Text _grameText;
    private TMP_Text _culoareText;
    private TMP_Text _presaProfilText;
    
    // private TMP_Text _cantitateInitiala;
    private TMP_Text _dataExpirare;

    // private int maxStringLength = 9; //

    public Amestec GetAmestec() { return _amestec; }

    private void Start() 
    {
        AssignChildTextToPrivateFields();
    }

    public void SetValuesInDataView(Amestec amestec)
    {
        _amestec = amestec;
        
        _amestecNameText.text = amestec.Name;
        _lotText.text ="Lot: " + amestec.Lot;
        
        var grameString = amestec.Grame.ToString("n2") + " g";
        //check if string is too long
        // if (grameString.Length > maxStringLength)   
        //     grameString = grameString.Substring(0, maxStringLength);
        _grameText.text = "Cantitate curenta: " + grameString;

        _duritateText.text = "Duritate: " + amestec.Duritate.ToString() + " ShA";
        _culoareText.text = "Culoare: " + amestec.Culoare;
        _presaProfilText.text = "Presa/Profil: " + amestec.PresaProfil;
        _dataAchizitie.text = "DataAchizitie: " + amestec.DataAchizitie;
        _dataExpirare.text = "DataExpirare: " + amestec.DataExpirare;
    }

    public void ResetFieldsNull()
    {
        _amestec = null;

        _amestecNameText.text = "Nume";
        _lotText.text = "Lot";
        _grameText.text = "Cantitate curenta";
        _duritateText.text = "Duritate";
        _culoareText.text = "Culoare";
        _presaProfilText.text = "Presa/Profil";
        _dataAchizitie.text = "DataAchizitie";
        _dataExpirare.text = "DataExpirare";
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
        if (!verticalLayoutGroupTransform.GetChild(3).TryGetComponent(out _duritateText)) {
            throw new Exception("Cannot find Duritate GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(4).TryGetComponent(out _culoareText)) {
            throw new Exception("Cannot find Culoare GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(5).TryGetComponent(out _presaProfilText)) {
            throw new Exception("Cannot find Presa/Profil GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(6).TryGetComponent(out _dataAchizitie)) {
            throw new Exception("Cannot find DataAchizitie GameObject or TMP_Text Component");
        }
        if (!verticalLayoutGroupTransform.GetChild(7).TryGetComponent(out _dataExpirare)) {
            throw new Exception("Cannot find DataExpirare GameObject or TMP_Text Component");
        }
    }
}