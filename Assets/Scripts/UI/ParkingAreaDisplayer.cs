using System;
using TMPro;
using UnityEngine;
namespace PhotonDemoProject.UI
{
    public class ParkingAreaDisplayer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI parkingAreaText;
        private void OnEnable()
        {
            PhotonNetworking.NetworkEventLayer.OnParkingAreaSelectionEventRecievedByClient += UpdateParkingAreaText;
        }
        // Start is called before the first frame update
        void OnDisable()
        {
            PhotonNetworking.NetworkEventLayer.OnParkingAreaSelectionEventRecievedByClient -= UpdateParkingAreaText;

        }

        private void UpdateParkingAreaText(string parkingID)
        {
            parkingAreaText.text = parkingID;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}