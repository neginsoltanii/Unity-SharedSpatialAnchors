using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;

public class DataFormatWorld
{
    public string countryName;
    public float co2emissions;
}

public class DataManager : MonoBehaviour
{
    public Dictionary<int, List<DataFormatWorld>> dataPerYear;

    public TMP_Text dataCountryUI;
    public TMP_Text dataCo2emissionsUI;

    void Start()
    {
        ResetDataUI();
        LoadCSVFiles();
    }

    public void ResetDataUI()
    {
        dataCountryUI.text = "";
        dataCo2emissionsUI.text = "";
    }

    public void SetDataToUI(DataFormatWorld data)
    {
        dataCountryUI.text = data.countryName;
        dataCo2emissionsUI.text = data.co2emissions.ToString();
    }

    void LoadCSVFiles()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "co2Emission.csv");
        string result = System.IO.File.ReadAllText(filePath);

        List<Dictionary<string, object>> data = CSVReader.Read(result);
        OrganizeDataInYear(data);
    }

    void OrganizeDataInYear(List<Dictionary<string, object>> data)
    {
        string colnameCountry = "Country";
        string colnameYear = "Year";
        string colnameCO2 = "CO2EmissionRate (mt)";

        dataPerYear = new Dictionary<int, List<DataFormatWorld>>();

        foreach (Dictionary<string, object> row in data)
        {
            string rowCountry = Convert.ToString(row[colnameCountry]);
            int rowYear = Convert.ToInt32(row[colnameYear]);

            float rowCO2;
            try
            {
                rowCO2 = float.Parse(row[colnameCO2].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                rowCO2 = -1f;
            }

            DataFormatWorld dataRow = new DataFormatWorld();
            dataRow.countryName = rowCountry;
            dataRow.co2emissions = rowCO2;

            if (!dataPerYear.ContainsKey(rowYear))
            {
                dataPerYear[rowYear] = new List<DataFormatWorld>();
            }

            dataPerYear[rowYear].Add(dataRow);
        }
    }

    public List<DataFormatWorld> GetDataForYear(int year)
    {
        if (dataPerYear.ContainsKey(year))
        {
            return dataPerYear[year];
        }
        return null;
    }
}