using UnityEngine;

namespace Controllers.MinigameManager
{
    public class TarretPlatformPhysicsController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private TarretMinigameController tarretMinigameController;

        #endregion
        #endregion
        
        private void OnTriggerStay(Collider other)
        {
            if (transform.CompareTag("TarretPlatform"))
            {
                if (other.CompareTag("Player"))
                {
                    tarretMinigameController.Platform(transform.gameObject);
                }
            }
        }
    }
}