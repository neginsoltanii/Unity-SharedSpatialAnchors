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
    // Dataset containing the data as desired in the dictionary per year
    public Dictionary<int, List<DataFormatWorld>> dataPerYear;

    // 
    public int testYear = 2021;
    private int testIndexData = 0;

    public TMP_Text dataCountryUI;
    public TMP_Text dataCo2emissionsUI;

    // Start is called before the first frame update
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

    public DataFormatWorld GetTestValueFromYear()
    {
        DataFormatWorld exampleData = dataPerYear[testYear][testIndexData];
        testIndexData++;
        return exampleData;
    }


    // Process CSV File
    void LoadCSVFiles()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "co2Emission.csv");
        string result = System.IO.File.ReadAllText(filePath);

        //Read csv
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
            // Check if the year is already in the main dictionary
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

            // Reformat the values into our own data structure
            DataFormatWorld dataRow = new DataFormatWorld();
            dataRow.countryName = rowCountry;
            dataRow.co2emissions = rowCO2;

            if (!dataPerYear.ContainsKey(rowYear))
            {
                // Year doesn't exist, create it in the dict and append data.
                dataPerYear[rowYear] = new List<DataFormatWorld>();
            }

            dataPerYear[rowYear].Add(dataRow);
        }
    }
}
