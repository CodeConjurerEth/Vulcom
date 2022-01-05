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
    
    private List<Metal> _metale;
    private MetalView _metalView;
    private int indexMetal;


    private void OnEnable()
    {
        if(!transform.TryGetComponent(out _metalView))
            throw new Exception("The MetalController GameObject does not have a metalView Component");
        
        InitialSetup();
    }

    private async void InitialSetup()
    {
        // ClearExistingViewObj();
        backBtn.onClick.AddListener(BackBtnOnClick);
        forwardBtn.onClick.AddListener(ForwardBtnOnClick);
        
        _metale = await RealmController.GetMetalListFromDB();
        if (_metale.Count > 0) {
            indexMetal = 0;
            _metalView.SetMetalValuesInView(_metale[indexMetal]);
            
            backBtn.gameObject.SetActive(false); //disable back button
            if(_metale.Count - 1 == indexMetal)
                forwardBtn.gameObject.SetActive(false); //disable forward button
        }

    }

    private void BackBtnOnClick()
    {
        if (indexMetal >= 1) {
            indexMetal--;
            _metalView.SetMetalValuesInView(_metale[indexMetal]);
            
            if (indexMetal == 0) {
                forwardBtn.gameObject.SetActive(true);
                backBtn.gameObject.SetActive(false);
            }
        }
        
    }

    private void ForwardBtnOnClick()
    {

        if (indexMetal < _metale.Count - 2) {
            indexMetal++;
            _metalView.SetMetalValuesInView(_metale[indexMetal]);

            if (indexMetal == _metale.Count - 1) {
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