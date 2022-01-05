using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;

public class MetalView : MonoBehaviour
{
    [SerializeField] private GameObject _baraViewPrefab;
    // private TMP_Text _idText;
    [SerializeField] private TMP_Text _metalNameText;
    [SerializeField] private TMP_Text _densitate;
    [SerializeField] private TMP_Text _kg;
    [SerializeField] private Transform _bareViewParentObj;
    [SerializeField] private GameObject plusBtnPrefab; //TODO: Add menu
    
    private void OnEnable()
    {
        // AssignChildTextToPrivateFields();
    }
    
    public async void SetMetalValuesInView(Metal metal)
    {
        // _idText.text = metal.Id.ToString();
        _metalNameText.text = metal.Name;
        var densitateTxt = "Densitate: " + metal.Densitate.ToString();
        var kgTxt = "KG: " + metal.Kg.ToString();

        _densitate.text = densitateTxt;
        _kg.text = kgTxt;

        var realm = await RealmController.GetRealm(RealmController.SyncUser);
        var realmCurrMetal = realm.Find<Metal>(metal.Id);
        var bareFromMetal = realm.All<Bara>().Where(thisbara => thisbara.TipMetal == realmCurrMetal); //TODO: fix
        
        foreach (var currBara in bareFromMetal) {
            var newObj = Instantiate(_baraViewPrefab, _bareViewParentObj); //instantiate as a child of _bareViewParentObj
            newObj.GetComponent<BaraView>().SetValuesInView(currBara);;
        }
        Instantiate(plusBtnPrefab, _bareViewParentObj);
    }
    

    // private void AssignChildTextToPrivateFields()
    // {
        // if (!transform.GetChild(0).TryGetComponent(out _idText)) {
        //     throw new Exception("Cannot find ID GameObject or TMP_Text Component");
        // }
        // if (!transform.GetChild(1).TryGetComponent(out _metalNameText)) {
        //     throw new Exception("Cannot find metalName GameObject or TMP_Text Component");
        // }
        // if (!transform.GetChild(2).TryGetComponent(out _densitate)) {
        //     throw new Exception("Cannot find densitate GameObject or TMP_Text Component");
        // }
        // if (!transform.GetChild(3).TryGetComponent(out _kg)) {
        //     throw new Exception("Cannot find kg GameObject or TMP_Text Component");
        // }
        // if (!transform.GetChild(4).TryGetComponent(out _bareViewParentObj)) {
        //     throw new Exception("Cannot find bareView GameObject or TMP_Text Component");
        // }
    // }
}