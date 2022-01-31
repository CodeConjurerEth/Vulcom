
using System;
using UnityEngine;
using UnityEngine.UI;

public class OpenAddMetalView : MonoBehaviour
{
    [SerializeField] private GameObject addMetalPanelPrefab;
    [SerializeField] private Transform metalContentPanel;
    private Button thisBtn;
    private GameObject panelInstance;

    private void OnEnable()
    {
        if(!TryGetComponent(out thisBtn)) {
            throw new Exception("OpenAddMetalView should be added on a GameObject with a Button Component!");
        }
        else
            thisBtn.onClick.AddListener(InstantiateAddMetalPanelPrefab);
    }

    private void OnDisable()
    {
        thisBtn.onClick.RemoveListener(InstantiateAddMetalPanelPrefab);
    }

    private void InstantiateAddMetalPanelPrefab()
    {
        panelInstance = Instantiate(addMetalPanelPrefab, metalContentPanel);
    }
}
