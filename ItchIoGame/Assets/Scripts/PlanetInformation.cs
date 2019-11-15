using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetInformation : MonoBehaviour
{
    public TextMeshProUGUI population;
    public TextMeshProUGUI pollution;
    public TextMeshProUGUI electricity;
    public TextMeshProUGUI happiness;
    private int pollutionMultiplier = 10;

    private void Update()
    {
        this.population.text = $"{Player.Instance.Planet.activeWorkers} / {Player.Instance.Planet.population}";

        if (Player.Instance.Planet.pollution >= 0 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#00FF00>Calm</color>";
        }

        if (Player.Instance.Planet.pollution > 5 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#ffd500>Mild</color>";
        }

        if (Player.Instance.Planet.pollution > 50 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#FF0000>Dangerous</color>";
        }

        if (Player.Instance.Planet.pollution > 150 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#b3001d>Severe</color>";
        }

        this.electricity.text = Player.Instance.Planet.electricity + " /s";
        this.happiness.text = Player.Instance.Planet.Happiness + " %";
    }
}
