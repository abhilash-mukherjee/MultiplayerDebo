using Photon.Pun;

namespace PhotonNetworking
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        public delegate void RoomLeaveHandler();
        public static event RoomLeaveHandler OnRoomLeft;
        public override void OnEnable()
        {
            PhotonDemoProject.UI.RoomUI.OnRoomQuit += LeaveRoom;
        }
        public override void OnDisable()
        {
            PhotonDemoProject.UI.RoomUI.OnRoomQuit -= LeaveRoom;
            
        }
        public override void OnLeftRoom()
        {
            OnRoomLeft?.Invoke();
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

    }

}