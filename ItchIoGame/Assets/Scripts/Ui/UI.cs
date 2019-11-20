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
    //[SerializeField] private LineRenderer teleportLineRenderer;
    [SerializeField] private GameObject teleportPrefab;
    public void Awake()
    {
        Instance = this;
        //teleportLineRenderer.positionCount += 1;
        //teleportLineRenderer.SetPosition(0, new Vector3(0,0,0));
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
    public void DrawTeleportLine(Vector3 p1, Vector3 p2) {

        float distance = Vector3.Distance(p1,p2)/2;
        GameObject teleport = Instantiate(teleportPrefab);
        teleport.transform.localScale = new Vector3(1, distance, 1);
        teleport.transform.position = p1 + distance*Vector3.Normalize(p2 - p1);
        teleport.transform.up = p2 - p1;//(p2,Vector3.up);

        //teleportLineRenderer.positionCount += 1;
        //teleportLineRenderer.SetPosition(teleportLineRenderer.positionCount-1, p2);
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
