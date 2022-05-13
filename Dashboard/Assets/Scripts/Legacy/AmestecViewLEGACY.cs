using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;
using UnityEngine.UI;

public class AmestecViewLEGACY : MonoBehaviour 
{
    
    [Header("ONLY ADD THIS TO PREFAB AMESTECVIEW")]
    private Amestec _amestec;
    
    private TMP_Text _amestecNameText;
    private TMP_Text _lotText;
    private TMP_Text _dataAchizitie;
    private TMP_Text _grameText;
    private TMP_Text _culoare;
    private TMP_Text _presaProfilText;
    
    private TMP_Text _cantitateInitiala;
    private TMP_Text _dataExpirare;


    private Button _deleteFromDBBtn;

    private int maxStringLength = 9; //

    public Amestec GetAmestec() { return _amestec; }

    private void Start() //TODO: REWORK AMESTEC UI !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        AssignChildTextToPrivateFields();
    }

    private void OnEnable()
    {
        if (_deleteFromDBBtn != null) {
            _deleteFromDBBtn.onClick.AddListener(DeleteCurrentAmestecFromDB);
            _deleteFromDBBtn.onClick.AddListener(AmestecController.Instance.RefreshViewNames);
        }
        
    }

    private void OnDisable()
    {
        _deleteFromDBBtn.onClick.RemoveListener(DeleteCurrentAmestecFromDB);
        _deleteFromDBBtn.onClick.RemoveListener(AmestecController.Instance.RefreshViewNames);
    }

    public void SetAmestecValuesInView(Amestec amestec)
    {
        //TODO: set values forta noi (din model)
        _amestec = amestec;
        
        _amestecNameText.text = amestec.Name;
        var grameString = amestec.Grame.ToString() + " g";
        if (grameString.Length > maxStringLength)
            grameString = grameString.Substring(0, maxStringLength);
        _grameText.text = grameString;
        
        _dataAchizitie.text = amestec.DataAchizitie;
    }

    private void DeleteCurrentAmestecFromDB()
    {
        RealmController.RemoveAmestecFromDB(_amestec.Id);
        _deleteFromDBBtn.onClick.AddListener(AmestecController.Instance.RefreshViewNames);
    }

    private void AssignChildTextToPrivateFields()
    {
        // if(!transform.GetChild(0).TryGetComponent(out _dataAchizitieText)){ 
            // throw new Exception("Cannot find dateTime GameObject or TMP_Text Component");
        // }
        if (!transform.GetChild(1).TryGetComponent(out HorizontalLayoutGroup horizontalLayoutGroup)) {
            throw new Exception("Cannot find HorizontalLayoutGroup GameObject or HorizontalLayoutGroup Component");
        }
        var horizontalLayoutGroupTransform = horizontalLayoutGroup.transform;
        if (!horizontalLayoutGroupTransform.GetChild(0).TryGetComponent(out _amestecNameText)) {
            throw new Exception("Cannot find NumeAmestec GameObject or TMP_Text Component");
        }
        if (!horizontalLayoutGroupTransform.GetChild(1).TryGetComponent(out _grameText)) {
            throw new Exception("Cannot find Cantitate(g) GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(2).TryGetComponent(out _deleteFromDBBtn)) {
            throw new Exception("Cannot find DeleteFromDBBtn GameObject or Button Component");
        }
    }
}