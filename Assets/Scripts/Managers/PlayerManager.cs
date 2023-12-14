using Controllers.PlayerManager;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnMovement;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFinish += OnFinish;
            CoreGameSignals.Instance.onStation += OnStation;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnMovement;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFinish -= OnFinish;
            CoreGameSignals.Instance.onStation += OnStation;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnStation(bool variable)
        {
            playerMovementController.Station(variable);
        }
        
        private void OnReset()
        {
            playerMovementController.Reset();
        }

        private void OnPlay()
        {
            playerMovementController.Play();
        }

        public void OnFinish()
        {
            playerMovementController.Finish();
        }
        
        private void OnMovement(HorizontalInputParams horizontalInputParams)
        {
            playerMovementController.movementcontroller(horizontalInputParams);
        }
    }
}