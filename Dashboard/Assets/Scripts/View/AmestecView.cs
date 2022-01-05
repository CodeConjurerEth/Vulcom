using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;

public class AmestecView : MonoBehaviour 
{
    
    [Header("ONLY ADD THIS TO PREFAB AMESTECVIEW")]
    // private TMP_Text _idText;
    private TMP_Text _amestecNameText;
    private TMP_Text _cantitateKgText;
    private int maxStringLength = 9;

    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
    }
    
    public void SetAmestecValuesInView(Amestec amestec)
    {
        // _idText.text = amestec.Id.ToString();
        _amestecNameText.text = amestec.Name;
        var kgstring = amestec.Kg.ToString();
        if (kgstring.Length > maxStringLength)
            kgstring = kgstring.Substring(0, maxStringLength);
        _cantitateKgText.text = kgstring;
    }
    

    private void AssignChildTextToPrivateFields()
    {
        // if (!transform.GetChild(0).TryGetComponent(out _idText)) {
        //     throw new Exception("Cannot find ID GameObject or TMP_Text Component");
        // }
        if (!transform.GetChild(0).TryGetComponent(out _amestecNameText)) {
            throw new Exception("Cannot find amestecName GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(1).TryGetComponent(out _cantitateKgText)) {
            throw new Exception("Cannot find cantitateKg GameObject or TMP_Text Component");
        }
    }
}