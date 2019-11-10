using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlanetCamera : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private PlanetGenerator currentPlanet;
    [SerializeField] private float minScroll;
    [SerializeField] private float maxScroll;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float rotationSpeed;
    private Quaternion crs;
    //local shit
    private float zoom;

    void Update()
    {
        if (currentPlanet == null) return;
        float minDist = currentPlanet.getSettings().planetRadius + minScroll-5;
        float dist = Vector3.Distance(this.transform.position, this.pivot.transform.position);
        float delta = (dist - minDist) / (maxScroll - minDist) + 0.1f;
        if (Input.GetMouseButton(1))
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            this.pivot.transform.rotation *= Quaternion.AngleAxis(rotationSpeed *delta* Time.deltaTime, new Vector3(-v, h, 0));
        }
        if (EventSystem.current.IsPointerOverGameObject()) return;
        this.zoom += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed*delta;
        this.zoom = Mathf.Lerp(this.zoom, 0, 0.05f);
        Vector3 target = this.transform.position + transform.forward * this.zoom * Time.deltaTime;
        dist = Vector3.Distance(target, this.pivot.transform.position);
        if (dist > currentPlanet.getSettings().planetRadius+minScroll+currentPlanet.getMinMax().Max && dist < maxScroll)
        {
            this.transform.position = target;
        }
    }
}
