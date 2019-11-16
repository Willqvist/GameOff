using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Money : MonoBehaviour
{
    private TextMeshProUGUI inst;

    void Start()
    {
        this.inst = this.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        this.inst.text = "$ " + Player.Instance.money;
    }
}
