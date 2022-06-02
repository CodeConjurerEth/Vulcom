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
        
        var grameString = amestec.Grame.ToString() + " g";
        //check if string is too long
        // if (grameString.Length > maxStringLength)   
        //     grameString = grameString.Substring(0, maxStringLength);
        _grameText.text = "Cantitate curenta: " + grameString;

        _duritateText.text = "Duritate: " + amestec.Duritate.ToString() + " ShA";
        _culoareText.text = "Culoare: " + amestec.Culoare;
        _presaProfilText.text ="Presa/Profil: " + amestec.PresaProfil;
        _dataAchizitie.text ="DataAchizitie: " + amestec.DataAchizitie;
        _dataExpirare.text ="DataExpirare: " + amestec.DataExpirare;
    }
    
    public void SetIstoricView(Amestec amestec)
    {
        string istorieCantitateData = amestec.IstorieCantitatiCuData;
        var cantitateData = istorieCantitateData.Split(char.Parse(","));
     
        
        //instantiate prefab and find amestecViewSlider component
        var prefab = Instantiate(AmestecViewSliderPrefab, AmestecController.Instance.GetRightPanel());
        AmestecViewSlider amestecViewSlider;
        if(!prefab.TryGetComponent(out amestecViewSlider)) {
            throw new Exception("Cannot Find AmestecViewSlider Component on AmestecViewSlider GameObject");
        }

        //create a TMP_text prefab for each obj in istorieCantitate
        var barsParent = AmestecController.Instance.GetIstoricParent();
        foreach (var cantitateSiData in cantitateData) {
            
            //split string into cantitate & data
            var currentCantitateDataList = cantitateSiData.Split(char.Parse("|"));
            if (currentCantitateDataList.Length != 2) {
                throw new Exception("istorieCantitateData cannot be Split properly");
            }
            
            //split string into cantitate& data
            var cantitate = currentCantitateDataList[0];
            var data = currentCantitateDataList[1];
            
            //set slider value in view
            //TODO: if cantitate > cantitateinitiala
            float fillSliderView = Mathf.Clamp(float.Parse(cantitate),0f, (float)_amestec.CantitateInitiala)
                                   / (float)_amestec.CantitateInitiala;
            amestecViewSlider.SetFillAmountWithPercentage(fillSliderView);
             
            //instantiate text prefab & set text value
            var textPrefab = Instantiate(Tmp_Text_Prefab, barsParent);
            TMP_Text tmpText;
            if (!textPrefab.TryGetComponent(out tmpText)) {
                throw new Exception("TMP_Text prefab does not have TMP_Text GameObject");
            }
            tmpText.text = data +"   "+ cantitate + "g";
        }
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