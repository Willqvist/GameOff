using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    [SerializeField] private WorldSpaceText worldSpaceText;
    [SerializeField] private Camera cam;
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

}
