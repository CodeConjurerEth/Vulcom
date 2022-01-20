
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddBaraMenuView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownForma;
    [SerializeField] private GameObject gridLayoutGroup;
    [SerializeField] private Button addBaraButton;
    [SerializeField] private Button closeButton;
    private TMP_InputField _nameInputField;
    private TMP_InputField _lungimeBaraInputField;
    private TMP_InputField _diametruInputField;
    private TMP_InputField _laturaSuprafInputField;
    private TMP_InputField _lungimeSuprafInputField;
    private TMP_InputField _latimeSuprafInputField;
    private TMP_InputField _laturaHexagonInputField;

    public Button GetCloseBtn() { return closeButton; }
    public Button GetAddBaraBtn() { return addBaraButton; }
    
    public enum FormaOrderDropdown
    {
        Cerc = 0,
        Patrat = 1,
        Dreptunghi = 2,
        Hexagon = 3
    }
    
    //TODO: add error when fields aren't completed
    
    private void OnEnable()
    {
        AssignChildTextToPrivateFields(gridLayoutGroup.transform);
        disableFormaInputFields();
        _diametruInputField.gameObject.SetActive(true); //default is cerc so have diametru field available as default
        dropdownForma.onValueChanged.AddListener(changeInputFieldsByForma);
        addBaraButton.onClick.AddListener(addOnClick);
    }

    private void OnDisable()
    {
        disableFormaInputFields();
        dropdownForma.onValueChanged.RemoveListener(changeInputFieldsByForma);
        addBaraButton.onClick.RemoveListener(addOnClick);
    }

    private async void addOnClick() // make TASK?
    {
        addBaraToCurrentMetal();
        
        //refresh bara view
        var metalController = MetalController.Instance;
        await BaraController.Instance.GenerateViewObjectsTask(metalController.Metale[metalController.IndexMetal]);
        
        MetalView.Instance.InstantiateOpenBaraMenuBtn();
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
            var lungimeBara = Double.Parse(_lungimeBaraInputField.text);
            
            switch (dropdownForma.value){
                case (int)FormaOrderDropdown.Cerc:
                    var diametru = Double.Parse(_diametruInputField.text);
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Cerc, lungimeBara){
                        Diametru = diametru,
                        Kg = Bara.GetGreutate(Bara.GetAriaCerc(diametru/2), lungimeBara, metalBeingAddedTo.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Patrat:
                    var laturaSupraf = Double.Parse(_laturaSuprafInputField.text);
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Patrat, lungimeBara){
                        LaturaSuprafataPatrat = laturaSupraf,
                        Kg = Bara.GetGreutate(Bara.GetAriaPatrat(laturaSupraf), lungimeBara, metalBeingAddedTo.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Dreptunghi:
                    var lungime = Double.Parse(_lungimeSuprafInputField.text);
                    var latime = Double.Parse(_latimeSuprafInputField.text);
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Dreptunghi, lungimeBara){
                        LungimeSuprafata = lungime,
                        LatimeSuprafata = latime,
                        Kg = Bara.GetGreutate(Bara.GetAriaDreptunghi(lungime, latime), lungimeBara, metalBeingAddedTo.Densitate)
                    });
                    break;
            
                case (int)FormaOrderDropdown.Hexagon:
                    var laturaHexagon = Double.Parse(_laturaHexagonInputField.text);
                    RealmController.AddToDB(new Bara(name, metalBeingAddedTo, (int)Bara.Forme.Hexagon, lungimeBara){
                        LaturaHexagon = laturaHexagon,
                        Kg = Bara.GetGreutate(Bara.GetAriaHexagon(laturaHexagon), lungimeBara, metalBeingAddedTo.Densitate)
                    });
                    break;
            }
        }

       
        
    }
    
    private void changeInputFieldsByForma(int formaInt)
    {
        if (dropdownForma.options.Count != 4)
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

