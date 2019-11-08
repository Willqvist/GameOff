using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float radius;

    public void Place(Transform body)
    {
        Vector3 dir = (body.position - this.transform.position).normalized;
        body.rotation = Quaternion.FromToRotation(body.up, dir) * body.rotation;
        body.position = (dir * radius) + this.transform.position;
    }

    public void Build(PlanetEntity entity)
    {

    }
}
