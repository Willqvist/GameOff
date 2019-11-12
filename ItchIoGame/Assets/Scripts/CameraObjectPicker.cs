using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraObjectPicker : MonoBehaviour
{

    private RaycastHit hit;
    [SerializeField] private ShowObjectInfo info;
    private Ray ray;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.mainCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 8) | (1<<8)))
        {
            if (hit.transform.gameObject.layer == 8)
            {
                //info.ShowEntity(hit.transform.gameObject.GetComponent<PlanetEntity>().entityData);
                PanelManager.Get<PanelObjectInfo>().Show(hit.transform.gameObject.GetComponent<PlanetEntity>().entityData);
            }
        }
    }
}
