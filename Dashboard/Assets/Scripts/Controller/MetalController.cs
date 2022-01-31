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
        if (Instance != null && Instance != this) {
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
        backBtn.onClick.RemoveListener(BackBtnOnClick);
        forwardBtn.onClick.RemoveListener(ForwardBtnOnClick);
        Instance = null;
    }

    private async void InitialSetup()
    {
        // ClearExistingViewObj();
        backBtn.onClick.AddListener(BackBtnOnClick);
        forwardBtn.onClick.AddListener(ForwardBtnOnClick);
        await RefreshMetaleList();
    }

    public async Task RefreshMetaleList()
    {
        Metale = await RealmController.GetMetalListFromDB();
        if (Metale.Count > 0) {
            IndexMetal = 0;
            _metalView.SetMetalValuesInView(Metale[IndexMetal]);
            
            backBtn.gameObject.SetActive(false); //disable back button
            if(Metale.Count - 1 == IndexMetal)
                forwardBtn.gameObject.SetActive(false); //disable forward button
            else {
                forwardBtn.gameObject.SetActive(true);
            }
        }
    }

    private void BackBtnOnClick()
    {
        if (IndexMetal > 0) {
            IndexMetal--;
            _metalView.SetMetalValuesInView(Metale[IndexMetal]);
            forwardBtn.gameObject.SetActive(true);
            if (IndexMetal == 0) {
                backBtn.gameObject.SetActive(false);
            }
        }
    }

    private void ForwardBtnOnClick()
    {
        if (IndexMetal < Metale.Count - 1) {
            IndexMetal++;
            _metalView.SetMetalValuesInView(Metale[IndexMetal]);
            backBtn.gameObject.SetActive(true);
            if (IndexMetal == Metale.Count - 1) {
                forwardBtn.gameObject.SetActive(false);
            }
        }
    }

}