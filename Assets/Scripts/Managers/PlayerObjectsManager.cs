using Controllers.PlayerObjectsManager;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerObjectsManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerObjectsController playerObjectsController;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerAnimation += OnPlayerAnimation;
            MinigameSignals.Instance.onPlayExecution += OnPlayExecution;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerAnimation -= OnPlayerAnimation;
            MinigameSignals.Instance.onPlayExecution -= OnPlayExecution;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnPlayerAnimation(string animation)
        {
            playerObjectsController.PlayerAnimation(animation);
        }

        public void PlayersMinigameControl()
        {
            playerObjectsController.MinigameControl();
        }

        public void PlayerColorChange(GameObject door)
        {
            playerObjectsController.Comparison(door);
        }

        private void OnPlayExecution()
        {
            playerObjectsController.PlayExecution();
        }
    }
}