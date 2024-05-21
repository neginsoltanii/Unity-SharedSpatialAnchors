using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using Photon.Pun;

public class SliderSyncDirect : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider yearSlider; 
    public TextMeshProUGUI yearText; 

    void Start()
    {
        
    }

    void Update()
    {
        if (yearSlider != null)
        {
            // Setting the Slider's minimum and maximum values
            yearSlider.minValue = 1990;
            yearSlider.maxValue = 2018;
            yearSlider.onValueChanged.AddListener(delegate { UpdateYearText(); });
        }

        // Initialise year text
        UpdateYearText();
    }



    void UpdateYearText()
    {
        int year = (int)yearSlider.value;
        yearText.text = year.ToString();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Local player sends data
            stream.SendNext(yearSlider.value);
        }
        else
        {
            // Remote player receives data
            yearSlider.value = (float)stream.ReceiveNext();
            UpdateYearText();
        }
    }
}
