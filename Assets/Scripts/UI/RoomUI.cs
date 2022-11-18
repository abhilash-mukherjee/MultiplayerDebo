using UnityEngine;

namespace PhotonDemoProject.UI
{
    public class RoomUI : MonoBehaviour
    {
        public delegate void RoomQuitHandler();
        public static event RoomQuitHandler OnRoomQuit;
        public void QuitRoom()
        {
            OnRoomQuit?.Invoke();
        }
    }
}