using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float minScroll;
    [SerializeField] private float maxScroll;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float roationSpeed;
    private Quaternion crs;
    //local shit
    private float zoom;

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            Debug.Log(Quaternion.AngleAxis(roationSpeed * Time.deltaTime, new Vector3(-v, h, 0)));
            this.pivot.transform.rotation *= Quaternion.AngleAxis(roationSpeed * Time.deltaTime, new Vector3(-v, h, 0));
        }
        this.zoom += Input.GetAxis("Mouse ScrollWheel") * 80f;
        this.zoom = Mathf.Lerp(this.zoom, 0, 0.05f);

        Vector3 target = this.transform.position + transform.forward * this.zoom * Time.deltaTime;
        float dist = Vector3.Distance(target, this.pivot.transform.position);

        if (dist < maxScroll)
        {
            this.transform.position = target;
        }
    }
}
