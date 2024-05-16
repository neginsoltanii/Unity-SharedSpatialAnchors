using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WPM;

public class ColorizeCountriesScript : MonoBehaviour
{
    public DataManager dataManager;
    public WorldMapGlobe map;

    void Start()
    {
        map = WorldMapGlobe.instance;
        //map.ToggleCountrySurface("Brazil", true, Color.green);
    }

    public void ColorizeCountries(int year)
    {
        List<DataFormatWorld> data = dataManager.GetDataForYear(year);
        if (data == null)
        {
            Debug.LogError("No data available for year " + year);
            return;
        }

        float minCO2 = float.MaxValue;
        float maxCO2 = float.MinValue;

        foreach (DataFormatWorld entry in data)
        {
            if (entry.co2emissions < minCO2) minCO2 = entry.co2emissions;
            if (entry.co2emissions > maxCO2) maxCO2 = entry.co2emissions;
        }

        foreach (DataFormatWorld entry in data)
        {
            Color color = CalculateColor(entry.co2emissions, minCO2, maxCO2);
            //Applying the appropriate color to the contry
            map.ToggleCountrySurface(entry.countryName, true, color);
        }
    }

    private Color CalculateColor(float value, float min, float max)
    {
        float normalized = (value - min) / (max - min);
        return Color.Lerp(Color.green, Color.red, normalized);
    }
}