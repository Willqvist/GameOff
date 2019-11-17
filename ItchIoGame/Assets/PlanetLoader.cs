using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLoader : MonoBehaviour
{
    [SerializeField] private List<Planet> planets;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private PlanetCamera planetCamera;
    [SerializeField] private int renderDist;
    [SerializeField] private Planet planetPrefab;
    [SerializeField] private GameObject planetPrefabHolder;
    [SerializeField] private int heightSeed;
    [SerializeField] private int spreadSeed;
    private Planet currentPlanet;
    private void Start()
    {
        Player.Instance.LoadPlanet(planets[0]);
        planets[0].Initalize(new Vector3(0,0,0));
    }

    private void Update()
    {
        if (currentPlanet == null || currentPlanet.Id != Player.Instance.Planet.Id)
        {
            LoadSurroundingPlanets(Player.Instance.Planet);
            currentPlanet = Player.Instance.Planet;
        }
        
    }

    private bool IsGridOccupied(Vector2 grid)
    {
        for (int i = 0; i < planets.Count; i++)
        {
            Vector2 pos = ToGrid(planets[i].transform.position);
            if (pos.Equals(grid))
                return true;
        }
        return false;
    }

    private void LoadSurroundingPlanets(Planet newPlanet)
    {
        Vector2 grid = ToGrid(newPlanet.transform.position);
        Debug.Log(grid);
        for (int i = -renderDist; i < renderDist; i++)
        {
            for (int j = -renderDist; j < renderDist; j++)
            {
                Vector2 newGrid = grid + new Vector2(i, j);
                Debug.Log(newGrid);
                if (!IsGridOccupied(newGrid))
                {
                    NewPlanet(newGrid);
                }
            }
        }
    }

    private Vector3 ToWorldPosition(Vector2 grid)
    {
        Vector2 gridPos = grid * gridSize + new Vector2(gridSize.x*0.5f,gridSize.y*0.5f);
        float angle = Mathf.PerlinNoise((grid.x + heightSeed) * 0.6f, (grid.y + heightSeed) * 0.6f)*Mathf.PI;
        return new Vector3(gridPos.x+Mathf.Cos(angle)*gridSize.x/4, Mathf.PerlinNoise((grid.x+heightSeed)*0.6f, (grid.y + heightSeed) * 0.6f) *100, gridPos.y + Mathf.Sin(angle) * gridSize.y / 4);
    }

    private void NewPlanet(Vector2 grid)
    {
        GameObject holder = Instantiate(planetPrefabHolder);
        Planet planet = Instantiate(planetPrefab);
        holder.transform.position = ToWorldPosition(grid);
        holder.GetComponent<PlanetHolder>().planet = planet;
        holder.GetComponent<PlanetHolder>().planetCamera = planetCamera;
        planet.gameObject.SetActive(false);
    }

    private Vector2 ToGrid(Vector3 position)
    {
        return new Vector2(Mathf.Floor(position.x / gridSize.x), Mathf.Floor(position.z / gridSize.y));
    }

}
