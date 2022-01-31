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
    public static MetalView Instance;
    [SerializeField] private GameObject openAddBaraMenuBtnPrefab;
    [SerializeField] private GameObject addBaraPanelPrefab;
    [SerializeField] private TMP_Text metalNameText;
    [SerializeField] private TMP_Text densitate;
    [SerializeField] private TMP_Text grame;

    private GameObject _addMenuObjInstance; 
    private GameObject _addMenuBtnObjInstance;

    private void OnEnable()
    {
        //  instantiate from Controller --> SetMetalValuesInView(Metal metal)
         if (Instance != null && Instance != this) {
            Destroy(Instance);
            Debug.Log("Destroyed MetalView Instance on:"+ Instance.gameObject.ToString() + ", there should only be ONE MetalView in a scene!");
        }
        Instance = this;
    }

    private void OnDisable()
    {
        
        AddBaraView addBaraView;
        if (!addBaraPanelPrefab.TryGetComponent(out addBaraView)) {
            throw new Exception("No addBaraView on addPanelPrefab");
        }
        else {
            var closeBtn = addBaraView.GetCloseBtn();
            var addBtn = addBaraView.GetAddBaraBtn();
            closeBtn.onClick.RemoveListener(destroyOpenAddMenu);
            closeBtn.onClick.RemoveListener(InstantiateOpenBaraMenuBtn);
            addBtn.onClick.RemoveListener(destroyOpenAddMenu);
            addBtn.onClick.RemoveListener(async delegate{
                 var metalController = MetalController.Instance;
                 await BaraController.Instance.GenerateViewObjectsTask(metalController.Metale[metalController.IndexMetal]);
                 });
            addBtn.onClick.RemoveListener(InstantiateOpenBaraMenuBtn);
        }
    }

    public async void SetMetalValuesInView(Metal metal)
    {
        // _idText.text = metal.Id.ToString();
        assignText(metal); 
        await BaraController.Instance.GenerateViewObjectsTask(metal); 
        InstantiateOpenBaraMenuBtn();
    }
    
    public void InstantiateOpenBaraMenuBtn()
    {
        Button addBaraMenuBtnTest;
        if(!openAddBaraMenuBtnPrefab.TryGetComponent(out addBaraMenuBtnTest)) {
            throw new Exception("No Button Component on addBtnPrefab!");
        }
        else {
            _addMenuBtnObjInstance = Instantiate(openAddBaraMenuBtnPrefab, BaraController.Instance.GetBaraViewParent()); //add addBtn at the end
            var openMenuButton = _addMenuBtnObjInstance.GetComponent<Button>();
            openMenuButton.onClick.AddListener(instantiateAddMenu);
            openMenuButton.onClick.AddListener(destroyAddMenuBtn);
        }
    }
    
    private void InstantiateOpenBaraMenuBtnOnAdd()
    {
        if(!_addMenuObjInstance.GetComponent<AddBaraView>().areEmptyInputFields())
        {
           InstantiateOpenBaraMenuBtn();
        }
    }
    
    private void instantiateAddMenu()
    {
        var contentPanelTransform = BaraController.Instance.GetBaraViewParent().parent.parent; //TODO: careful with this
        AddBaraView addBaraViewTest;
        if (!addBaraPanelPrefab.TryGetComponent(out addBaraViewTest)) {
            throw new Exception("No addBaraView on addBaraPanelPrefab");
        }
        else {
            _addMenuObjInstance = Instantiate(addBaraPanelPrefab, contentPanelTransform);
            var addBaraMenuView = _addMenuObjInstance.GetComponent<AddBaraView>();
           
            var closeBtn = addBaraMenuView.GetCloseBtn();
            closeBtn.onClick.AddListener(destroyOpenAddMenu); //assign destroyAddMenu onclick close btn
            closeBtn.onClick.AddListener(InstantiateOpenBaraMenuBtn);
            
            //when we add bara we destroy addMenuPanel, refresh bara Objects from DB and add the button to open menu again at the end
            //we add the Bara to DB in AddBaraMenuView
            var addBaraBtn = addBaraMenuView.GetAddBaraBtn();
            addBaraBtn.onClick.AddListener(destroyOpenAddMenu);
            // InstantiateOpenBaraMenuBtnOnAdd is happening in addBaraMenuView addOnClick()
        }
    }

    private void destroyOpenAddMenu()
    {
        if(!_addMenuObjInstance.GetComponent<AddBaraView>().areEmptyInputFields()){ //check that all inputfields are not empty
            if (_addMenuObjInstance != null) {
                Destroy(_addMenuObjInstance);
            }
        }
    }

    private void destroyAddMenuBtn()
    {
        if (_addMenuBtnObjInstance != null) {
            Destroy(_addMenuBtnObjInstance);
        }
    }

    private void assignText(Metal metal)
    {
        metalNameText.text = metal.Name;
        var densitateTxt = "Densitate: " + metal.Densitate.ToString();
        var grameText = "g: " + metal.Grame.ToString();

        densitate.text = densitateTxt;
        grame.text = grameText;
    }
}