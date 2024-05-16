using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WPM;

public class GlobeControllerScript : MonoBehaviour
{
    
    public DataManager dataManager;
    

    public float rotateSpeed = 10f; // degrees per second
    float step;
    public ColorizeCountriesScript colorizeScript;
    public int selectedYear = 1990;

    // Start is called before the first frame update
    void Start()
    {
        colorizeScript.map.ToggleCountrySurface("Brazil", true, Color.blue);


    }

    // Update is called once per frame
    void Update()
    {
        //Test code from g
        if (OVRInput.GetUp(OVRInput.RawButton.Y) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            colorizeScript.ColorizeCountries(selectedYear);
            Debug.Log("Hello From inside the if conidition");
            //colorizeScript.map.ToggleCountrySurface("Brazil", true, Color.green);
        }

        // Handle year selection logic
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            selectedYear = Mathf.Max(selectedYear - 1, 1991); // Adjust year bounds as needed
            Debug.Log("Selected Year:" + selectedYear);
            colorizeScript.ColorizeCountries(selectedYear);
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            selectedYear = Mathf.Min(selectedYear + 1, 2021); // Adjust year bounds as needed
            Debug.Log("Selected Year:" + selectedYear);
            colorizeScript.ColorizeCountries(selectedYear);
        }
        //End test
        //// Check if there is a connected gamepad
        //if (Gamepad.current != null)
        //{
        //    Vector2 rotationInput = Gamepad.current.rightStick.ReadValue();
        //  //  Vector2 rotationInput = new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
        //
        //
        //    // Rotate around the Y axis
        //    transform.Rotate(0, rotationInput.x * rotateSpeed * Time.deltaTime, 0);
        //}


        // Documentation from: https://developer.oculus.com/documentation/unity/unity-tutorial-basic-controller-input/


        /*
        //if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            //// Testing send data to Data UI Manager
            //DataFormatWorld data = new DataFormatWorld();
            //data.countryName = "Sweden";
            //data.co2emissions = 2300;

            // Read a value from the CSV file
            DataFormatWorld data = dataManager.GetTestValueFromYear();
            dataManager.SetDataToUI(data);
        }*/




        /// Rotate with the thumbsticks

        // Calculate a proportion in the rotation (degrees per second)
        step = rotateSpeed * Time.deltaTime;
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft))
        {
            transform.Rotate(0, step, 0);
            Debug.Log("Right Thumbstick detected - Left");
        }

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight))
        {
            transform.Rotate(0, -step, 0);
            Debug.Log("Right Thumbstick detected - Right");
        }

    }
        
}
