using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetObjectPlacer : IClickHandler
{
    [SerializeField] private Camera mainCamera;
    public PlanetEntity holding;
    private Planet instance;
    private PlanetEntity holdingEntity;
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
            if (hit.transform.gameObject.tag.Equals("PlanetSide") && hit.transform.parent.gameObject.Equals(this.gameObject))
            {
                this.holding.RotateTowardsPlanet(this.instance);
                Vector3 point = hit.point + this.holding.transform.up * holdingEntity.colliderHeight * 0.5f;
                this.holding.transform.position = point;
                this.radiusSphereInstance.transform.position = this.holding.transform.position;

                if (Input.GetMouseButton(0))
                {
                    this.holding.PlaceOnPlanet(instance, point);
                    this.holding = null;
                    Destroy(this.radiusSphereInstance);
                }
            }
        }
    }

    public void HoldPlanetEntity(PlanetEntity planetEntity)
    {
        holdingEntity = planetEntity;
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

    public override void OnClick(ButtonController button)
    {
        if (button.getName().Equals("BuyTree")) {
            HoldPlanetEntity(PrefabData.GetEntity("Tree"));
        }
        else if (button.getName().Equals("BuyFactory"))
        {
            HoldPlanetEntity(PrefabData.GetEntity("Factory"));
        }
    }
}
