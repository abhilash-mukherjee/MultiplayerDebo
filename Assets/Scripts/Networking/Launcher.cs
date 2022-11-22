using Photon.Pun;
using Photon.Realtime;
using PhotonDemoProject.Data;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace PhotonNetworking
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public delegate void ConnectionHandler();
        public static event ConnectionHandler OnConnectionLost;

        #region Private Serializable Fields
        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField] private byte maxPlayersPerRoom = 4;
        [SerializeField] private string Operator, Salior;
        #endregion


        #region Private Fields


        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";
        bool isConnecting;
        private const string OPERATOR_EXIST = "OE";
        private const string SAILOR_EXISTS = "PE";
        private bool masterConnected = false;


        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            RoomManager.OnClientLeftRoom += () => masterConnected = false;
        }
        public override void OnDisable()
        {
            base.OnDisable();
            RoomManager.OnClientLeftRoom -= () => masterConnected = false;

        }
        #endregion


        #region Public Methods

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }


        #endregion


        #region MonoBehaviourPunCallbacks Callbacks


        public override void OnConnectedToMaster()
        {
            masterConnected = true;
            if (isConnecting)
            {
                JoinRandomRoom();
                isConnecting = false;
            }

        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            masterConnected = false;
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            OnConnectionLost?.Invoke();

        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            CreateRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");
                PhotonNetwork.LoadLevel("Room for 1");
            } 
        }

        #endregion

        #region Private Methods
        private void CreateRoom()
        {
            if (!masterConnected) return;
            RoomOptions roomOptions = new RoomOptions();
            string[] keys = { OPERATOR_EXIST, SAILOR_EXISTS};
            roomOptions.CustomRoomPropertiesForLobby = keys;
            bool isOperatorView = ClientSideData.Instance.View == Operator;
            bool isSailorView = !isOperatorView;
            roomOptions.CustomRoomProperties = new Hashtable { {maxPlayersPerRoom, maxPlayersPerRoom},{ OPERATOR_EXIST, isOperatorView }, 
                { SAILOR_EXISTS, isSailorView } };
            PhotonNetwork.CreateRoom(null, roomOptions, null);
        }

        private void JoinRandomRoom()
        {
            bool isOperatorView = ClientSideData.Instance.View == Operator;
            Debug.Log("Is operator view = " + isOperatorView);
            Hashtable expectedCustomRoomProperties = new Hashtable { { OPERATOR_EXIST, !isOperatorView }, { SAILOR_EXISTS, isOperatorView } };
            PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxPlayersPerRoom);
        }
        #endregion


    }

}