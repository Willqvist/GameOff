using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowInfo : MonoBehaviour
{
    public Animator parentAnimator;
    public TextMeshProUGUI text;
    private bool active = false;

    public void OnClick()
    {
        active = !active;
        text.SetText(active ? "X Info" : "? Info");
        parentAnimator.SetBool("isActive", active);
    }
}
