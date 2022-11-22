using UnityEngine;

namespace PhotonDemoProject.UI
{
    public class ParkingAreaSelector: MonoBehaviour
    {
        [SerializeField] private string[] parkingAreaNames;
        private int currentParkingIndex;
        public delegate void ParkingAreaSelectionHandler(string parkingID);
        public static event ParkingAreaSelectionHandler OnParkingAreaSelected;
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                currentParkingIndex = currentParkingIndex >= parkingAreaNames.Length - 1 ? 0 : currentParkingIndex + 1;
                OnParkingAreaSelected?.Invoke(parkingAreaNames[currentParkingIndex]);
                Debug.Log("Parking Area Updated: " + parkingAreaNames[currentParkingIndex]);
            }
        }
    }
}