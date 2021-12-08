﻿using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;

public class AmestecView : MonoBehaviour
{
    private TMP_Text _idText;
    private TMP_Text _amestecNameText;
    private TMP_Text _cantitateKgText;
    private TMP_Text _cantitateMText;

    private void OnEnable()
    {
        assignChildTextToPrivateFields();
    }

    public void SetAmestecValues(string id, string amestecName, float cantitateKg, float cantitateM)
    {
         _idText.text = id;
         _amestecNameText.text = amestecName;
         _cantitateKgText.text = cantitateKg.ToString();
         _cantitateMText.text = cantitateM.ToString();
    }
    

    private void assignChildTextToPrivateFields()
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
        if (!transform.GetChild(3).TryGetComponent(out _cantitateMText)) {
            throw new Exception("Cannot find cantitateM GameObject or TMP_Text Component");
        }
    }
}