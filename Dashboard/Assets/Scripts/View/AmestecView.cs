using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;

public class AmestecView : MonoBehaviour
{
    private TMP_Text _idText;
    private TMP_Text _amestecNameText;
    private TMP_Text _cantitateKgText;

    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
    }
    
    /**
    public void SetAmestecValuesInView(string id, string amestecName, float cantitateKg)
    {
         _idText.text = id;
         _amestecNameText.text = amestecName;
         _cantitateKgText.text = cantitateKg.ToString();
    }*/

    public void SetAmestecValuesInView(Amestec amestec)
    {
        _idText.text = amestec.Id.ToString();
        _amestecNameText.text = amestec.Name;
        _cantitateKgText.text = amestec.CantitateKg.ToString();
    }
    

    private void AssignChildTextToPrivateFields()
    {
        if (!transform.GetChild(0).TryGetComponent(out _idText)) {
            throw new Exception("Cannot find ID GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(1).TryGetComponent(out _amestecNameText)) {
            throw new Exception("Cannot find amestecName GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(2).TryGetComponent(out _cantitateKgText)) {
            throw new Exception("Cannot find cantitateKg GameObject or TMP_Text Component");
        }
    }
}