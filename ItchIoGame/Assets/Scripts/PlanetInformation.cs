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
        string s = "Calm";
        int i = 0;
        string c = "#00FF00";

        if (Player.Instance.Planet.pollution >= 0 * pollutionMultiplier)
        {
            s = "Calm";
            c = "#00FF00";
        }

        if (Player.Instance.Planet.pollution > 5 * pollutionMultiplier)
        {
            s = "Mild";
            c = "#ffd500";
        }

        if (Player.Instance.Planet.pollution > 50 * pollutionMultiplier)
        {
            s = "Dangerous";
            c = "#FF0000";
        }

        if (Player.Instance.Planet.pollution > 150 * pollutionMultiplier)
        {
            s = "Severe";
            c = "#b3001d";
        }

        this.pollution.text = $"<color={c}>{Player.Instance.Planet.pollution} - {s}</color>";

        this.electricity.text = Player.Instance.Planet.electricity + " /s";
        this.happiness.text = (Player.Instance.Planet.happiness >= 0 ? "<color=#00FF00>+" : "<color=#FF0000>") + Player.Instance.Planet.happiness + "</color>";
    }
}
