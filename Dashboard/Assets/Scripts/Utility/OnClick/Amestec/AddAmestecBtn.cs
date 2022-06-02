
using System;
using System.Threading.Tasks;
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
    [SerializeField] private Button openThisMenuBtn;
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

    private async Task SendAmestecToRealm()
    {   
        //save input
        var name = nameInput.text;
        double grame;
        double duritate;
        
        //parse text input
        var tryParseGrameDouble = double.TryParse(grameInput.text, out grame);
        var tryParseDuritateDouble = double.TryParse(duritateInput.text, out duritate);
        if (!tryParseGrameDouble) 
            throw new Exception("Cannot parse Cantitate(g): (CantitateInputField.text) to Double.");
        if (!tryParseDuritateDouble)
            throw new Exception("Cannot parse Duritate: (DruitateInputField.Text) to Double.");

        //save input
        var culoare = culoareInput.text;
        var lot = lotInput.text;
        var presaProfil = dropdownPresaProfil.captionText.text;

        var dataAchizitie = dropdownZiAchizitie.captionText.text + "." + dropdownLunaAchizitie.captionText.text + "." +
                            dropdownAnAchizitie.captionText.text;
        var dataExpirare = dropdownZiExpirare.captionText.text + "." + dropdownLunaExpirare.captionText.text + "." +
                           dropdownAnExpirare.captionText.text;
        var istorieCantitaticuData = grame.ToString() + "|" + dataAchizitie;

        //TODO: if(grame > cantitateInitiala(pull) AICI!)
        
        //create new Amestec and AddToDB
        var newAmestec = new Amestec(name, grame, culoare, duritate, lot, presaProfil,
                        dataAchizitie, dataExpirare, istorieCantitaticuData); 
        RealmController.AddToDB(newAmestec);

        //refresh names view
        await AmestecController.Instance.GenerateViewNamesTask();
    }

    private async void AddOnClick()
    {
        TMP_Text errorText; 
        if(!transform.GetChild(0).TryGetComponent(out errorText))
            throw new Exception("TMP_Text Component cannot be found on child 0 of" + gameObject +" GameObject");

        if (!areEmptyInputFields()) {
            errorText.color = Color.black; //turn add button text to black
            await SendAmestecToRealm();
            
            addPanel.SetActive(false); //hide panel
            openThisMenuBtn.gameObject.SetActive(true); //show plusBtn (to open add menu)
        }
        else {
            errorText.color = Color.red; //turn add button text to red
        }
    }

    private bool areEmptyInputFields()
    {
        if (string.IsNullOrEmpty(nameInput.text)
            || string.IsNullOrEmpty(grameInput.text) || string.IsNullOrEmpty(duritateInput.text)
            || string.IsNullOrEmpty(culoareInput.text) || string.IsNullOrEmpty(lotInput.text))
            return true;
        return false;
    }
}
