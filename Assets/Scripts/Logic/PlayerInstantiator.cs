using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhotonDemoProject.Data;
using UnityEngine.SceneManagement;

namespace PhotonNetworking
{
    public class PlayerInstantiator : MonoBehaviour
    {
        [Tooltip("The prefab to use for representing the player")]
        public GameObject sailorPrefab, operatorPrefab;
        [SerializeField] private string operatorView, sailorView;
        [SerializeField] private Transform operatorTransform, sailorTransform;
        #region Monobehaviour Callbacks

        private void Start()
        {
            if (sailorPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    
                    if(ClientSideData.Instance.View == sailorView)
                    PhotonNetwork.Instantiate(this.sailorPrefab.name, sailorTransform.position, sailorTransform.rotation, 0);
                    else
                    PhotonNetwork.Instantiate(this.operatorPrefab.name, operatorTransform.position, operatorTransform.rotation, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
        }

        #endregion

    }
}