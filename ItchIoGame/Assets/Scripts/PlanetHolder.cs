using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHolder : MonoBehaviour
{

    public Planet planet;
    public PlanetCamera planetCamera;
    private MaterialPropertyBlock property;
    private MeshRenderer renderer;
    private static Color c = new Vector4(0.5331315f, 0.7924528f, 0.3999644f, 1f);
    private bool teleportable = false;
    private void Start()
    {
        this.property = new MaterialPropertyBlock();
        renderer = this.GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        if (UI.Instance.planetCamera.GetState() == CameraState.TELEPORT)
        {
            if (Vector3.Distance(this.transform.position, Player.Instance.Planet.transform.position) < ((TeleporterData)UI.Instance.ShopItem.entityData.externalData).radius)
            {
                UI.Instance.DrawLine(this.transform.position, Player.Instance.Planet.transform.position);
                property.SetColor("_BaseColor", Color.yellow);
                teleportable = true;
            }
            else
            {
                UI.Instance.ClearLines();
                property.SetColor("_BaseColor", Color.red);
                teleportable = false;
            }
            renderer.SetPropertyBlock(property);
        }
    }

    private void OnMouseExit()
    {
        UI.Instance.ClearLines();
        if (UI.Instance.planetCamera.GetState() == CameraState.TELEPORT)
        {
            teleportable = false;
            property.SetColor("_BaseColor", c);
            renderer.SetPropertyBlock(property);
        }
    }

    private void OnMouseDown()
    {
        if (UI.Instance.planetCamera.GetState() == CameraState.TELEPORT && teleportable)
        {
            this.planet.position = this.transform.position;
            Debug.Log("PLnaet pos: " + this.planet.position);
            UI.Instance.DrawTeleportLine(Player.Instance.Planet.position,this.planet.position);
            UI.Instance.ClearLines();
            Player.Instance.LoadPlanet(this.planet);
            this.planet.GetComponent<PlanetObjectPlacer>().mainCamera = planetCamera.GetComponent<Camera>();
            ShopItem.listener.SignalOnPlace();
            //TODO: create teleport between current planet and this.planet.
            planet.gameObject.SetActive(true);
            planet.Initalize(this.transform.position, PlanetLoader.GetType(Random.value > 0.5 ? PlanetName.Normal : PlanetName.Lava));
            planetCamera.PivotTranslate(this.transform.position);
            Player.Instance.LoadPlanet(planet);
            Destroy(this.gameObject);

        }
        if (UI.Instance.planetCamera.GetState() != CameraState.PLANET) return;
        planet.planetCamera = planetCamera;
    }
}
