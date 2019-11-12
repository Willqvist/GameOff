using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPointerClickHandler : MonoBehaviour
{
    public abstract void OnClick(ButtonController button);
}
