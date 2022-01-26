using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BaraController : MonoBehaviour
{
    public static BaraController Instance;
    [SerializeField] private GameObject baraViewPrefab;
    [SerializeField] private Transform bareViewParentTransform;
    
    private List<BaraView> _baraViews; 
    private List<Bara> _bare;

    public Transform GetBaraViewParent() { return bareViewParentTransform; }
    
    private void OnEnable()
    {
        if (Instance != null) {
            Destroy(Instance);
            Debug.Log("Destroyed BaraController Instance on:"+ Instance.gameObject.ToString() + ", there should only be ONE BaraController in a scene!");
        }
        Instance = this;

        _baraViews = new List<BaraView>();
    }

    private void OnDisable()
    {
        Instance = null;
    }

    public async Task GenerateViewObjectsTask(Metal metal)
    {
        ClearExistingViewObj();
        
        var realm = await RealmController.GetRealm(RealmController.SyncUser);
        var realmCurrMetal = realm.Find<Metal>(metal.Id);
        var bareFromMetal = realm.All<Bara>().Where(thisbara => thisbara.TipMetal == realmCurrMetal);
        
        foreach (var currBara in bareFromMetal) {
            var newObj = Instantiate(baraViewPrefab, bareViewParentTransform); //instantiate as a child of _bareViewParentObj
            newObj.transform.GetChild(1).GetComponent<BaraView>().SetValuesInView(currBara);;
        }
    }

    private void ClearExistingViewObj()
    {
        _baraViews.Clear();
        ClearChildrenOf(bareViewParentTransform);
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