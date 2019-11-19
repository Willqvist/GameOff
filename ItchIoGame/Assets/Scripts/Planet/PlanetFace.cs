using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFace
{
    private Planet context;
    private Mesh mesh;
    private MeshCollider collider;
    private int resolution;
    private ShapeGenerator shapeGenerator;
    private Vector3 localUp;
    private Vector3 axisA;
    private Vector3 axisB;
    private ResourceGenerator rg;
    private bool isTutorialWorld;
    // Start is called before the first frame update

    public PlanetFace(Planet planet, ResourceGenerator rg, ShapeGenerator shapeGenerator, Mesh mesh,MeshCollider collider, int resolution, Vector3 localUp, bool isTutorialWorld) {
        this.context = planet;
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        this.axisB = Vector3.Cross(localUp, axisA);
        this.collider = collider;
        this.rg = rg;
        this.isTutorialWorld = isTutorialWorld;
    }
    private Vector3 min = new Vector3(-0.1f, -0.1f, -0.1f);
    public void ConstructMesh() {
        Vector3[] vertices = new Vector3[resolution * resolution];

        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int[] uvs = new int[(resolution - 1) * (resolution - 1) * 4];
        int triIndex = 0;
        int uvIndex = 0;
        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                float noise = shapeGenerator.getNoise(pointOnUnitSphere);
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(noise,pointOnUnitSphere);

                if(!isTutorialWorld)
                {
                    if (rg.ShouldPlaceTree(vertices[i]) && noise > 0.001f && x % 3 == 0 && y % 3 == 0 && this.context.PType().planetName == PlanetName.Normal)
                    {
                        rg.PlaceTree(vertices[i] - min);
                    }
                    else if (rg.ShouldPlaceMine(vertices[i]) && noise > 0.001f && x % 7 == 0 && y % 7 == 0)
                    {
                        rg.PlaceMine(vertices[i]);
                    }
                }

                if (x != resolution - 1 && y != resolution - 1) {
                    triangles[triIndex] = i;
                    triangles[triIndex+1] = i+resolution+1;
                    triangles[triIndex+2] = i+resolution;

                    //uvs[uvIndex]

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                    uvIndex += 4;
                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        collider.sharedMesh = mesh;
        collider.convex = true;
    }

}
