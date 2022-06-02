using System.Collections.Generic;
using TMPro;
using UnityEngine;

    public class ResetInputFieldsAndDropDowns : MonoBehaviour
    {
        [SerializeField] private List<TMP_InputField> inputFields;
        [SerializeField] private List<TMP_Dropdown> dropdowns;
        
        public void ResetInput()
        {
            foreach (var input in inputFields) {
                input.text = null;
            }

            foreach (var dropdown in dropdowns) {
                dropdown.value = 0;
            }
        }
    }