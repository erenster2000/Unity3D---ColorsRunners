using System;
using UnityEngine;

namespace Controllers
{
    public class BulletPhysicsController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private BulletController bulletController;

        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerObj"))
            {
                bulletController.BulletDestroy();
            }
        }
    }
}