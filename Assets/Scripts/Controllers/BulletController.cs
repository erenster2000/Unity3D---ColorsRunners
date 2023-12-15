using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        public void BulletDestroy()
        {
            transform.gameObject.SetActive(false);
        }
    }
}