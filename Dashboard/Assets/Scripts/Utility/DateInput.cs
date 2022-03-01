using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DateInput : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown day;
    [SerializeField] private TMP_Dropdown month;
    [SerializeField] private TMP_Dropdown year;

    public DateTime getDateFromInput()
    {
        return new DateTime(Int32.Parse(year.value.ToString()),Int32.Parse(month.value.ToString()),Int32.Parse(day.value.ToString()));
    }
}
