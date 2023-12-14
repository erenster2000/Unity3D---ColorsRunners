using System;
using UnityEngine;
using Managers;

namespace Controllers.PlayerObjectsManager
{
    public class PlayerObjectsPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Managers.PlayerObjectsManager playerObjectsManager;
        [SerializeField] private PlayerObjectsController playerObjectsController;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ColorChangeDoor"))
            {
                playerObjectsManager.PlayerColorChange(other.gameObject);
            }

            if (other.CompareTag("MinigameHelicopter"))
            {
                playerObjectsManager.PlayersMinigameControl();
            }
            
            if (other.CompareTag("Platform"))
            {
                playerObjectsController.PlayerExecution(other.gameObject);
            }
        }
    }
}