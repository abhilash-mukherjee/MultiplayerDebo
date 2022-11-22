using UnityEngine;
using Photon.Pun;


namespace PhotonDemoProject.UI
{
    public class RoomUI : MonoBehaviour
    {

        public void QuitRoom()
        {
            Debug.Log("Quit Room clicked");
            PhotonNetworking.NetworkHelper.Instance.LeaveRoom();
        }
    }
}