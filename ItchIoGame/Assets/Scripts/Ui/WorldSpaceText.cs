using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WorldSpaceText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float alpha = 1;
    private Vector3 worldPoint;
    private float index = 0;
    private Vector3 delta = new Vector3(0,0,0);
    private Camera cam;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (text.color.a < 0)
        {
            Destroy(this.gameObject);
        }

        Vector3 pos = cam.WorldToScreenPoint(worldPoint);
        alpha -= Time.deltaTime * 0.9f;
        delta += Vector3.up*Time.deltaTime*80;
        this.transform.position = new Vector3(pos.x + delta.x, pos.y+delta.y + index, 0);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

    }


    public void ShowNew(Camera camera,string text, Vector3 worldPoint, Color color,int index)
    {
        Vector3 position = camera.WorldToScreenPoint(worldPoint);
        WorldSpaceText inst = Instantiate(this);
        inst.cam = camera;
        inst.worldPoint = worldPoint;
        inst.transform.SetParent(UI.Instance.transform,false);
        inst.text = inst.GetComponent<TextMeshProUGUI>();
        RectTransform trans = inst.GetComponent<RectTransform>();
        inst.index = trans.rect.height / 2 - index * trans.rect.height;
        inst.transform.position = new Vector3(position.x, position.y + trans.rect.height / 2 - index* trans.rect.height, 0);
        inst.text.color = color;
        inst.text.SetText(text);

    }

}
