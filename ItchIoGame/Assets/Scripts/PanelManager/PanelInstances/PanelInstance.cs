using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelInstance : MonoBehaviour
{
    public PanelName name;
    public PanelComponents components;
    protected PanelComponents instanciatedComponent;
    protected GameObject instance;
    private static Dictionary<PanelName, PanelInstance> prefabs = new Dictionary<PanelName, PanelInstance>();

    protected virtual PanelInstance OnCreate()
    {
        instance = Instantiate(components.gameObject);
        instanciatedComponent = GetComponentInObject();
        instance.transform.SetParent(this.transform);
        instance.transform.localPosition = new Vector3(0,0,0);
        return this;
    }

    public void Start()
    {
        Debug.Log("HERE: " + this.gameObject.name);
        prefabs.Add(name,this);
    }

    public static T GetInstance<T>(PanelName name) where T : PanelInstance
    {
        return (T)prefabs[name];
    }

    public static T CreateInstance<T>(PanelName name) where T : PanelInstance
    {
        Debug.Log(name);
        return (T)prefabs[name].OnCreate();
    }

    protected virtual PanelComponents GetComponentInObject()
    {
        return instance.GetComponent<PanelComponents>();
    }

}
