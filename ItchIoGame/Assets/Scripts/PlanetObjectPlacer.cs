using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetObjectPlacer : IClickHandler
{
    [SerializeField] private Camera mainCamera;
    public PlanetEntity holding;
    private Planet instance;
    public GameObject radiusSpherePrefab;
    private GameObject radiusSphereInstance;
    private PlanetGenerator planetGenerator;
    private SphereCollisionHandler sphereCollision;
    private bool onWater = false;
    private MaterialPropertyBlock property;
    [ColorUsage(true, true)]
    public Color onWaterColor,onLandColor;
    public void Start()
    {
        this.instance = this.GetComponent<Planet>();
        this.planetGenerator = this.GetComponent<PlanetGenerator>();
        this.property = new MaterialPropertyBlock();
    }

    public void Update()
    {
        if(this.holding == null)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButton(1)) {
            Destroy(this.holding.gameObject);
            Destroy(this.radiusSphereInstance);
            this.holding = null;
            return;
        }
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 8)))
        {
            if (hit.transform.gameObject.tag.Equals("PlanetSide") && hit.transform.parent.gameObject.Equals(this.gameObject))
            {
                this.holding.RotateTowardsPlanet(this.instance);
                this.radiusSphereInstance.transform.rotation = this.holding.transform.rotation;
                Vector3 point = hit.point + this.holding.transform.up * holding.colliderHeight * 0.5f* holding.transform.localScale.y;
                Debug.Log(Vector3.Distance(point, this.transform.position) + " | " + planetGenerator.getSettings().planetRadius);
                float min = planetGenerator.getMinMax().Min;
                float max = planetGenerator.getMinMax().Max;
                float waterRange = min + 5 / 256f;
                if (Vector3.Distance(point, this.transform.position) <= waterRange || sphereCollision.isColliding())
                {
                    Debug.Log("on water");
                    if (!onWater)
                    {
                        property.SetColor("_color", onWaterColor);
                        this.radiusSphereInstance.GetComponent<MeshRenderer>().SetPropertyBlock(property);
                    }
                    onWater = true;

                }
                else {

                    if (onWater)
                    {
                        property.SetColor("_color", onLandColor);
                        this.radiusSphereInstance.GetComponent<MeshRenderer>().SetPropertyBlock(property);
                    }

                    onWater = false;
                    
                }
                this.holding.transform.position = point;
                this.radiusSphereInstance.transform.position = this.holding.transform.position;
                if (Input.GetMouseButton(0) && !onWater)
                {
                    this.holding.PlaceOnPlanet(instance, point);
                    this.holding = null;
                    Destroy(this.radiusSphereInstance);
                }
            }
        }
        else
        {
            this.holding.transform.position = Vector3.zero;
            if(Input.GetMouseButton(0))
            {
                Destroy(this.holding);
                this.holding = null;
            }
        }
    }

    public void HoldPlanetEntity(PlanetEntity planetEntity)
    {
        this.holding = Instantiate(planetEntity);
        holding.OnPlace();
        this.holding.transform.position = Vector3.zero;
        //this.holding.transform.localScale = Vector3.one;
        this.holding.transform.rotation = Quaternion.identity;
        this.holding.transform.SetParent(this.transform);
        
        this.radiusSphereInstance = Instantiate(this.radiusSpherePrefab);
        sphereCollision = radiusSphereInstance.GetComponent<SphereCollisionHandler>();
        sphereCollision.setHolding(this.holding.gameObject);
        this.radiusSphereInstance.transform.position = Vector3.zero;
        this.radiusSphereInstance.transform.rotation = Quaternion.identity;
        this.radiusSphereInstance.transform.localScale = new Vector3(planetEntity.sphereRadius, 1, planetEntity.sphereRadius);

        this.radiusSphereInstance.transform.SetParent(this.transform);
        
    }

    public override void OnClick(ButtonController button)
    {
        if(this.holding != null)
        {
            Destroy(this.holding);
        }

        if (button.getName().Equals("BuyTree")) {
            HoldPlanetEntity(PrefabData.GetEntity("Tree"));
        }
        else if (button.getName().Equals("BuyFactory"))
        {
            HoldPlanetEntity(PrefabData.GetEntity("Factory"));
        }
        else if (button.getName().Equals("BuyLivingEntity"))
        {
            HoldPlanetEntity(PrefabData.GetEntity("LivingEntity"));
        }
    }
}
