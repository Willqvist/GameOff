using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacerListener
{
    // Start is called before the first frame update
    public delegate void OnEvent();

    private event OnEvent cancelEvent;
    private event OnEvent placeEvent;

    private static bool isWorking = false;
    public static ObjectPlacerListener create() {
        if (isWorking)
            return null;
        isWorking = true;
        return new ObjectPlacerListener();
    }

    public void OnCancelListener(OnEvent e){
        cancelEvent += e;
    }

    public void OnPlaceListener(OnEvent e)
    {
        placeEvent += e;
    }

    public void SignalOnPlace()
    {
        if(placeEvent != null)
        placeEvent();
    }

    public void SignalOnCancel()
    {
        cancelEvent();
    }

    public static void clear()
    {
        isWorking = false;
    }
    public static bool IsWorking()
    {
        return isWorking;
    }

}
