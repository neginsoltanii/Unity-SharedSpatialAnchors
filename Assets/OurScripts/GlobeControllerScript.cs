using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WPM;
using Photon.Pun;

public class GlobeControllerScript : MonoBehaviourPun  // Inherit from MonoBehaviourPun
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
        if (!photonView.IsMine) return; // If it's not the local player's object, don't execute the input code

        // Handle color change or year selection input
        HandleInput();

        
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

        if (!photonView.IsMine) return; // If it's not the local player's object, don't execute the rotation code

        step = rotateSpeed * Time.deltaTime;

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft))
        {
            RotateLeft();
        }

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight))
        {
            RotateRight();
        }

    }

    
    void HandleInput()
    {
        //Test code from g
        // Handle color change input
        if (OVRInput.GetUp(OVRInput.RawButton.Y) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            photonView.RPC("ColorizeCountriesRPC", RpcTarget.All, selectedYear); // Call RPC to colorize countries for all players
            Debug.Log("Hello From inside the if conidition");
            //colorizeScript.map.ToggleCountrySurface("Brazil", true, Color.green);
        }

        // Handle year selection logic
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            SelectPreviousYear();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            SelectNextYear();
        }
    }

    void SelectPreviousYear()
    {
        selectedYear = Mathf.Max(selectedYear - 1, 1991); // Adjust year bounds as needed
        photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear); // Call RPC to update selected year for all players
    }

    void SelectNextYear()
    {
        selectedYear = Mathf.Min(selectedYear + 1, 2021); // Adjust year bounds as needed
        photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear); // Call RPC to update selected year for all players
    }

    [PunRPC]
    void UpdateSelectedYearRPC(int year)
    {
        selectedYear = year;
        Debug.Log("Selected Year: " + selectedYear);
        colorizeScript.ColorizeCountries(selectedYear);
    }

    [PunRPC]
    void ColorizeCountriesRPC(int year)
    {
        Debug.Log("Colorizing countries for year: " + year);
        colorizeScript.ColorizeCountries(year);
    }




    [PunRPC] // Mark the RPC methods with this attribute
    void RotateLeft()
    {
        transform.Rotate(0, step, 0);
        photonView.RPC("RotateLeftRPC", RpcTarget.Others); // Send RPC to other clients
        Debug.Log("Right Thumbstick detected - Left");
    }

    [PunRPC]
    void RotateRight()
    {
        transform.Rotate(0, -step, 0);
        photonView.RPC("RotateRightRPC", RpcTarget.Others); // Send RPC to other clients
        Debug.Log("Right Thumbstick detected - Right");
    }

    [PunRPC]
    void RotateLeftRPC()
    {
        transform.Rotate(0, step, 0);
    }

    [PunRPC]
    void RotateRightRPC()
    {
        transform.Rotate(0, -step, 0);
    }
}
