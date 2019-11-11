using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowInfo : IClickHandler
{
    public Animator parentAnimator;
    public TextMeshProUGUI text;
    private bool active = false;

    public override void OnClick(ButtonController button)
    {
        active = !active;
        text.SetText(active ? "X Info" : "? Info");
        parentAnimator.SetBool("isActive", active);
    }
}
