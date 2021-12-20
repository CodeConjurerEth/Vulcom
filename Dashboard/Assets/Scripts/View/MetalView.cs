using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;

public class MetalView : MonoBehaviour
{
    private TMP_Text _idText;
    private TMP_Text _metalNameText;
    private List<BaraView> _bareView;
    // private TMP_Text 

    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
    }
    
    public void SetMetalValuesInView(Metal metal)
    {
        _idText.text = metal.Id.ToString();
        _metalNameText.text = metal.Name;
        // _bareObj.text = metal.Kg.ToString();
    }
    

    private void AssignChildTextToPrivateFields()
    {
        if (!transform.GetChild(0).TryGetComponent(out _idText)) {
            throw new Exception("Cannot find ID GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(1).TryGetComponent(out _metalNameText)) {
            throw new Exception("Cannot find metalName GameObject or TMP_Text Component");
        }
        // if (!transform.GetChild(2).TryGetComponent(out _bareObj)) {
        //     throw new Exception("Cannot find cantitateKg GameObject or TMP_Text Component");
        // }
    }
}