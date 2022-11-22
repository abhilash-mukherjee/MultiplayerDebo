using UnityEngine;
using Photon.Pun;


namespace PhotonDemoProject.UI
{
    public class RoomUI : MonoBehaviour
    {
        public delegate void RoomLeaveHandler();
        public static event RoomLeaveHandler OnRoomLeft;
        public void QuitRoom()
        {
            OnRoomLeft?.Invoke();
        }
    }
}