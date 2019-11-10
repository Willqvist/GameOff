using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorLogController : MonoBehaviour
{
    public GameObject prefab;

    public static ErrorLogController Instance { get; private set; }

    private int count;
    private float tick;

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if(count <= 0)
        {
            return;
        }

        tick += Time.deltaTime;

        if(tick >= 2)
        {
            count = 0;
        }
    }

    public void LogError(string error)
    {
        GameObject go = Instantiate(this.prefab);
        go.transform.SetParent(this.transform, false);
        go.GetComponent<RectTransform>().localPosition = new Vector3(0, 100 * count, 0);
        go.GetComponent<RectTransform>().localScale = Vector3.one;
        go.GetComponent<TextMeshProUGUI>().text = error;

        count++;
    }
}
