using UnityEngine;
using UnityEngine.SceneManagement;



namespace PhotonDemoProject.GameLogic
{
    public class GameManager : MonoBehaviour
    {

        #region Monobehaviour Callbacks
        private void OnEnable()
        {
            PhotonNetworking.RoomManager.OnRoomLeft += RoomLeft;
        }
        private void OnDisable()
        {
            
            PhotonNetworking.RoomManager.OnRoomLeft -= RoomLeft;
        }
        #endregion

        #region Private Methods


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