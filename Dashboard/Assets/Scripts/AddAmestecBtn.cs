
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddAmestecBtn : MonoBehaviour
{
    [SerializeField] private AmestecController _amestecController;
    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField cantitateKgInput;
    private Button _thisBtn;
    
    private void Awake()
    {
        if (!gameObject.TryGetComponent(out _thisBtn)) {
            throw new Exception("this AddAmestecBtn is not attached to an Obj with a Button component!");
        }
        else {
            _thisBtn.onClick.AddListener(SendAmestecToRealm);
        }
    }

    private void SendAmestecToRealm()
    {
        float cantitateKg;
        var parseOK = float.TryParse(cantitateKgInput.text, out cantitateKg);
        if (!parseOK)
            throw new Exception("Cannot parse cantitateKg: (InputField.text) to Float");
        else {
            _amestecController.AddAmestecToRealm(idInput.text, nameInput.text, cantitateKg);
        }
            
    }
}
