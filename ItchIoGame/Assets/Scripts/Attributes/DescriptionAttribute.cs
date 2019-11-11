using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionAttribute : Attribute
{
    public string Description { get; private set; }

    public DescriptionAttribute(string desc)
    {
        this.Description = desc;
    }
}
