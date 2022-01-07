
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddBaraMenuView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdownForma;
    [SerializeField] private GameObject _gridLayoutGroup;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _closeButton;
    private TMP_InputField _nameInputField;
    private TMP_InputField _lungimeBaraInputField;
    private TMP_InputField _diametruInputField;
    private TMP_InputField _laturaSuprafInputField;
    private TMP_InputField _lungimeSuprafInputField;
    private TMP_InputField _latimeSuprafInputField;
    private TMP_InputField _laturaHexagonInputField;

    public Metal MetalBeingAddedTo;
    public Button GetCloseBtn() { return _closeButton; }
    
    public enum FormaOrderDropdown
    {
        Cerc = 0,
        Patrat = 1,
        Dreptunghi = 2,
        Hexagon = 3
    }
    
    //TODO: add menu when fields aren't completed
    
    private void OnEnable()
    {
        AssignChildTextToPrivateFields(_gridLayoutGroup.transform);
        disableFormaInputFields();
        _diametruInputField.gameObject.SetActive(true); //default is cerc so have diametru field available as default
        _dropdownForma.onValueChanged.AddListener(changeInputFieldsByForma);
        _addButton.onClick.AddListener(AddOnClick);
    }

    private void OnDisable()
    {
        disableFormaInputFields();
        _dropdownForma.onValueChanged.RemoveListener(changeInputFieldsByForma);
    }

    private async void AddOnClick() // make TASK?
    {
        AddBaraToCurrentMetal();
        await BaraController.Instance.GenerateViewObjects(MetalBeingAddedTo);
    }

    private void AddBaraToCurrentMetal()  //add bara on metal we are currently on
    {
        if (MetalBeingAddedTo == null) {
            throw new Exception("No Metal assigned to AddBaraMenuView");
        }
        else {
            var name = _nameInputField.text;
            var tipMetal = MetalBeingAddedTo;
            // long forma = -1;
            var lungimeBara = Double.Parse(_lungimeBaraInputField.text);
            
            switch (_dropdownForma.value){
                case (int)FormaOrderDropdown.Cerc:
                    var diametru = Double.Parse(_diametruInputField.text);
                    RealmController.AddToDB(new Bara(name, tipMetal, (int)Bara.Forme.Cerc, lungimeBara){
                        Diametru = diametru,
                        Kg = Bara.GetGreutate(Bara.GetAriaCerc(diametru/2), lungimeBara, tipMetal.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Patrat:
                    var laturaSupraf = Double.Parse(_laturaSuprafInputField.text);
                    RealmController.AddToDB(new Bara(name, tipMetal, (int)Bara.Forme.Patrat, lungimeBara){
                        LaturaSuprafataPatrat = laturaSupraf,
                        Kg = Bara.GetGreutate(Bara.GetAriaPatrat(laturaSupraf), lungimeBara, tipMetal.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Dreptunghi:
                    var lungime = Double.Parse(_lungimeSuprafInputField.text);
                    var latime = Double.Parse(_latimeSuprafInputField.text);
                    RealmController.AddToDB(new Bara(name, tipMetal, (int)Bara.Forme.Dreptunghi, lungimeBara){
                        LungimeSuprafata = lungime,
                        LatimeSuprafata = latime,
                        Kg = Bara.GetGreutate(Bara.GetAriaDreptunghi(lungime, latime), lungimeBara, tipMetal.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Hexagon:
                    var laturaHexagon = Double.Parse(_laturaHexagonInputField.text);
                    RealmController.AddToDB(new Bara(name, tipMetal, (int)Bara.Forme.Hexagon, lungimeBara){
                        LaturaHexagon = laturaHexagon,
                        Kg = Bara.GetGreutate(Bara.GetAriaHexagon(laturaHexagon), lungimeBara, tipMetal.Densitate)
                    });
                    break;
            }
        }

       
        
    }
    
    private void changeInputFieldsByForma(int formaInt)
    {
        if (_dropdownForma.options.Count != 4)
            throw new Exception("Not enough forma options for bara dropdown menu, make sure you have Cerc, Patrat, Dreptunghi, Hexagon");
        else {
            switch (formaInt) {
                case (int)FormaOrderDropdown.Cerc:
                    disableFormaInputFields();
                    _diametruInputField.gameObject.SetActive(true);
                    break;
                
                case (int)FormaOrderDropdown.Patrat:
                    disableFormaInputFields();
                    _laturaSuprafInputField.gameObject.SetActive(true);
                    break;
                
                case (int)FormaOrderDropdown.Dreptunghi:
                    disableFormaInputFields();
                    _lungimeSuprafInputField.gameObject.SetActive(true);
                    _latimeSuprafInputField.gameObject.SetActive(true);
                    break;
                
                case (int)FormaOrderDropdown.Hexagon:
                    disableFormaInputFields();
                    _laturaHexagonInputField.gameObject.SetActive(true);
                    break;
            }
        }
    }
    
    private void disableFormaInputFields()
    {
        _diametruInputField.gameObject.SetActive(false);
        _laturaSuprafInputField.gameObject.SetActive(false);
        _lungimeSuprafInputField.gameObject.SetActive(false);
        _latimeSuprafInputField.gameObject.SetActive(false);
        _laturaHexagonInputField.gameObject.SetActive(false);
    }

    private void AssignChildTextToPrivateFields(Transform parent)
    {
        if (!parent.GetChild(0).TryGetComponent(out _nameInputField)) {
            throw new Exception("Cannot find nameInputField GameObject or TMP_Text Component");
        }
        if (!parent.GetChild(1).TryGetComponent(out _lungimeBaraInputField)) {
            throw new Exception("Cannot find lungimeBaraInputField GameObject or TMP_Text Component");
        }
        if (!parent.GetChild(2).TryGetComponent(out _diametruInputField)) {
            throw new Exception("Cannot find diametruInputField GameObject or TMP_Text Component");
        }
        if (!parent.GetChild(3).TryGetComponent(out _laturaSuprafInputField)) {
            throw new Exception("Cannot find laturaSuprafInputField GameObject or TMP_Text Component");
        }
        if (!parent.GetChild(4).TryGetComponent(out _lungimeSuprafInputField)) {
            throw new Exception("Cannot find lungimeSuprafInputField GameObject or TMP_Text Component");
        }
        if (!parent.GetChild(5).TryGetComponent(out _latimeSuprafInputField)) {
            throw new Exception("Cannot find latimeSuprafInputField GameObject or TMP_Text Component");
        }
        if (!parent.GetChild(6).TryGetComponent(out _laturaHexagonInputField)) {
            throw new Exception("Cannot find laturaHexagonInputField GameObject or TMP_Text Component");
        }
    }
}

