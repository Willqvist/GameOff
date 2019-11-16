using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelInstance : MonoBehaviour
{
    public PanelName name;
    [SerializeField]private PanelComponents components;
    protected PanelComponents instanciatedComponent;
    public GameObject instance;
    protected bool instanciated = false;
    private static Dictionary<PanelName, PanelInstance> prefabs = new Dictionary<PanelName, PanelInstance>();

    protected PanelInstance OnCreate()
    {
        instanciated = true;
        instance.SetActive(true);
        //instance = Instantiate(components.gameObject);
        instanciatedComponent = GetComponentInObject();
        //instance.transform.SetParent(this.transform,false);
        //instance.transform.localPosition = new Vector3(0,0,0);
        return this;
    }

    public void Start()
    {
        Debug.Log("HERE: " + this.gameObject.name);
        prefabs.Add(name,this);
    }

    public static T GetInstance<T>(PanelName name) where T : PanelInstance
    {
        PanelInstance i = prefabs[name];
        if (!i.instanciated)
        {
            i.OnCreate();
        }
        return (T)i;
    }

    public static T CreateInstance<T>(PanelName name) where T : PanelInstance
    {
        return (T)prefabs[name].OnCreate();
    }

    protected virtual PanelComponents GetComponentInObject()
    {
        return instance.GetComponent<PanelComponents>();
    }

}
