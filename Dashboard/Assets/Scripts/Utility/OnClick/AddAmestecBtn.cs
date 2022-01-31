
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddAmestecBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField grameInput;
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
        double grame;
        var tryParseDouble = double.TryParse(grameInput.text, out grame);
        if (!tryParseDouble)
            throw new Exception("Cannot parse Cantitate(g): (InputField.text) to Double.");
        else {
            Amestec amestec = new Amestec(nameInput.text, grame);
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
            || string.IsNullOrEmpty(grameInput.text))
            return true;
        return false;
    }
}
