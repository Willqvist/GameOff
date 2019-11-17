using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    public ShopItemData ShopItem { get; set; }

    [SerializeField] private WorldSpaceText worldSpaceText;
    [SerializeField] private Camera cam;
    public PlanetCamera planetCamera;
    public GameObject area;
    [SerializeField] private LineRenderer lineRenderer;
    public void Awake()
    {
        Instance = this;
    }


    public void ShowWorldText(string text, Vector3 position, Color color)
    {
        worldSpaceText.ShowNew(cam, text,position,color,0);
    }

    public void ShowWorldText(string text, Vector3 position, Color color,int index)
    {
        worldSpaceText.ShowNew(cam,text, position, color,index);
    }

    public void ShowArea(Vector3 pos,float radius)
    {
        area.transform.localScale = new Vector3(radius, 0.1f, radius);
        area.transform.position = pos;
        area.SetActive(true);

    }

    public void DrawLine(Vector3 p1, Vector3 p2)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, p1);
        lineRenderer.SetPosition(1, p2);
    }

    public void ClearLines()
    {
        lineRenderer.positionCount = 0;
    }

    public void HideArea()
    {
        area.SetActive(false);
    }

}
