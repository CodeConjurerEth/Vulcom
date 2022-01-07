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
    [SerializeField] private GameObject plusBtnPrefab;
    [SerializeField] private GameObject addBaraPrefab;
    // [SerializeField] private GameObject _baraViewPrefab;  
    // [SerializeField] private Transform _bareViewParentObj;
    [SerializeField] private TMP_Text metalNameText;
    [SerializeField] private TMP_Text densitate;
    [SerializeField] private TMP_Text kg;

    private Metal _metalWeOn; //maybe public?
    private GameObject _addMenuObjInstance; 
    private GameObject _plusBtnObjInstance;
    private void OnEnable()
    {
        // AssignChildTextToPrivateFields();
    }

    private void OnDisable()
    {
        // Button plusBtn;
        // if(!_plusBtnPrefab.TryGetComponent(out plusBtn)) {
        //     plusBtn.onClick.AddListener(instantiateAddMenu);
        // }
        // else {
        //     plusBtn.onClick.RemoveAllListeners();
        // }
        
        AddBaraMenuView addBaraMenuView;
        if (!addBaraPrefab.TryGetComponent(out addBaraMenuView)) {
            throw new Exception("No addBaraView on addMenuPrefab");
        }
        else {
            addBaraMenuView.GetCloseBtn().onClick.RemoveAllListeners();
        }
    }

    public async void SetMetalValuesInView(Metal metal)
    {
        // _idText.text = metal.Id.ToString();
        _metalWeOn = metal;
        assignText(metal); 
        await BaraController.Instance.GenerateViewObjects(metal); 
        instantiatePlusBtn();   //TODO: change class that does this (last 4 func), last 2 private obj up top
        
    }

    private void assignText(Metal metal)
    {
        metalNameText.text = metal.Name;
        var densitateTxt = "Densitate: " + metal.Densitate.ToString();
        var kgTxt = "KG: " + metal.Kg.ToString();

        densitate.text = densitateTxt;
        kg.text = kgTxt;
    }
    
    private void instantiatePlusBtn()
    {
        Button plusBtnTest;
        if(!plusBtnPrefab.TryGetComponent(out plusBtnTest)) {
            throw new Exception("No Button Component on plusBtnPrefab!");
        }
        else {
            _plusBtnObjInstance = Instantiate(plusBtnPrefab, BaraController.Instance.GetBaraViewParent()); //add plusBtn at the end
            var plusButton = _plusBtnObjInstance.GetComponent<Button>();
            plusButton.onClick.AddListener(instantiateAddMenu);
            plusButton.onClick.AddListener(destroyPlusBtn);
        }
    }
    
    private void instantiateAddMenu()
    {
        var contentPanelTransform = BaraController.Instance.GetBaraViewParent().parent.parent; //TODO: careful with this
        AddBaraMenuView addBaraMenuViewTest;
        if (!addBaraPrefab.TryGetComponent(out addBaraMenuViewTest)) {
            throw new Exception("No addBaraView on addMenuPrefab");
        }
        else {
            _addMenuObjInstance = Instantiate(addBaraPrefab, contentPanelTransform);
            var addBaraMenuView = _addMenuObjInstance.GetComponent<AddBaraMenuView>();
            var closeBtn = addBaraMenuView.GetCloseBtn();

            addBaraMenuView.MetalBeingAddedTo = _metalWeOn;
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