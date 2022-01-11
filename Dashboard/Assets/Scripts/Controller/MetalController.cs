using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MetalController : MonoBehaviour
{
    [Header("Add this to MetalController GameObj MetalView")]
    [SerializeField] private Button backBtn;
    [SerializeField] private Button forwardBtn;

    [HideInInspector] public static MetalController Instance;
    [HideInInspector] public List<Metal> Metale;
    [HideInInspector] public int IndexMetal;
    
    private MetalView _metalView;


    private void OnEnable()
    {
        if (Instance != null) {
            Destroy(Instance);
            Debug.Log("Destroyed MetalController Instance on:"+ Instance.gameObject.ToString() + ", there should only be ONE MetalController in a scene!");
        }
        Instance = this;
        
        if(!transform.TryGetComponent(out _metalView))
            throw new Exception("The MetalController GameObject does not have a metalView Component");
        
        InitialSetup();
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private async void InitialSetup()
    {
        // ClearExistingViewObj();
        backBtn.onClick.AddListener(BackBtnOnClick);
        forwardBtn.onClick.AddListener(ForwardBtnOnClick);
        
        Metale = await RealmController.GetMetalListFromDB();
        if (Metale.Count > 0) {
            IndexMetal = 0;
            _metalView.SetMetalValuesInView(Metale[IndexMetal]);
            
            backBtn.gameObject.SetActive(false); //disable back button
            if(Metale.Count - 1 == IndexMetal)
                forwardBtn.gameObject.SetActive(false); //disable forward button
        }

    }

    private void BackBtnOnClick()
    {
        if (IndexMetal >= 1) {
            IndexMetal--;
            _metalView.SetMetalValuesInView(Metale[IndexMetal]);
            
            if (IndexMetal == 0) {
                forwardBtn.gameObject.SetActive(true);
                backBtn.gameObject.SetActive(false);
            }
        }
        
    }

    private void ForwardBtnOnClick()
    {

        if (IndexMetal < Metale.Count - 2) {
            IndexMetal++;
            _metalView.SetMetalValuesInView(Metale[IndexMetal]);

            if (IndexMetal == Metale.Count - 1) {
                backBtn.gameObject.SetActive(true);
                forwardBtn.gameObject.SetActive(false);
            }
        }
    }

    /*

    private void ClearExistingViewObj()
    {
        _metalViews.Clear();
        ClearChildrenOf(metalElemParent);
    }
    
    /*
     
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
     */

}