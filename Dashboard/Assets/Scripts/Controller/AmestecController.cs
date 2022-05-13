using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;

public class AmestecController : MonoBehaviour
{
    public static AmestecController Instance;
    public AmestecViewData GetAmestecViewDataInstance() {return amestecViewDataInstance;}

    public Transform GetBarsParent() { return sliderParent; }
    
    [SerializeField] private GameObject amestecViewNamePrefab;
    [SerializeField] private Transform amestecElemParent;
    [SerializeField] private AmestecViewData amestecViewDataInstance;
    [SerializeField] private Transform sliderParent;
    
    private List<AmestecViewName> _amestecViews; 
    private List<Amestec> _amestecuri;

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
        _amestecViews = new List<AmestecViewName>(); 
        InitSingleton();
        RefreshViewNames(); //generate the nr of amestecuri we get from realm
    }

    public async Task GenerateViewNamesTask() 
    {
        ClearExistingViewObj();
        await GenerateAmestecViewNames();
    }
    
    public async void RefreshViewNames() 
    {
        ClearExistingViewObj();
        await GenerateAmestecViewNames();
    }
    public void ClearSliderParentView()
    {
        ClearChildrenOf(sliderParent);
    }
    
    public void GenerateSliderAndDataViews()
    {
        //Refresh Sliders View
        var amestecData = amestecViewDataInstance;
        amestecData.SetAmestecValuesInDataView(amestecData.GetAmestec());
        amestecData.SetAmestecSlidersView(amestecData.GetAmestec());
    }

    // Instantiate AmestecView prefab for each amestec from DB, as children of amestecElemParent
    private async Task GenerateAmestecViewNames()
    {
        _amestecuri = await RealmController.GetAmestecListFromDB();
        
        foreach(var currentAmestec in _amestecuri) {
            var newPrefab = Instantiate(amestecViewNamePrefab, amestecElemParent);
            AmestecViewName amestecViewName;
            if (!newPrefab.TryGetComponent(out amestecViewName))
                throw new Exception("No AmestecViewName Component is on the prefab GameObject");
            else {
                amestecViewName.SetAmestecValuesInView(currentAmestec);
                _amestecViews.Add(amestecViewName);
            }
        }
    }


    private void ClearExistingViewObj()
    {
        _amestecViews.Clear();
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