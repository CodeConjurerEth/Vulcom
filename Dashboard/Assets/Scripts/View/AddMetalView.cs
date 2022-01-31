
using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddMetalView : MonoBehaviour
{
    private TMP_InputField nameInputField;
    private TMP_InputField densitateInputField;
    private Button addToDBBtn;
    private Button closeThisPanelBtn;

    private void SelfDestruct() { Destroy(gameObject); }

    private void OnEnable()
    {
        AssignChildObjects();
        addToDBBtn.onClick.AddListener(AddMetalToDB);
        closeThisPanelBtn.onClick.AddListener(SelfDestruct);
    }

    private void OnDisable()
    {
        addToDBBtn.onClick.RemoveListener(AddMetalToDB);
        closeThisPanelBtn.onClick.RemoveListener(SelfDestruct);
    }

    private void AddMetalToDB()
    {
        double densitate;
        if (!areEmptyInputFields()) {
            var tryParseDouble = double.TryParse(densitateInputField.text, out densitate);
            if (!tryParseDouble) {
                throw new Exception("Cannot parse densitate: (densitateInputfield.text) to Double.");
            }
            else {
                OnAddMetalToDB(densitate);
            }
        }
    }

    private void OnAddMetalToDB(double densitate)
    {
        Metal newMetal = new Metal(nameInputField.text, densitate);
        RealmController.AddToDB(newMetal);
        
        RefreshView();
        SelfDestruct();
    }
    
    private async void RefreshView()
    {
        await MetalController.Instance.RefreshMetaleList();
        //refresh baraview?
    }
    
    private bool areEmptyInputFields()
    {
        if (string.IsNullOrEmpty(nameInputField.text)
            || string.IsNullOrEmpty(densitateInputField.text))
            return true;
        return false;
    }

    private void AssignChildObjects()
    {
        if (!transform.GetChild(0).TryGetComponent(out nameInputField)) {
            throw new Exception("Cannot find nameInputField GameObject or TMP_InputField Component");
        }
        if (!transform.GetChild(1).TryGetComponent(out densitateInputField)) {
            throw new Exception("Cannot find densitateInputField GameObject or TMP_InputField Component");
        }
        if (!transform.GetChild(2).TryGetComponent(out addToDBBtn)) {
            throw new Exception("Cannot find AddBtn GameObject or Button Component");
        }
        if (!transform.GetChild(3).TryGetComponent(out closeThisPanelBtn)) {
            throw new Exception("Cannot find closeThisPanelBtn GameObject or Button Component");
        }
    }
}
