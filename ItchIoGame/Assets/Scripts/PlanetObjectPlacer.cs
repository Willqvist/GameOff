using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetObjectPlacer : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public PlanetEntity holding;
    private Planet instance;

    public GameObject radiusSpherePrefab;
    private GameObject radiusSphereInstance;

    public void Start()
    {
        this.instance = this.GetComponent<Planet>();
    }

    public void Update()
    {
        if(this.holding == null)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 8)))
        {
            if (hit.transform.gameObject.Equals(this.gameObject))
            {
                this.holding.transform.position = hit.point;
                Debug.Log(hit.point);
                this.holding.RotateTowardsPlanet(this.instance);
                this.radiusSphereInstance.transform.position = this.holding.transform.position;

                if (Input.GetMouseButton(0))
                {
                    this.holding.PlaceOnPlanet(this.GetComponent<Planet>());
                    this.holding = null;
                    Destroy(this.radiusSphereInstance);
                }
            }
        }
    }

    public void HoldPlanetEntity(PlanetEntity planetEntity)
    {
        this.holding = Instantiate(planetEntity);
        this.holding.transform.position = Vector3.zero;
        this.holding.transform.localScale = Vector3.one;
        this.holding.transform.rotation = Quaternion.identity;
        this.holding.transform.SetParent(this.transform);

        this.radiusSphereInstance = Instantiate(this.radiusSpherePrefab);
        this.radiusSphereInstance.transform.position = Vector3.zero;
        this.radiusSphereInstance.transform.rotation = Quaternion.identity;
        this.radiusSphereInstance.transform.SetParent(this.transform);
    }
}
