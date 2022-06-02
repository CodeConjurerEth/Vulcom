
using System;
using System.Globalization;
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
            currAmestec.IstorieCantitatiCuData += "," + newGrame.ToString() + "|" + DateTime.Now.ToString("d", new CultureInfo("ro-RO"));
            currAmestec.Grame = newGrame; // remove cantitate grame
        });

        //refresh view data
        RefreshViewDataIsotric(currAmestec);    
        
        Destroy(gameObject);
    }
    
    public void OnClickAddBtn()
    {
        double value = double.Parse(inputFieldAddOrRemove.text);
        var currAmestec = _amestecData.GetAmestec();

        var newGrame = currAmestec.Grame + value;
        _realm.Write(() => {
            currAmestec.IstorieCantitatiCuData +=  "," + newGrame.ToString() + "|" + DateTime.Now.ToString("d", new CultureInfo("ro-RO")) ;
            currAmestec.Grame = newGrame; // add cantitate grame
        });

        //refresh view data
        RefreshViewDataIsotric(currAmestec);
        
        Destroy(gameObject);
    }

    private void RefreshViewDataIsotric(Amestec currAmestec)
    {
        AmestecController.Instance.ClearSliderParentView();
        
        _amestecData.SetValuesInDataView(currAmestec);
        _amestecData.SetIstoricView(currAmestec);
    }
    
}