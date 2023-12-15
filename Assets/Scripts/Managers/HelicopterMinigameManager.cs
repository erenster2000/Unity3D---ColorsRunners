using Controllers.MinigameManager;
using Signals;
using UnityEngine;

namespace Managers
{
    public class HelicopterMinigameManager : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables
        
        [SerializeField] private HelicopterMinigameController helicopterMinigameController;

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
            helicopterMinigameController.Reset();
        }
    }
}