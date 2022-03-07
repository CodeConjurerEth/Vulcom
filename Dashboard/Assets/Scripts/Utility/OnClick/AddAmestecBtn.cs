
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddAmestecBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField duritateInput;
    [SerializeField] private TMP_InputField grameInput;
    [SerializeField] private TMP_InputField culoareInput;
    [SerializeField] private TMP_InputField lotInput;
    [SerializeField] private TMP_Dropdown dropdownPresaProfil;
    [SerializeField] private TMP_Dropdown dropdownZiAchizitie;
    [SerializeField] private TMP_Dropdown dropdownLunaAchizitie;
    [SerializeField] private TMP_Dropdown dropdownAnAchizitie;
    [SerializeField] private TMP_Dropdown dropdownZiExpirare;
    [SerializeField] private TMP_Dropdown dropdownLunaExpirare;
    [SerializeField] private TMP_Dropdown dropdownAnExpirare;
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
        var tryParseGrameDouble = double.TryParse(grameInput.text, out grame);
        double duritate;
        var tryParseDuritateDouble = double.TryParse(duritateInput.text, out duritate);

        if (!tryParseGrameDouble) 
            throw new Exception("Cannot parse Cantitate(g): (CantitateInputField.text) to Double.");
        if (!tryParseDuritateDouble)
            throw new Exception("Cannot parse Duritate: (DruitateInputField.Text) to Double.");

        var dataAchizitie = dropdownZiAchizitie.captionText.text + "." + dropdownLunaAchizitie.captionText.text + "." +
                            dropdownAnAchizitie.captionText.text;
        var dataExpirare = dropdownZiExpirare.captionText.text + "." + dropdownLunaExpirare.captionText.text + "." +
                           dropdownAnExpirare.captionText.text;
        RealmController.AddToDB(new Amestec(nameInput.text, grame){
          CantitateInitiala  = grame,
          Culoare = culoareInput.text,
          Lot = lotInput.text, 
          PresaProfil = dropdownPresaProfil.captionText.text,
          DataAchizitie = dataAchizitie,
          DataExpirare = dataExpirare
        });

        await AmestecController.Instance.GenerateViewObjectsTask();
        
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
            || string.IsNullOrEmpty(grameInput.text) || string.IsNullOrEmpty(duritateInput.text) || string.IsNullOrEmpty(culoareInput.text) || string.IsNullOrEmpty(lotInput.text))
            return true;
        return false;
    }
}
