
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddAmestecBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField cantitateKgInput;
    [SerializeField] private GameObject addPanel;
    [SerializeField] private Button plusBtn;
    [SerializeField] private TMP_Text errorEmptyFieldText;
    private Button _thisBtn;
    
    private void Awake()
    {
        if (!gameObject.TryGetComponent(out _thisBtn)) {
            throw new Exception("this AddAmestecBtn is not attached to an Obj with a Button component!");
        }
        else {
            _thisBtn.onClick.AddListener(AddOnClick);
        }
    }

    private async void SendAmestecToRealm()
    {
        double cantitateKg;
        var tryParseDouble = double.TryParse(cantitateKgInput.text, out cantitateKg);
        if (!tryParseDouble)
            throw new Exception("Cannot parse cantitateKg: (InputField.text) to Double.");
        else {
            Amestec amestec = new Amestec(nameInput.text, cantitateKg);
            RealmController.AddToDB(amestec);
            await AmestecController.Instance.GenerateViewObjectsTask();
        }
        
    }

    private void AddOnClick()
    {
        if (!areEmptyInputFields()) {
            errorEmptyFieldText.gameObject.SetActive(false); //hide error
            SendAmestecToRealm();
            
            addPanel.SetActive(false); //hide panel
            plusBtn.gameObject.SetActive(true); //show plusBtn (to open add menu)
        }
        else {
            errorEmptyFieldText.gameObject.SetActive(true); //show error
        }
    }

    private bool areEmptyInputFields()
    {
        if (string.IsNullOrEmpty(nameInput.text)
            || string.IsNullOrEmpty(cantitateKgInput.text))
            return true;
        return false;
    }
}
