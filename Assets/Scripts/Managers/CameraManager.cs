using System;
using Cinemachine;
using Controllers.CameraManager;
using Signals;
using UnityEngine;
using CameraState = Enums.CameraState;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private CameraController cameraController;

        #endregion

        #endregion
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MinigameSignals.Instance.onSetCamera += OnSetCamera;
            PlayerSignals.Instance.onPlayCamera += OnPlayCamera;

        }

        private void UnsubscribeEvents()
        {
            MinigameSignals.Instance.onSetCamera -= OnSetCamera;
            PlayerSignals.Instance.onPlayCamera -= OnPlayCamera;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnPlayCamera(CameraState cameraState)
        {
            cameraController.PlayCamera(cameraState);
        }

        private void OnSetCamera(GameObject follow)
        {
            cameraController.SetCamera(follow);
        }
    }
}