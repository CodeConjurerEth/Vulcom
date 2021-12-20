using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;

public class MetalController : MonoBehaviour
{
    [SerializeField] private GameObject metalViewPrefab;
    [SerializeField] private Transform metalElemParent;
    
    private List<MetalView> _metalViews; 
    private List<Metal> _metale;

    private void OnEnable()
    {
        _metalViews = new List<MetalView>(); 
        GenerateViewObjects(); //generate the nr of metale we get from realm
    }

    public async void GenerateViewObjects() //it was private, better public I think!
    {
        ClearExistingViewObj();
        _metale = await RealmController.GetMetalListFromDB();
        
        foreach(var currentMetal in _metale) {
            var newPrefab = Instantiate(metalViewPrefab, metalElemParent);
            MetalView metalView;
            if (!newPrefab.TryGetComponent(out metalView))
                throw new Exception("No MetalView Component is on the prefab GameObject");
            else {
                metalView.SetMetalValuesInView(currentMetal);
                _metalViews.Add(metalView);
            }
        }
    }

    private void ClearExistingViewObj()
    {
        _metalViews.Clear();
        ClearChildrenOf(metalElemParent);
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