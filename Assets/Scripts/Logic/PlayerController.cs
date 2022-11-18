using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhotonDemoProject.GameLogic
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 100f;
        private void Update()
        {
            var delX = Input.GetAxis("Horizontal");
            var delZ = Input.GetAxis("Vertical");
            transform.position += new Vector3(delX, 0, delZ);
        }
    }

}
