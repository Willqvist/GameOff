using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShowInfo : MonoBehaviour
{
    public Animator parentAnimator;
    public TextMeshProUGUI text;
    public bool active = false;

    public void OnClick()
    {
        active = !active;
        Debug.Log(active);
        text.SetText(active ? "X Info" : "? Info");
        parentAnimator.SetBool("isActive", active);
    }
}
