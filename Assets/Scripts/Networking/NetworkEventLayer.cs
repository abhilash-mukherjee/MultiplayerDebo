using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace PhotonNetworking
{
    public class NetworkEventLayer : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        public delegate void ParkingAreaSelectionHandler(string parkingID);
        public static event ParkingAreaSelectionHandler OnParkingAreaSelectionEventRecievedByClient;
        [SerializeField] private byte parkingAreaSelectionEventCode = 1;
        public override void OnEnable()
        {
            base.OnEnable();
            PhotonDemoProject.UI.ParkingAreaSelector.OnParkingAreaSelected += OnParkingAreaSelected;
        }
        // Start is called before the first frame update
        public override void OnDisable()
        {
            base.OnDisable();
            PhotonDemoProject.UI.ParkingAreaSelector.OnParkingAreaSelected -= OnParkingAreaSelected;

        }
        private void OnParkingAreaSelected(string parkingID)
        {
            object[] content = new object[] {parkingID }; // Array contains the target position and the IDs of the selected units
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            PhotonNetwork.RaiseEvent(parkingAreaSelectionEventCode, content, raiseEventOptions, SendOptions.SendReliable);
            Debug.Log("Parkingareaselection event emitted");

        }

        public void OnEvent(EventData photonEvent)
        {

            byte eventCode = photonEvent.Code;
            if (eventCode == parkingAreaSelectionEventCode)
            {
                Debug.Log("Parkingareaselection event recieved by local client");
                object[] data = (object[])photonEvent.CustomData;
                string parkingID = (string)data[0];
                OnParkingAreaSelectionEventRecievedByClient?.Invoke(parkingID);
            }
        }
    }
}