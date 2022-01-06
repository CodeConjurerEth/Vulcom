using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;
using UnityEngine.UI;

public class MetalView : MonoBehaviour
{
    public Metal MetalWeOn;
    
    [SerializeField] private GameObject _plusBtnPrefab; //TODO: Add menu
    [SerializeField] private GameObject _addBaraPrefab;
    [SerializeField] private GameObject _baraViewPrefab;  
    [SerializeField] private Transform _bareViewParentObj;
    [SerializeField] private TMP_Text _metalNameText;
    [SerializeField] private TMP_Text _densitate;
    [SerializeField] private TMP_Text _kg;

    private GameObject _addMenuObjInstance;
    private GameObject _plusBtnObjInstance;
    private void OnEnable()
    {
        // AssignChildTextToPrivateFields();
    }

    private void OnDisable()
    {
        Button plusBtn;
        if(!_plusBtnPrefab.TryGetComponent(out plusBtn)) {
            plusBtn.onClick.AddListener(instantiateAddMenu);
        }
        else {
            plusBtn.onClick.RemoveAllListeners();
        }
        
        AddBaraMenuView addBaraMenuView;
        if (!_addBaraPrefab.TryGetComponent(out addBaraMenuView)) {
            throw new Exception("No addBaraView on addMenuPrefab");
        }
        else {
            addBaraMenuView.GetCloseBtn().onClick.RemoveAllListeners();
        }
    }

    public async void SetMetalValuesInView(Metal metal)
    {
        // _idText.text = metal.Id.ToString();
        MetalWeOn = metal;
        assignText(metal); 
        await assignBareViews(metal); 
        instantiatePlusBtn();
        
    }

    private void assignText(Metal metal)
    {
        _metalNameText.text = metal.Name;
        var densitateTxt = "Densitate: " + metal.Densitate.ToString();
        var kgTxt = "KG: " + metal.Kg.ToString();

        _densitate.text = densitateTxt;
        _kg.text = kgTxt;
    }

    private async Task assignBareViews(Metal metal)
    {
        var realm = await RealmController.GetRealm(RealmController.SyncUser);
        var realmCurrMetal = realm.Find<Metal>(metal.Id);
        var bareFromMetal = realm.All<Bara>().Where(thisbara => thisbara.TipMetal == realmCurrMetal); //TODO: fix
        
        foreach (var currBara in bareFromMetal) {
            var newObj = Instantiate(_baraViewPrefab, _bareViewParentObj); //instantiate as a child of _bareViewParentObj
            newObj.GetComponent<BaraView>().SetValuesInView(currBara);;
        }
    }

    private void instantiatePlusBtn()
    {
        Button plusBtnTest;
        if(!_plusBtnPrefab.TryGetComponent(out plusBtnTest)) {
            throw new Exception("No Button Component on plusBtnPrefab!");
        }
        else {
            _plusBtnObjInstance = Instantiate(_plusBtnPrefab, _bareViewParentObj); //add plusBtn at the end
            var plusButton = _plusBtnObjInstance.GetComponent<Button>();
            plusButton.onClick.AddListener(instantiateAddMenu);
            plusButton.onClick.AddListener(destroyPlusBtn);
        }
    }
    
    private void instantiateAddMenu()
    {
        var contentPanelTransform = _bareViewParentObj.parent.parent;
        AddBaraMenuView addBaraMenuViewTest;
        if (!_addBaraPrefab.TryGetComponent(out addBaraMenuViewTest)) {
            throw new Exception("No addBaraView on addMenuPrefab");
        }
        else {
            _addMenuObjInstance = Instantiate(_addBaraPrefab, contentPanelTransform);
            var addBaraMenuView = _addMenuObjInstance.GetComponent<AddBaraMenuView>();
            var closeBtn = addBaraMenuView.GetCloseBtn();

            addBaraMenuView.MetalBeingAddedTo = MetalWeOn;
            closeBtn.onClick.AddListener(destroyAddMenu); //assign destroyAddMenu onclick close btn
            closeBtn.onClick.AddListener(instantiatePlusBtn);
        }
    }

    private void destroyAddMenu()
    {
        if (_addMenuObjInstance != null) {
            Destroy(_addMenuObjInstance);
        }
    }

    private void destroyPlusBtn()
    {
        if (_plusBtnObjInstance != null) {
            Destroy(_plusBtnObjInstance);
        }
    }

}