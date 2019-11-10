using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CloseUI : IClickHandler
{
    // Start is called before the first frame update
    public GameObject[] rts;
    private bool expanded = true;


    public override void OnClick(ButtonController button)
    {
        if (expanded) {
            foreach(GameObject rt in rts)
                rt.SetActive(false);
        }
        else
        {
            foreach (GameObject rt in rts)
                rt.SetActive(true);
        }

        expanded = !expanded;
    }

}
