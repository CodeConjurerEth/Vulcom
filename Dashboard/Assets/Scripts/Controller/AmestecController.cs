using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;

public class AmestecController : MonoBehaviour
{
    public static AmestecController Instance;
    public AmestecViewData GetAmestecViewDataInstance() {return amestecViewDataInstance;}
    public Transform GetIstoricParent() { return sliderParent; }
    public Transform GetRightPanel() { return rightPanel; }

    public GameObject GetSliderInstance() { return _sliderInstance;}
    public Amestec CurrentAmestec { get; set; }
    
    [SerializeField] private GameObject amestecViewNamePrefab;
    [SerializeField] private Transform amestecElemParent;
    [SerializeField] private AmestecViewData amestecViewDataInstance;
    [SerializeField] private Transform rightPanel;
    [SerializeField] private Transform sliderParent;
    [SerializeField] private GameObject adaugaCantitateBtn;
    [SerializeField] private GameObject scadeCantitateBtn;
    [SerializeField] private GameObject deleteAmestecBtn;
    [SerializeField] private GameObject amestecViewSliderPrefab;
    [SerializeField] private GameObject tmp_Text_Prefab;
    
    private List<AmestecViewName> _amestecViewsNames; 
    private List<Amestec> _amestecuri;
    private GameObject _sliderInstance;
    
    private void InitSingleton()
    {
        if (Instance != null && Instance != this) {
            Destroy(Instance);
            Debug.Log("Destroyed AmestecController Instance on:"+ Instance.gameObject.ToString() + ", there should only be ONE AmestecController in a scene!");
        }
        Instance = this;
    }
    
    private void Start()
    {
        _amestecViewsNames = new List<AmestecViewName>(); 
        InitSingleton();
        SetActiveBtns(false); //hide Btns
        RefreshNamesView(); //generate the nr of amestecuri we get from realm
    }

    public async Task RefreshNamesViewTask() {
        ClearViewNames();
        await GenerateAmestecViewNames();
    }
    
    public async void RefreshNamesView() {
        ClearViewNames();
        await GenerateAmestecViewNames();
    }
    public void ClearSliderParentView() {
        ClearChildrenOf(sliderParent);
    }

    public void ClearIstoricView() {
        ClearChildrenOf(GetIstoricParent());
    }
    
    public void GenerateIstoricView(Amestec amestec)
    {
        string istorieCantitateData = amestec.IstorieCantitatiCuData;
        var cantitateData = istorieCantitateData.Split(char.Parse(","));

        //instantiate prefab and find amestecViewSlider component
        if(_sliderInstance == null)
            _sliderInstance = Instantiate(amestecViewSliderPrefab, GetRightPanel());
        AmestecViewSlider amestecViewSlider;
        if(!_sliderInstance.TryGetComponent(out amestecViewSlider)) {
            throw new Exception("Cannot Find AmestecViewSlider Component on AmestecViewSlider GameObject");
        }

        //create a TMP_text prefab for each obj in istorieCantitate
        var barsParent = GetIstoricParent();
        foreach (var cantitateSiData in cantitateData) {
            
            //split string into cantitate & data
            var currentCantitateDataList = cantitateSiData.Split(char.Parse("|"));
            if (currentCantitateDataList.Length != 2) {
                throw new Exception("istorieCantitateData cannot be Split properly");
            }
            
            //split string into cantitate & data
            var cantitate = currentCantitateDataList[0];
            var data = currentCantitateDataList[1];
            
            //set slider value in view
            amestecViewSlider.SetFillAmountWithText(float.Parse(cantitate), CurrentAmestec.CantitateInitiala);
             
            //instantiate text prefab & set text value
            var textPrefab = Instantiate(tmp_Text_Prefab, barsParent);
            TMP_Text tmpText;
            if (!textPrefab.TryGetComponent(out tmpText)) {
                throw new Exception("TMP_Text prefab does not have TMP_Text GameObject");
            }
            tmpText.text = data +"   "+ cantitate + "g";
        }
    }

    public void GenerateDataAndSliderViews() {
        //Refresh Sliders View
        var amestecData = amestecViewDataInstance;
        amestecData.SetValuesInDataView(amestecData.GetAmestec());
        GenerateIstoricView(CurrentAmestec);
    }

    public void SetActiveBtns(bool active) {
        if (adaugaCantitateBtn.activeSelf != active) {
            adaugaCantitateBtn.SetActive(active);
            scadeCantitateBtn.SetActive(active);
            deleteAmestecBtn.SetActive(active);
        }
    }

    // Instantiate AmestecView prefab for each amestec from DB, as children of amestecElemParent
    private async Task GenerateAmestecViewNames()
    {
        // if(_amestecuri.Count != 0)
        //     _amestecuri.Clear();
        _amestecuri = await RealmController.GetAmestecListFromDB();
        
        foreach(var currentAmestec in _amestecuri) {
            var newPrefab = Instantiate(amestecViewNamePrefab, amestecElemParent);
            AmestecViewName amestecViewName;
            if (!newPrefab.TryGetComponent(out amestecViewName))
                throw new Exception("No AmestecViewName Component is on the prefab GameObject");
            else {
                amestecViewName.SetValuesInView(currentAmestec);
                _amestecViewsNames.Add(amestecViewName);
            }
        }
    }

    private void ClearViewNames()
    {
        _amestecViewsNames.Clear();
        ClearChildrenOf(amestecElemParent);
    }
    
    private void ClearChildrenOf(Transform parentObj)
    {
        int i = 0;
        
        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[parentObj.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in parentObj)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }

}