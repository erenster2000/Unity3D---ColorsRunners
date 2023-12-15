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

        #region Private Variables

        private GameObject _platform;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnHyperCasualMovement;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFinish += OnFinish;
            CoreGameSignals.Instance.onStation += OnStation;
            CoreGameSignals.Instance.onPlayerGameChange += OnGameChange;
            InputSignals.Instance.onCasualMovement += OnCasualMovement;
            MinigameSignals.Instance.onSlowMove += OnSlowMove;
            PlayerObjectsSignals.Instance.onIdleObjScale += OnIdleObjScale;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnHyperCasualMovement;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFinish -= OnFinish;
            CoreGameSignals.Instance.onStation -= OnStation;
            CoreGameSignals.Instance.onPlayerGameChange -= OnGameChange;
            InputSignals.Instance.onCasualMovement -= OnCasualMovement;
            MinigameSignals.Instance.onSlowMove -= OnSlowMove;
            PlayerObjectsSignals.Instance.onIdleObjScale -= OnIdleObjScale;
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
        
        private void OnHyperCasualMovement(HorizontalInputParams horizontalInputParams)
        {
            playerMovementController.HyperCasualMovementController(horizontalInputParams);
        }

        private void OnCasualMovement(JoystickInputParams joystickInputParams)
        {
            playerMovementController.CasualMovementController(joystickInputParams);
        }

        private void OnSlowMove()
        {
            playerMovementController.SlowMove();
        }

        private void OnGameChange()
        {
            playerMovementController.GameChange();
        }

        private void OnIdleObjScale(bool state)
        {
            playerMovementController.IdleObjScale(state);
        }

    }
}