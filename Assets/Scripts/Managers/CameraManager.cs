using System;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineStateDrivenCamera vmStateCamera;
        [SerializeField] private GameObject player;

        #endregion

        #endregion

        private void Start()
        {
            OnSetCamera();
        }

        private void OnSetCamera()
        {
            vmStateCamera.Follow = player.transform;
        }
    }
}