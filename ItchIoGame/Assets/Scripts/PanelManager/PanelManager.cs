using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager
{
    private static Dictionary<Type,Panel> panels = new Dictionary<Type,Panel>();

    public static T Get<T>() where T : Panel, new()
    {
        Type t = typeof(T);
        if (panels.ContainsKey(t))
        {
            return (T)panels[t];
        }
        panels[t] = new T();
        return (T)panels[t];
    }
    /*
    public static T GetOnce<T>(PanelName name) where T : Panel, new()
    {
        if (panels.ContainsKey(name))
        {
            return (T)panels[name];
        }
        T t = new T();
        panels[name] = t;
        return t;
    }
    */

}
