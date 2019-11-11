using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttributesHelperExtension
{
    public static string ToDescription(this Enum value)
    {
        var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return da.Length > 0 ? da[0].Description : value.ToString();
    }
}
