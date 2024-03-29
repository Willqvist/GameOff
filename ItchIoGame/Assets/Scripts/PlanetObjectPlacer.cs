﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlanetObjectPlacer : IPointerClickHandler
{
    [SerializeField] public Camera mainCamera;
    public PlanetEntity holding;
    private Planet instance;
    public GameObject radiusSpherePrefab;
    private GameObject radiusSphereInstance;
    private PlanetGenerator planetGenerator;
    private SphereCollisionHandler sphereCollision;
    private bool onWater = false;
    private ObjectPlacerListener listener;
    private MaterialPropertyBlock property;
    [ColorUsage(true, true)]
    public Color onWaterColor,onLandColor;
    private UnityAction onPlace;
    public void Start()
    {
        this.instance = this.GetComponent<Planet>();
        this.planetGenerator = this.GetComponent<PlanetGenerator>();
        this.property = new MaterialPropertyBlock();
    }
    private void OnSignalDestroy()
    {
        property.SetColor("_color", onWaterColor);
        Destroy(this.holding.gameObject);
        Destroy(this.radiusSphereInstance);
        this.holding = null;
    }
    private void DestroyHolding()
    {
        this.listener.SignalOnCancel();
    }
    public void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (this.holding == null)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyUp(KeyCode.Escape)) {
            DestroyHolding();
            return;
        }
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 8)))
        {
            bool ignorewaterCheck = false;
            if (hit.transform.gameObject.tag.Equals("PlanetSide") && hit.transform.parent.gameObject.Equals(this.gameObject))
            {
                if(this.holding.GetComponent<Mine>() != null)
                {
                    Collider[] hits = Physics.OverlapSphere(hit.point, 0.1f);
                    foreach(var h in hits)
                    {
                        if(h.transform.CompareTag("Vespene"))
                        {
                            //Fulhack
                            property.SetColor("_color", onLandColor);
                            this.radiusSphereInstance.GetComponent<MeshRenderer>().SetPropertyBlock(property);
                            this.holding.transform.position = h.transform.position;
                            this.holding.RotateTowardsPlanet(this.instance);
                            this.radiusSphereInstance.transform.position = this.holding.transform.position;
                            this.radiusSphereInstance.transform.rotation = this.holding.transform.rotation;

                            if (Input.GetMouseButton(0))
                            {
                                Vector3 p = hit.point + this.holding.transform.up * holding.colliderHeight * 0.5f;
                                this.holding.PlaceOnPlanet(instance, this.holding.transform.position);
                                Destroy(h.gameObject);
                                UI.Instance.ShowWorldText("+5 xp", p, new Color(238 / 256f, 130 / 256f, 238 / 256f));
                                UI.Instance.ShowWorldText("-" + this.holding.entityData.cost + " money", p, Color.red, 1);
                                this.holding = null;
                                this.listener.SignalOnPlace();
                                Destroy(this.radiusSphereInstance);
                                this.listener.SignalOnPlace();
                                this.onPlace?.Invoke();
                                return;
                            }

                            return;
                        }
                        else
                        {
                            property.SetColor("_color", onWaterColor);
                            this.radiusSphereInstance.GetComponent<MeshRenderer>().SetPropertyBlock(property);
                        }
                    }

                    ignorewaterCheck = true;
                    this.holding.transform.position = hit.point + this.holding.transform.up * holding.colliderHeight * 0.5f;
                    this.radiusSphereInstance.transform.position = this.holding.transform.position;
                    this.holding.RotateTowardsPlanet(this.instance);
                    this.radiusSphereInstance.transform.rotation = this.holding.transform.rotation;
                    return;
                }

                this.holding.RotateTowardsPlanet(this.instance);
                this.radiusSphereInstance.transform.rotation = this.holding.transform.rotation;
                Vector3 point = hit.point + this.holding.transform.up * holding.colliderHeight * 0.5f;

                if(!ignorewaterCheck)
                {
                    float min = planetGenerator.getMinMax().Min;
                    float max = planetGenerator.getMinMax().Max;
                    float waterRange = min + 5 / 256f;
                    if (Vector3.Distance(point, this.transform.position) <= waterRange || sphereCollision.isColliding())
                    {
                        if (!onWater)
                        {
                            property.SetColor("_color", onWaterColor);
                            this.radiusSphereInstance.GetComponent<MeshRenderer>().SetPropertyBlock(property);
                        }
                        onWater = true;

                    }
                    else
                    {

                        if (onWater)
                        {
                            property.SetColor("_color", onLandColor);
                            this.radiusSphereInstance.GetComponent<MeshRenderer>().SetPropertyBlock(property);
                        }

                        onWater = false;

                    }
                }
                this.holding.transform.position = point;
                this.radiusSphereInstance.transform.position = this.holding.transform.position;
                if (Input.GetMouseButton(0) && !onWater)
                {
                    this.holding.PlaceOnPlanet(instance, point);
                    UI.Instance.ShowWorldText("+5 xp",point, new Color(238/256f, 130/256f, 238/256f));
                    UI.Instance.ShowWorldText("-" + this.holding.entityData.cost + " money", point, Color.red,1);
                    this.holding = null;
                    this.listener.SignalOnPlace();
                    Destroy(this.radiusSphereInstance);
                    this.listener.SignalOnPlace();
                    this.onPlace?.Invoke();
                }
            }
        }
        else
        {
            this.holding.transform.position = Vector3.zero;
            if(Input.GetMouseButton(0))
            {
                DestroyHolding();
            }
        }
    }

    public void HoldPlanetEntity(PlanetEntity planetEntity)
    {

        this.holding = Instantiate(planetEntity);
        this.holding.transform.position = Vector3.zero;
        //this.holding.transform.localScale = Vector3.one;
        this.holding.transform.rotation = Quaternion.identity;
        this.holding.transform.SetParent(this.transform);

        property.SetColor("_color", onWaterColor);
        this.radiusSphereInstance = Instantiate(this.radiusSpherePrefab);
        sphereCollision = radiusSphereInstance.GetComponent<SphereCollisionHandler>();
        sphereCollision.setHolding(this.holding.gameObject);
        this.radiusSphereInstance.transform.position = Vector3.zero;
        this.radiusSphereInstance.transform.rotation = Quaternion.identity;
        this.radiusSphereInstance.transform.localScale = new Vector3(planetEntity.sphereRadius, 1, planetEntity.sphereRadius);
        this.radiusSphereInstance.GetComponent<MeshRenderer>().SetPropertyBlock(property);
        this.radiusSphereInstance.transform.SetParent(this.transform);
        
    }

    public void PlaceObject(ObjectPlacerListener listener,EntityName entity, UnityAction onPlace)
    {
        this.listener = listener;
        this.listener.OnCancelListener(OnSignalDestroy);
        PlanetEntity e = PrefabData.Instance.GetEntity(entity);
        HoldPlanetEntity(e);
        this.onPlace = onPlace;
    }

    public override void OnClick(ButtonController button)
    {
        if(this.holding != null)
        {
            DestroyHolding();
        }

        /*if (button.getName().Equals("BuyTree")) {
            HoldPlanetEntity(PrefabData.GetEntity("Tree"));
        }
        else if (button.getName().Equals("BuyFactory"))
        {
            HoldPlanetEntity(PrefabData.GetEntity("Factory"));
        }
        else if (button.getName().Equals("BuyLivingEntity"))
        {
            HoldPlanetEntity(PrefabData.GetEntity("LivingEntity"));
        }*/
    }
}