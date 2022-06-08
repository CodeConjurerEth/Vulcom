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
    private TMP_Text _lungimeBaraTextInCM;
    private TMP_Text _diametruTextInMM;
    private TMP_Text _laturaSuprafataTextInMM;
    private TMP_Text _lungimeSuprafataTextInMM;
    private TMP_Text _latimeSuprafataTextInMM;
    private TMP_Text _laturaHexagonTextInMM;
    private TMP_Text _grameText;
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
        _diametruTextInMM.gameObject.SetActive(false);
        _laturaSuprafataTextInMM.gameObject.SetActive(false);
        _lungimeSuprafataTextInMM.gameObject.SetActive(false);
        _latimeSuprafataTextInMM.gameObject.SetActive(false);
        _laturaHexagonTextInMM.gameObject.SetActive(false);
        
        var ariaMM = -1d;
        switch (bara.Forma) {
            case (int)Bara.Forme.Cerc:
                _formaText.text = "Cerc";
                ariaMM = Bara.GetAriaCerc(bara.DiametruMM / 2);
                _diametruTextInMM.gameObject.SetActive(true);
                _diametruTextInMM.SetText("diametru: " + bara.DiametruMM.ToString() + " mm");

                break;
            case (int)Bara.Forme.Patrat:
                _formaText.text = "Patrat";
                ariaMM = Bara.GetAriaPatrat(bara.LaturaSuprafataPatratMM);
                _laturaSuprafataTextInMM.gameObject.SetActive(true);
                _laturaSuprafataTextInMM.SetText("laturaSectiune: " + bara.LaturaSuprafataPatratMM.ToString() + " mm");
                
                break;
            case (int)Bara.Forme.Dreptunghi:
                _formaText.text = "Dreptunghi";
                ariaMM = Bara.GetAriaDreptunghi(bara.LungimeSuprafataMM, bara.LatimeSuprafataMM);
                _lungimeSuprafataTextInMM.gameObject.SetActive(true);
                _latimeSuprafataTextInMM.gameObject.SetActive(true);
                _lungimeSuprafataTextInMM.SetText("lungimeSectiune: " + bara.LungimeSuprafataMM.ToString() + " mm");
                _latimeSuprafataTextInMM.SetText("latimeSectiune: " + bara.LatimeSuprafataMM.ToString() + " mm");
                
                break;
            case (int)Bara.Forme.Hexagon:
                _formaText.text = "Hexagon";
                ariaMM = Bara.GetAriaHexagon(bara.LaturaHexagonMM);
                _laturaHexagonTextInMM.gameObject.SetActive(true);
                _laturaHexagonTextInMM.SetText("laturaHexagonSectiune: " + bara.LaturaHexagonMM.ToString()+ " mm");
                
                break;
            default:
                throw new Exception("Bara " + bara.Name + " does not have a Forma assigned");
        }
        
        _dateTimeText.text = bara.Date;
        _baraNameText.SetText(bara.Name);
        _lungimeBaraTextInCM.SetText("lungimeBara: " + bara.LungimeBaraCM.ToString() + " cm");
        if (Math.Abs(ariaMM - (-1d)) > 0.00000000001d) {              // 11 decimals
            var grame = Bara.FromMmToCm(ariaMM) * bara.LungimeBaraCM * bara.TipMetal.Densitate;
            _grameText.SetText(grame.ToString("n2") + " g");
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
        if (!transform.GetChild(2).TryGetComponent(out _lungimeBaraTextInCM)) {
            throw new Exception("Cannot find lungimeBaraInCM GameObject or TMP_Text Component");
        }
        //child 3 is inputfield, onclick listeners -- editor
        if (!transform.GetChild(4).TryGetComponent(out _diametruTextInMM)) {
            throw new Exception("Cannot find diametruInMM GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(5).TryGetComponent(out _laturaSuprafataTextInMM)) {
            throw new Exception("Cannot find laturaSuprafatainMM GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(6).TryGetComponent(out _lungimeSuprafataTextInMM)) {
            throw new Exception("Cannot find lungimeSuprafatainMM GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(7).TryGetComponent(out _latimeSuprafataTextInMM)) {
            throw new Exception("Cannot find latimeSuprafatainMM GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(8).TryGetComponent(out _laturaHexagonTextInMM)) {
            throw new Exception("Cannot find laturaHexagonInMM GameObject or TMP_Text Component");
        }
        if (!transform.GetChild(9).TryGetComponent(out _grameText)) {
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