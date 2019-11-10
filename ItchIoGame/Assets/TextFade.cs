using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float a = 1;

    void Start()
    {
        this.text = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        this.text.rectTransform.position = new Vector3(this.text.rectTransform.position.x, this.text.rectTransform.position.y + 0.25f, this.text.rectTransform.position.z);
        this.text.color = new Color(this.text.color.r, this.text.color.g, this.text.color.b, a);
        a -= 0.005f;

        if(a <= 0)
        {
            Destroy(this.text.gameObject);
        }
    }
}
