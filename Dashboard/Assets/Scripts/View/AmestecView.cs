using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;
using UnityEngine.UI;

public class AmestecView : MonoBehaviour 
{
    
    [Header("ONLY ADD THIS TO PREFAB AMESTECVIEW")]
    private Amestec _amestec;
    private TMP_Text _dateTimeText;
    private TMP_Text _amestecNameText;
    private TMP_Text _cantitateKgText;
    private Button _deleteFromDBBtn;

    private int maxStringLength = 9; //

    public Amestec GetAmestec() { return _amestec; }

    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
        if (_deleteFromDBBtn != null) {
            _deleteFromDBBtn.onClick.AddListener(DeleteCurrentAmestecFromDB);
            _deleteFromDBBtn.onClick.AddListener(AmestecController.Instance.GenerateViewObjects);
        }
    }

    private void OnDisable()
    {
        _deleteFromDBBtn.onClick.RemoveListener(DeleteCurrentAmestecFromDB);
        _deleteFromDBBtn.onClick.RemoveListener(AmestecController.Instance.GenerateViewObjects);
    }

    public void SetAmestecValuesInView(Amestec amestec)
    {
        _amestec = amestec;
        
        _dateTimeText.text = amestec.Date;
        _amestecNameText.text = amestec.Name;
        var kgstring = "Kg: " + amestec.Kg.ToString();
        if (kgstring.Length > maxStringLength)
            kgstring = kgstring.Substring(0, maxStringLength);
        _cantitateKgText.text = kgstring;
    }

    private void DeleteCurrentAmestecFromDB()
    {
        RealmController.RemoveAmestecFromDB(_amestec.Id);
    }

    private void AssignChildTextToPrivateFields()
    {
        if(!transform.GetChild(0).TryGetComponent(out _dateTimeText)){
            throw new Exception("Cannot find dateTime GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(1).TryGetComponent(out HorizontalLayoutGroup horizontalLayoutGroup)) {
            throw new Exception("Cannot find HorizontalLayoutGroup GameObject or HorizontalLayoutGroup Component");
        }
        var horizontalLayoutGroupTransform = horizontalLayoutGroup.transform;
        if (!horizontalLayoutGroupTransform.GetChild(0).TryGetComponent(out _amestecNameText)) {
            throw new Exception("Cannot find amestecName GameObject or TMP_Text Component");
        }
        if (!horizontalLayoutGroupTransform.GetChild(1).TryGetComponent(out _cantitateKgText)) {
            throw new Exception("Cannot find cantitateKg GameObject or TMP_Text Component");
        }
        if (!horizontalLayoutGroupTransform.parent.GetChild(2).TryGetComponent(out _deleteFromDBBtn)) {
            throw new Exception("Cannot find DeleteFromDBBtn GameObject or Button Component");
        }
    }
}