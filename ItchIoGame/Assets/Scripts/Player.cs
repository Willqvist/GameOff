using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private float money;
    private Planet planet;

    public void Start()
    {
        Instance = this;
    }
}
