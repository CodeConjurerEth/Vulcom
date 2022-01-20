
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddAmestecBtn : MonoBehaviour
{
    [SerializeField] private AmestecController _amestecController; //ADD SINGLETON?
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

    private async void SendAmestecToRealm()
    {
        float cantitateKg;
        var parseOK = float.TryParse(cantitateKgInput.text, out cantitateKg);
        if (!parseOK)
            throw new Exception("Cannot parse cantitateKg: (InputField.text) to Float");
        else {
            Amestec amestec = new Amestec(nameInput.text, cantitateKg);
            RealmController.AddToDB(amestec);
            await _amestecController.GenerateViewObjectsTask();
        }
        
    }
}
