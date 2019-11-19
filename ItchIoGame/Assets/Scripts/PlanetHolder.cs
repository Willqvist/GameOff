using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHolder : MonoBehaviour
{

    public Planet planet;
    public PlanetCamera planetCamera;
    private MaterialPropertyBlock property;
    private MeshRenderer renderer;
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
            }
            else
            {
                UI.Instance.ClearLines();
                property.SetColor("_BaseColor", Color.red);
            }
            renderer.SetPropertyBlock(property);
        }
    }

    private void OnMouseExit()
    {
        UI.Instance.ClearLines();
        if (UI.Instance.planetCamera.GetState() == CameraState.TELEPORT)
        {
            property.SetColor("_BaseColor", Color.green);
            renderer.SetPropertyBlock(property);
        }
    }

    private void OnMouseDown()
    {
        if (UI.Instance.planetCamera.GetState() == CameraState.TELEPORT)
        {
            //TODO: create teleport between current planet and this.planet.
        }
        if (UI.Instance.planetCamera.GetState() != CameraState.PLANET) return;
        planet.planetCamera = planetCamera;
        planet.gameObject.SetActive(true);
        planet.Initalize(this.transform.position, PlanetLoader.GetType(Random.value > 0.5 ? PlanetName.Normal : PlanetName.Lava));
        planetCamera.PivotTranslate(this.transform.position);
        Player.Instance.LoadPlanet(planet);
        Destroy(this.gameObject);
        planet.gameObject.SetActive(true);
    }
}
