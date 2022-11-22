using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace PhotonNetworking
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        public delegate void RoomEntryExitHandler();
        public static event RoomEntryExitHandler OnClientLeftRoom, OnRemotePlayerLeftRoom, OnRemotePlayerEnteredRoom;

        public override void OnLeftRoom()
        {
            Debug.Log("Room is actually left");
            OnClientLeftRoom?.Invoke();
        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                OnRemotePlayerEnteredRoom?.Invoke();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                OnRemotePlayerLeftRoom?.Invoke();
            }
        }


    }

}