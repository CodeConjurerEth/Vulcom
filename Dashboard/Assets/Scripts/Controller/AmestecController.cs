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
    [SerializeField] private GameObject amestecViewPrefab;
    [SerializeField] private Transform amestecElemParent;
    
    private List<AmestecView> _amestecViews; 
    private List<Amestec> _amestecuri;

    private void OnEnable()
    {
        _amestecViews = new List<AmestecView>(); 
        GenerateViewObjects(); //generate the nr of amestecuri we get from realm
    }

    public async void GenerateViewObjects() //it was private, better public I think!
    {
        ClearExistingViewObj();
        _amestecuri = await RealmController.GetAmestecListFromDB();
        
        foreach(var currentAmestec in _amestecuri) {
            var newPrefab = Instantiate(amestecViewPrefab, amestecElemParent);
            AmestecView amestecView;
            if (!newPrefab.TryGetComponent(out amestecView))
                throw new Exception("No AmestecView Component is on the prefab GameObject");
            else {
                amestecView.SetAmestecValuesInView(currentAmestec);
                _amestecViews.Add(amestecView);
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