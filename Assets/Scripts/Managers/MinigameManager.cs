using Controllers.MinigameManager;
using Signals;
using UnityEngine;
using Color = Enums.Color;

namespace Managers
{
    public class MinigameManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MinigameController _minigameController;

        #endregion

        #endregion
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onPlatformClose += OnPlatformClose;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onPlatformClose -= OnPlatformClose;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void OnPlatformClose()
        {
            _minigameController.Close();
        }
        
    }
}