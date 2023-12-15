using System;
using DG.Tweening;
using UnityEngine;
using Managers;
using Signals;

namespace Controllers.PlayerObjectsManager
{
    public class PlayerObjectsPhysicsController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private Managers.PlayerObjectsManager playerObjectsManager;
        [SerializeField] private PlayerObjectsController playerObjectsController;

        #endregion
        #region Private Variables

        private bool _bullet;
        private bool CasualGame;
        private bool _trigger;
        
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

            if (other.CompareTag("MinigameTarret"))
            {
                playerObjectsController.MinigameAnimationChange();
            }

            if (other.CompareTag("Bullet"))
            {
                if (_bullet != true)
                {
                    playerObjectsController.PlayExecution();
                    playerObjectsController.Bullet();
                }
                _bullet = true;
            }

            if (other.CompareTag("IdleMap"))
            {
                _trigger = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("IdleObj"))
            {
                if (other.transform.position == transform.position)
                {
                    if (!_trigger)
                    {
                        playerObjectsController.ObjCasualStack();
                        _trigger = true;
                    }
                }
            }
        }
    }
}