using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using UnityEngine;
using TMPro;

public class BaraController : MonoBehaviour
{
    [SerializeField] private GameObject baraViewPrefab;
    [SerializeField] private Transform baraElemParent;
    
    private List<BaraView> _baraViews; 
    private List<Bara> _bare;

    private void OnEnable()
    {
        _baraViews = new List<BaraView>(); 
        GenerateViewObjects(); //generate the nr of bare we get from realm
    }

    private async void GenerateViewObjects() //better public I think?
    {
        ClearExistingViewObj();
        _bare = await RealmController.GetBaraListFromDB();
        
        foreach(var currentBara in _bare) {
            var newPrefab = Instantiate(baraViewPrefab, baraElemParent);
            BaraView baraView;
            if (!newPrefab.TryGetComponent(out baraView))
                throw new Exception("No BaraView Component is on the prefab GameObject");
            else {
                baraView.SetValuesInView(currentBara);
                _baraViews.Add(baraView);
            }
        }
    }

    private void ClearExistingViewObj()
    {
        _baraViews.Clear();
        ClearChildrenOf(baraElemParent);
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