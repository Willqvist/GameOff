using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CloseUI : MonoBehaviour, IPointerUpHandler
{
    // Start is called before the first frame update
    public GameObject[] rts;
    private bool expanded = true;

    public void OnPointerUp(PointerEventData eventData)
    {
        SetUIVisible(!expanded);
    }

    public void SetUIVisible(bool state)
    {
        if (!state)
        {
            foreach (GameObject rt in rts)
                rt.SetActive(false);
        }
        else
        {
            foreach (GameObject rt in rts)
                rt.SetActive(true);
        }

        expanded = state;
    }
}
