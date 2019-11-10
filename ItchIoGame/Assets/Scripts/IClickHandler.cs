using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IClickHandler : MonoBehaviour
{
    public abstract void OnClick(ButtonController button);
}
