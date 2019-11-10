using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    public ShapeGenerator shapeGenerator;
    public NoiseSettings noiseSettings;
    [SerializeField, HideInInspector]
    private MeshFilter[] meshFilters;
    private MeshCollider[] meshColliders;
    private PlanetFace[] planetFaces;

    private ColorGenerator colorGenerator;

    private void OnValidate()
    {
        //GeneratePlanet();
    }
    public NoiseSettings getNoiseSettings() {
        return noiseSettings;
    }
    private void Start()
    {
        shapeGenerator = new ShapeGenerator(noiseSettings, shapeSettings);
        colorGenerator = new ColorGenerator(colorSettings);
        meshFilters = new MeshFilter[6];
        meshColliders = new MeshCollider[6];

        planetFaces = new PlanetFace[6];
        GeneratePlanet();
    }

    public ShapeSettings getSettings() {
        return shapeSettings;
    }

    public PlanetGenerator() {
        noiseSettings = new NoiseSettings();
        noiseSettings.roughness = 0.02f;
        noiseSettings.center = new Vector3(0, 0, 0);
    }

    public MinMax getMinMax() {
        return shapeGenerator.elevationMinMax;
    }

    void Initialize() {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Vector3[] directions = {
            Vector3.up,Vector3.down,Vector3.left,Vector3.right,Vector3.forward,Vector3.back
        };

        for (int i = 0; i < 6; i++) {
            GameObject meshObj = new GameObject("mesh-" + directions[i].ToString());
            meshObj.transform.parent = transform;
            meshObj.tag = "PlanetSide";
            meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            meshFilters[i] = meshObj.AddComponent<MeshFilter>();
            meshFilters[i].sharedMesh = new Mesh();
            meshColliders[i] = meshObj.AddComponent<MeshCollider>();
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
            planetFaces[i] = new PlanetFace(
                shapeGenerator,
                meshFilters[i].sharedMesh, 
                meshColliders[i],
                resolution, 
                directions[i]);
        }
    }

    public void GeneratePlanet() {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }


    void onColorSettingsUpdate()
    {
        Initialize();
        GenerateColors();
    }

    void onShapeSettingsUpdate()
    {
        Initialize();
        GenerateMesh();
    }
    void GenerateMesh() {
        foreach (PlanetFace face in planetFaces) {
            face.ConstructMesh();
        }

        colorGenerator.updateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors() {
        foreach (MeshFilter m in meshFilters) {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.planetColor;
        }
    }
}
