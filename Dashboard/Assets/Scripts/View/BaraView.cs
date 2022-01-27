using System;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaraView : MonoBehaviour
{
    [Header("ONLY ADD THIS TO PREFAB BARAVIEW")]
    private Bara _bara;
    private TMP_Text _dateTimeText;
    private TMP_Text _baraNameText;
    private TMP_Text _formaText;
    private TMP_Text _diametruText;
    private TMP_Text _lungimeBaraText;
    private TMP_Text _laturaSuprafataText;
    private TMP_Text _lungimeSuprafataText;
    private TMP_Text _latimeSuprafataText;
    private TMP_Text _laturaHexagonText;
    private TMP_Text _kgText;
    private Button _deleteFromDBBtn;

    public Bara GetBara() { return _bara; }

    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
        if (_deleteFromDBBtn != null) {
            _deleteFromDBBtn.onClick.AddListener(DeleteCurrentBaraFromDB);
            _deleteFromDBBtn.onClick.AddListener(async delegate { 
                var metalController = MetalController.Instance;
               await BaraController.Instance.GenerateViewObjectsTask(metalController.Metale[metalController.IndexMetal]); 
               MetalView.Instance.InstantiateOpenBaraMenuBtn();
               });
        }
    }

    private void OnDisable(){
        _deleteFromDBBtn.onClick.RemoveListener(DeleteCurrentBaraFromDB);
        _deleteFromDBBtn.onClick.RemoveListener(async delegate { 
                var metalController = MetalController.Instance;
                await BaraController.Instance.GenerateViewObjectsTask(metalController.Metale[metalController.IndexMetal]);
                MetalView.Instance.InstantiateOpenBaraMenuBtn();
                 });
    }

    private void DeleteCurrentBaraFromDB()
    {
        if (_bara == null) {
            throw new Exception("bara we're trying to delete is not assigned");
        }
        else
            RealmController.RemoveBaraFromDB(_bara.Id);
    }
    
    public void SetValuesInView(Bara bara)
    {
        _bara = bara;
        _diametruText.gameObject.SetActive(false);
        _laturaSuprafataText.gameObject.SetActive(false);
        _lungimeSuprafataText.gameObject.SetActive(false);
        _latimeSuprafataText.gameObject.SetActive(false);
        _laturaHexagonText.gameObject.SetActive(false);
        
        var aria = -1d;
        switch (bara.Forma) {
            case (int)Bara.Forme.Cerc:
                _formaText.text = "Cerc";
                aria = Bara.GetAriaCerc(bara.Diametru / 2);
                _diametruText.gameObject.SetActive(true);
                _diametruText.SetText("diametru: " + bara.Diametru.ToString());

                break;
            case (int)Bara.Forme.Patrat:
                _formaText.text = "Patrat";
                aria = Bara.GetAriaPatrat(bara.LaturaSuprafataPatrat);
                _laturaSuprafataText.gameObject.SetActive(true);
                _laturaSuprafataText.SetText("laturaSupraf: " + bara.LaturaSuprafataPatrat.ToString());
                
                break;
            case (int)Bara.Forme.Dreptunghi:
                _formaText.text = "Dreptunghi";
                aria = Bara.GetAriaDreptunghi(bara.LungimeSuprafata, bara.LatimeSuprafata);
                _lungimeSuprafataText.gameObject.SetActive(true);
                _latimeSuprafataText.gameObject.SetActive(true);
                _lungimeSuprafataText.SetText("lungimeSupraf: " + bara.LungimeSuprafata.ToString());
                _latimeSuprafataText.SetText("latimeSupraf: " + bara.LatimeSuprafata.ToString());
                
                break;
            case (int)Bara.Forme.Hexagon:
                _formaText.text = "Hexagon";
                aria = Bara.GetAriaHexagon(bara.LaturaHexagon);
                _laturaHexagonText.gameObject.SetActive(true);
                _laturaHexagonText.SetText("laturaHexagon: " + bara.LaturaHexagon.ToString());
                
                break;
            default:
                throw new Exception("Bara " + bara.Name + " does not have a Forma assigned");
        }
        
        _dateTimeText.text = bara.Date;
        _baraNameText.SetText(bara.Name);
        _lungimeBaraText.SetText("lungimeBara: " + bara.LungimeBara.ToString());
        if (Math.Abs(aria - (-1d)) > 0.00000000001d) {              // 11 decimals
            var kg = aria * bara.LungimeBara * bara.TipMetal.Densitate;
            _kgText.SetText("Kg: " + kg.ToString("n2"));
        }
    }
    

    private void AssignChildTextToPrivateFields()
    {
        if (!transform.GetChild(0).TryGetComponent(out _baraNameText)) {
            throw new Exception("Cannot find amestecName GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(1).TryGetComponent(out _formaText)) {
            throw new Exception("Cannot find forma GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(2).TryGetComponent(out _lungimeBaraText)) {
            throw new Exception("Cannot find lungimeBara GameObject or TMP_Text Component");
        }
        //child 3 is inputfield, onclick listeners -- editor
        if (!transform.GetChild(4).TryGetComponent(out _diametruText)) {
            throw new Exception("Cannot find diametru GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(5).TryGetComponent(out _laturaSuprafataText)) {
            throw new Exception("Cannot find laturaSuprafata GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(6).TryGetComponent(out _lungimeSuprafataText)) {
            throw new Exception("Cannot find lungimeSuprafata GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(7).TryGetComponent(out _latimeSuprafataText)) {
            throw new Exception("Cannot find latimeSuprafata GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(8).TryGetComponent(out _laturaHexagonText)) {
            throw new Exception("Cannot find laturaHexagon GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(9).TryGetComponent(out _kgText)) {
            throw new Exception("Cannot find Kg GameObject or TMP_Text Component");
        }
        //bros
        if (!transform.parent.parent.GetChild(0).TryGetComponent(out _dateTimeText)) {
            throw new Exception("Cannot find dateTimeText GameObject or TMP_Text Component");
        }
        if (!transform.parent.GetChild(1).TryGetComponent(out _deleteFromDBBtn)) {
            throw new Exception("Cannot find deleteFromDBBtn GameObject or Button Component as the 2nd child of: " + transform.parent.ToString());
        }
    }
    
}