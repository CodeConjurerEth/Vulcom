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

    public Amestec GetAmestec() { return _amestec; }

    private void OnEnable()
    {
        AssignChildTextToPrivateFields();
        if (_thisBtn != null) {
            _thisBtn.onClick.AddListener(OnClickAmestecName);
        }

    }

    private void OnDisable()
    {
        if (_thisBtn != null) {
            _thisBtn.onClick.RemoveListener(OnClickAmestecName);
        }
            
    }

    private void OnClickAmestecName()
    {
        var currAmestec = GetAmestec();
        AmestecController.Instance.GetAmestecViewDataInstance().SetAmestecValuesInDataView(currAmestec);
        AmestecController.Instance.GenerateSliderAndDataViews();
    }
    
    public void SetAmestecValuesInView(Amestec amestec)
    {
        _amestec = amestec;
        _amestecNameText.text = amestec.Name;
    }

    private void AssignChildTextToPrivateFields()
    {
        if (!transform.TryGetComponent(out _thisBtn)) {
            throw new Exception("Cannot Find Button Component on AmestecViewName Object");
        }
        
        if (!transform.GetChild(0).TryGetComponent(out VerticalLayoutGroup verticalLayoutGroup)) {
            throw new Exception("Cannot find VerticalLayoutGroup GameObject or HorizontalLayoutGroup Component");
        }
        var verticalLayoutGroupTransform = verticalLayoutGroup.transform;
        
        if (!verticalLayoutGroupTransform.GetChild(0).TryGetComponent(out _amestecNameText)) {
            throw new Exception("Cannot find AmestecName GameObject or HorizontalLayoutGroup Component");
        }
    }
}