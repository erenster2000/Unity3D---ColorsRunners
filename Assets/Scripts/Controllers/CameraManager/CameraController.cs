using Cinemachine;
using UnityEngine;
using CameraState = Enums.CameraState;

namespace Controllers.CameraManager
{
    public class CameraController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private CinemachineStateDrivenCamera vmStateCamera;
        [SerializeField] private GameObject player;

        #endregion

        #region Private Variables

        private CameraState _cameraState;
        private GameObject _player;
        
        #endregion
        #endregion

        private void Start()
        {
            _player = FindObjectOfType<Managers.PlayerManager>().gameObject;
            SetCamera(_player);
        }
        
        public void SetCamera(GameObject follow)
        {
            vmStateCamera.Follow = follow.transform;
        }

        public void PlayCamera(CameraState cameraState)
        {
            _cameraState = cameraState;
            switch (_cameraState)
            {
                case CameraState.Runner:
                    animator.SetTrigger(_cameraState.ToString());
                    break;
                case CameraState.Minigame:
                    animator.SetTrigger(_cameraState.ToString());
                    break;
                case CameraState.Casual:
                    animator.SetTrigger(_cameraState.ToString());
                    break;
            }
        }
    }
}