using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Realms;
using Realms.Sync;
using UnityEngine.UI;

public class AmestecViewName : MonoBehaviour
{
    [Header("ONLY ADD THIS TO PREFAB AMESTECVIEWNAME")]
    private Amestec _amestec;
    private TMP_Text _amestecNameText;
    private Button _thisBtn;
    private Button _deleteBtn;
    
    public Amestec GetAmestec()
    {
        return _amestec;
    }

    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
        if (_thisBtn != null) {
            _thisBtn.onClick.AddListener(OnClickAmestecName);
            _thisBtn.onClick.AddListener(delegate { AmestecController.Instance.SetActiveBtns(true); });
        }
    }

    private void OnDisable()
    {
        if (_thisBtn != null) {
            _thisBtn.onClick.RemoveListener(OnClickAmestecName);
            _thisBtn.onClick.RemoveListener(delegate { AmestecController.Instance.SetActiveBtns(true); });
        }
    }

    private void OnClickAmestecName()
    {
        var amestecController = AmestecController.Instance;
        var thisAmestec = GetAmestec();
        if (amestecController.CurrentAmestec != thisAmestec) {
            amestecController.CurrentAmestec = thisAmestec;
            amestecController.ClearSliderParentView();
            amestecController.GetAmestecViewDataInstance().SetValuesInDataView(thisAmestec);
            amestecController.GenerateDataAndSliderViews();
        }
    }

    public void SetValuesInView(Amestec amestec)
    {
        _amestec = amestec;
        _amestecNameText.text = amestec.Name;
    }

    private void AssignChildTextToPrivateFields()
    {
        if (!transform.TryGetComponent(out _thisBtn)) {
            throw new Exception("Cannot Find Button Component on AmestecViewName Object");
        }
        if (!transform.GetChild(0).TryGetComponent(out _amestecNameText)) {
            throw new Exception("Cannot find AmestecName GameObject or HorizontalLayoutGroup Component");
        }
    }
}