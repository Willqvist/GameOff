using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polluter : MonoBehaviour
{
    public float pollutionPerSecond;

    public void Start()
    {
        StartCoroutine(Tick());
    }

    public IEnumerator Tick()
    {
        Planet.Current.Pollute(pollutionPerSecond);
        yield return new WaitForSeconds(1);
        StartCoroutine(Tick());
    }
}
