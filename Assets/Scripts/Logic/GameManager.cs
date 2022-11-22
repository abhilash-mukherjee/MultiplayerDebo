using UnityEngine;
using UnityEngine.SceneManagement;
using PhotonNetworking;



namespace PhotonDemoProject.GameLogic
{
    public class GameManager : MonoBehaviour
    {

        #region Monobehaviour Callbacks
        private void OnEnable()
        {
            RoomManager.OnClientLeftRoom += RoomLeft;
            RoomManager.OnRemotePlayerLeftRoom += LoadArena;
            RoomManager.OnRemotePlayerEnteredRoom += LoadArena;
        }
        private void OnDisable()
        {
            
            RoomManager.OnClientLeftRoom -= RoomLeft;
            RoomManager.OnRemotePlayerLeftRoom -= LoadArena;
            RoomManager.OnRemotePlayerEnteredRoom -= LoadArena;
        }
        #endregion

        #region Private Methods

        #region Private Methods


        void LoadArena()
        {
            if (!NetworkHelper.Instance.IsMasterClient())
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", NetworkHelper.Instance.CurrentRoom.PlayerCount);
            NetworkHelper.Instance.LoadLevel("Room for " + NetworkHelper.Instance.CurrentRoom.PlayerCount);
        }


        #endregion

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        private void RoomLeft()
        {
            SceneManager.LoadScene(0);
        }


        #endregion


        #region Public Methods



        #endregion
    }
}