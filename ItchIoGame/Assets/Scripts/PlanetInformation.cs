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
    public TextMeshProUGUI title;
    private int pollutionMultiplier = 10;

    private static PlanetInformation info;

    public void Awake()
    {
        info = this;
    }

    public static void updateText() {
        info.updateNewPlanet();
    }

    private void updateNewPlanet()
    {
        this.title.text = Player.Instance.Planet.title;
        this.population.text = $"{Player.Instance.Planet.activeWorkers} / {Player.Instance.Planet.population}";
        string s = "Calm";
        int i = 0;
        string c = "#00FF00";

        if (Player.Instance.Planet.pollution >= 0 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#00FF00>Calm</color>";
            s = "Calm";
            c = "#00FF00";
        }

        if (Player.Instance.Planet.pollution > 5 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#ffd500>Mild</color>";
            s = "Mild";
            c = "#ffd500";
        }

        if (Player.Instance.Planet.pollution > 50 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#FF0000>Dangerous</color>";
            s = "Dangerous";
            c = "#FF0000";
        }

        if (Player.Instance.Planet.pollution > 150 * pollutionMultiplier)
        {
            this.pollution.text = "<color=#b3001d>Severe</color>";
            s = "Severe";
            c = "#b3001d";
        }

        this.pollution.text = $"<color={c}>{Player.Instance.Planet.pollution} - {s}</color>";

        this.electricity.text = Player.Instance.Planet.electricity + " /s";
        this.happiness.text = (Player.Instance.Planet.happiness >= 0 ? "<color=#00FF00>+" : "<color=#FF0000>") + Player.Instance.Planet.happiness + "</color>";
    }
}
