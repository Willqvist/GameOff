using System;
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
    private Quaternion crs,translateRot;
    private Vector3 translatePivot;
    private bool atPivot = true, atRotPivtort = true;
    //local shit
    private float zoom;
    private bool rotate = true;
    private CameraState state = CameraState.PLANET;
    void Update()
    {
        if (!atPivot)
        {
            pivot.transform.position = Vector3.Lerp(pivot.transform.position, translatePivot, 0.05f);
            if (Vector3.Distance(pivot.transform.position, translatePivot) < 0.02f)
            {
                pivot.transform.position = translatePivot;
                atPivot = true;
            }
        }

        if (!atRotPivtort)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, translateRot, 0.05f);
            if (Vector3.Distance(pivot.transform.rotation.eulerAngles, translateRot.eulerAngles) < 0.02f)
            {
                pivot.transform.rotation = translateRot;
                atRotPivtort = true;
            }
        }
        if (state == CameraState.PLANET)
        {
            if (currentPlanet == null || !currentPlanet.Loaded()) return;
            float minDist = currentPlanet.GetRadius() + minScroll - 5;
            float dist = Vector3.Distance(this.transform.position, this.pivot.transform.position);
            float delta = (dist - minDist) / (maxScroll - minDist) + 0.1f;
            if (Input.GetMouseButton(1) && rotate)
            {
                atRotPivtort = true;
                float h = Input.GetAxis("Mouse X");
                float v = Input.GetAxis("Mouse Y");
                this.pivot.transform.rotation *= Quaternion.AngleAxis(rotationSpeed * delta * Time.deltaTime, new Vector3(-v, h, 0));
            }
            if (EventSystem.current.IsPointerOverGameObject()) return;
            this.zoom += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * delta;
            this.zoom = Mathf.Lerp(this.zoom, 0, 0.05f);
            Vector3 target = this.transform.position + transform.forward * this.zoom * Time.deltaTime;
            dist = Vector3.Distance(target, this.pivot.transform.position);
            if (dist > currentPlanet.GetRadius() + minScroll + currentPlanet.getMinMax().Max && dist < maxScroll)
            {
                this.transform.position = target;
            }
        }
        else if (state == CameraState.TELEPORT)
        {

        }
    }

    public CameraState GetState()
    {
        return state;
    }

    public void SetState(CameraState state)
    {
        this.state = state;
    }

    public void DisableRotation(bool v)
    {
        rotate = !v;
    }

    public void PivotTranslate(Vector3 position)
    {
        translatePivot = position;
        atPivot = false;
    }

    public void PivotTranslateRotation(Quaternion rot)
    {
        translateRot = rot;
        atRotPivtort = false;
    }

}
