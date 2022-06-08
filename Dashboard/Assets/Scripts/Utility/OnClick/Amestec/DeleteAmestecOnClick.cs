
using System;
using UnityEngine;
using UnityEngine.UI;

public class DeleteAmestecOnClick : MonoBehaviour
{
    private Button _thisBtn;

    private void Awake()
    {
        if (!TryGetComponent(out _thisBtn))
            throw new Exception("Cannot find Button Component deleteAmestecBtn on " + this);
        _thisBtn.onClick.AddListener(DeleteCurrentAmestec);
    }

    private void OnDisable()
    {
        _thisBtn.onClick.AddListener(DeleteCurrentAmestec);
    }

    private void DeleteCurrentAmestec()
    {
        var amestecController = AmestecController.Instance;
        var currentAmestec = amestecController.CurrentAmestec;
        if (currentAmestec != null) {
            RealmController.RemoveAmestecFromDB(currentAmestec.Id);
            amestecController.CurrentAmestec = null;
            
            amestecController.RefreshNamesView();
            amestecController.GetAmestecViewDataInstance().ResetFieldsNull();
            amestecController.ClearIstoricView();
            amestecController.SetActiveBtns(false);
            amestecController.GetSliderInstance().GetComponent<AmestecViewSlider>().RefreshSliderView();
        }
    } 
}
