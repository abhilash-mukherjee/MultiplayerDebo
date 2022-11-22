using UnityEngine;
using UnityEngine.UI;

namespace PhotonDemoProject.UI
{
    public class ConnectionUI : MonoBehaviour
    {
        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;
        private void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

        }

        private void OnEnable()
        {
            PhotonNetworking.Launcher.OnConnectionLost += OnDisconnected;
        }
        private void OnDisable()
        {
            PhotonNetworking.Launcher.OnConnectionLost -= OnDisconnected;
        }

        private void OnDisconnected()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public void OnConnectionStarted()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
        }
    }
}