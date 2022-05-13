
using System;
using Realms;
using TMPro;
using UnityEngine;

public class AmestecCantitateAddRemove : MonoBehaviour
{

    [Tooltip("ONLY ADD THIS TO PREFAB 'Remove/Add' + 'CantitateMenu'")]
    [SerializeField] private TMP_InputField inputFieldAddOrRemove;

    private AmestecViewData _amestecData;
    private Realm _realm;
    private async void Awake()
    {
        _amestecData = AmestecController.Instance.GetAmestecViewDataInstance();
        _realm = await RealmController.GetRealm(RealmController.SyncUser);
    }

    public void OnClickRemoveBtn()
    {
        double value = double.Parse(inputFieldAddOrRemove.text);
        var currAmestec = _amestecData.GetAmestec();
        
        
        var newGrame = currAmestec.Grame - value;
        _realm.Write(() => {
            currAmestec.IstorieCantitate += "," + newGrame.ToString();
            currAmestec.Grame -= newGrame;
        });

        //refresh view data
        AmestecController.Instance.ClearSliderParentView();
        _amestecData.SetAmestecSlidersView(currAmestec);
        _amestecData.SetAmestecValuesInDataView(currAmestec);
        
        Destroy(gameObject);
    }
    
    public void OnClickAddBtn()
    {
        double value = double.Parse(inputFieldAddOrRemove.text);
        var currAmestec = _amestecData.GetAmestec();

        var newGrame = currAmestec.Grame + value;
        _realm.Write(() => {
            currAmestec.IstorieCantitate +=  "," + newGrame.ToString();
            currAmestec.Grame += newGrame;
        });

        //refresh view data
        AmestecController.Instance.ClearSliderParentView();
        _amestecData.SetAmestecSlidersView(currAmestec);
        _amestecData.SetAmestecValuesInDataView(currAmestec);
        
        Destroy(gameObject);
    }
    
}