
using System;
using System.Net;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddBaraView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownForma;
    [SerializeField] private GameObject gridLayoutGroup;
    [SerializeField] private Button addBaraButton;
    [SerializeField] private Button closeButton;
    private TMP_InputField _nameInputField;
    private TMP_InputField _lungimeBaraCMInputField;
    private TMP_InputField _diametruMMInputField;
    private TMP_InputField _laturaSuprafMMInputField;
    private TMP_InputField _lungimeSuprafMMInputField;
    private TMP_InputField _latimeSuprafMMInputField;
    private TMP_InputField _laturaHexagonMMInputField;
    private TMP_Text _emptyInputFieldErrorText;

    public Button GetCloseBtn() { return closeButton; }
    public Button GetAddBaraBtn() { return addBaraButton; }
    
    public enum FormaOrderDropdown
    {
        Cerc = 0,
        Patrat = 1,
        Dreptunghi = 2,
        Hexagon = 3
    }

    private void OnEnable()
    {
        AssignChildTextToPrivateFields(gridLayoutGroup.transform);
        disableFormaInputFields();
        
        //default is cerc so have diametru field available as default
        _diametruMMInputField.gameObject.SetActive(true);
        
        dropdownForma.onValueChanged.AddListener(enableInputFieldsByForma);
        addBaraButton.onClick.AddListener(addOnClick);
    }

    private void OnDisable()
    {
        disableFormaInputFields();
        dropdownForma.onValueChanged.RemoveListener(enableInputFieldsByForma);
        addBaraButton.onClick.RemoveListener(addOnClick);
    }

    public bool areEmptyInputFields()
    {
        if (string.IsNullOrEmpty(_nameInputField.text)
            || string.IsNullOrEmpty(_lungimeBaraCMInputField.text))
            return true;
        switch (dropdownForma.value) {
            case (int)FormaOrderDropdown.Cerc:
                if (string.IsNullOrEmpty(_diametruMMInputField.text))
                    return true;
                break;

            case (int)FormaOrderDropdown.Patrat:
                if (string.IsNullOrEmpty(_laturaSuprafMMInputField.text))
                    return true;
                break;

            case (int)FormaOrderDropdown.Dreptunghi:
                if (string.IsNullOrEmpty(_lungimeSuprafMMInputField.text)
                    || string.IsNullOrEmpty(_latimeSuprafMMInputField.text))
                    return true;
                break;

            case (int)FormaOrderDropdown.Hexagon:
                if (string.IsNullOrEmpty(_laturaHexagonMMInputField.text))
                    return true;
                break;
        }
        return false;
    }

    private async void addOnClick() // TASK?
    {
        if (!areEmptyInputFields()) {
            addBaraToCurrentMetal();

            //hide empty field error messages
            _emptyInputFieldErrorText.gameObject.SetActive(false);
            //refresh bara view
            var metalController = MetalController.Instance;
            await BaraController.Instance.GenerateViewObjectsTask(metalController.Metale[metalController.IndexMetal]);

            MetalView.Instance.InstantiateOpenBaraMenuBtn();
        }
        else {
            _emptyInputFieldErrorText.gameObject.SetActive(true);
        }
    }

    private void addBaraToCurrentMetal()  //add bara on metal we are currently on
    {
        var metalController = MetalController.Instance;
        var metalBeingAddedTo = metalController.Metale[metalController.IndexMetal];
        if (metalBeingAddedTo == null) {
            throw new Exception("No Metal assigned to AddBaraMenuView");
        }
        else {
            var name = _nameInputField.text;
            // long forma = -1;
            var lungimeBaraCM = Double.Parse(_lungimeBaraCMInputField.text);
            
            switch (dropdownForma.value){
                case (int)FormaOrderDropdown.Cerc:
                    var diametruMM = Double.Parse(_diametruMMInputField.text);
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Cerc, lungimeBaraCM){
                        DiametruMM = diametruMM,
                        Grame = Bara.GetGreutate(Bara.GetAriaCerc(Bara.FromMmToCm(diametruMM/2)), lungimeBaraCM, metalBeingAddedTo.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Patrat:
                    var laturaSuprafMM = Double.Parse(_laturaSuprafMMInputField.text);
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Patrat, lungimeBaraCM){
                        LaturaSuprafataPatratMM = laturaSuprafMM,
                        Grame = Bara.GetGreutate(Bara.GetAriaPatrat(Bara.FromMmToCm(laturaSuprafMM)), lungimeBaraCM, metalBeingAddedTo.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Dreptunghi:
                    var lungimeMM = Double.Parse(_lungimeSuprafMMInputField.text);
                    var latimeMM = Double.Parse(_latimeSuprafMMInputField.text);
                    if (lungimeMM < latimeMM) {
                        (lungimeMM, latimeMM) = (latimeMM, lungimeMM); //switch lungime & latime if latime is bigger
                    }
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Dreptunghi, lungimeBaraCM){
                        LungimeSuprafataMM = lungimeMM,
                        LatimeSuprafataMM = latimeMM,
                        Grame = Bara.GetGreutate(Bara.GetAriaDreptunghi(Bara.FromMmToCm(lungimeMM), Bara.FromMmToCm(latimeMM)), lungimeBaraCM, metalBeingAddedTo.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Hexagon:
                    var laturaHexagonMM = Double.Parse(_laturaHexagonMMInputField.text);
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Hexagon, lungimeBaraCM){
                        LaturaHexagonMM = laturaHexagonMM,
                        Grame = Bara.GetGreutate(Bara.GetAriaHexagon(Bara.FromMmToCm(laturaHexagonMM)), lungimeBaraCM, metalBeingAddedTo.Densitate)
                    });
                    break;
            }
        }
    }
    
    private void enableInputFieldsByForma(int formaInt)
    {
        if (dropdownForma.options.Count != 4)
            throw new Exception("Not enough forma options for bara dropdown menu, make sure you have Cerc, Patrat, Dreptunghi, Hexagon");
        else {
            switch (formaInt) {
                case (int)FormaOrderDropdown.Cerc:
                    disableFormaInputFields();
                    _diametruMMInputField.gameObject.SetActive(true);
                    break;
                
                case (int)FormaOrderDropdown.Patrat:
                    disableFormaInputFields();
                    _laturaSuprafMMInputField.gameObject.SetActive(true);
                    break;
                
                case (int)FormaOrderDropdown.Dreptunghi:
                    disableFormaInputFields();
                    _lungimeSuprafMMInputField.gameObject.SetActive(true);
                    _latimeSuprafMMInputField.gameObject.SetActive(true);
                    break;
                
                case (int)FormaOrderDropdown.Hexagon:
                    disableFormaInputFields();
                    _laturaHexagonMMInputField.gameObject.SetActive(true);
                    break;
            }
        }
    }
    
    private void disableFormaInputFields()
    {
        _diametruMMInputField.gameObject.SetActive(false);
        _laturaSuprafMMInputField.gameObject.SetActive(false);
        _lungimeSuprafMMInputField.gameObject.SetActive(false);
        _latimeSuprafMMInputField.gameObject.SetActive(false);
        _laturaHexagonMMInputField.gameObject.SetActive(false);
    }

    private void AssignChildTextToPrivateFields(Transform parent)
    {
        if (!parent.GetChild(0).TryGetComponent(out _nameInputField)) {
            throw new Exception("Cannot find nameInputField GameObject or TMP_InputField Component");
        }
        if (!parent.GetChild(1).TryGetComponent(out _lungimeBaraCMInputField)) {
            throw new Exception("Cannot find lungimeBaraInputField GameObject or TMP_InputField Component");
        }
        if (!parent.GetChild(2).TryGetComponent(out _diametruMMInputField)) {
            throw new Exception("Cannot find diametruInputField GameObject or TMP_InputField Component");
        }
        if (!parent.GetChild(3).TryGetComponent(out _laturaSuprafMMInputField)) {
            throw new Exception("Cannot find laturaSuprafInputField GameObject or TMP_InputField Component");
        }
        if (!parent.GetChild(4).TryGetComponent(out _lungimeSuprafMMInputField)) {
            throw new Exception("Cannot find lungimeSuprafInputField GameObject or TMP_InputField Component");
        }
        if (!parent.GetChild(5).TryGetComponent(out _latimeSuprafMMInputField)) {
            throw new Exception("Cannot find latimeSuprafInputField GameObject or TMP_InputField Component");
        }
        if (!parent.GetChild(6).TryGetComponent(out _laturaHexagonMMInputField)) {
            throw new Exception("Cannot find laturaHexagonInputField GameObject or TMP_InputField Component");
        }
        if (!parent.parent.GetChild(1).TryGetComponent(out _emptyInputFieldErrorText)) {
            throw new Exception("Cannot find emptyInputFieldErrorText GameObject or TMP_Text Component");
        }
    }
}

