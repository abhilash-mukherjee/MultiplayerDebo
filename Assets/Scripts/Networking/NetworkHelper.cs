using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace PhotonNetworking
{
    public class NetworkHelper : MonoBehaviourPunCallbacks
    {
        public static NetworkHelper Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        public  Room CurrentRoom { get => PhotonNetwork.CurrentRoom; }
        public  void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            Debug.Log("Instructed client to leave room");
        }

        public  bool IsMasterClient()
        {
            return PhotonNetwork.IsMasterClient;
        }

        public  void LoadLevel(string name)
        {
            PhotonNetwork.LoadLevel(name);

        }
    }
}