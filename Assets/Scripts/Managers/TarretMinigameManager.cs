using System;
using Controllers.MinigameManager;
using Signals;
using UnityEngine;
using Color = Enums.Color;

namespace Managers
{
    public class TarretMinigameManager : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private TarretMinigameController _tarretMinigameController;

        #endregion
        #endregion
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnReset()
        {
            _tarretMinigameController.Reset();
        }
    }
}