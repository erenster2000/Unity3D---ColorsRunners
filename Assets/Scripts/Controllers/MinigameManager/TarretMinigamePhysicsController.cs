using Signals;
using UnityEngine;

namespace Controllers.MinigameManager
{
    public class TarretMinigamePhysicsController : MonoBehaviour
    {
        #region Self Variables
        #region Serizlized Variables

        [SerializeField] private TarretMinigameController tarretMinigameController;

        #endregion
        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (transform.CompareTag("MinigameTarret"))
            {
                if (other.CompareTag("Player"))
                {
                    MinigameSignals.Instance.onSlowMove?.Invoke();
                    tarretMinigameController.TargetStartLook();
                }
            }
        }
    }
}