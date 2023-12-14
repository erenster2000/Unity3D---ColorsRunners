using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Controllers.PlayerManager
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody move;

        #endregion

        #region Private Variables

        [Header("Data")] private PlayerData _playerData;

        private float _inputSpeed;
        private Vector2 _clamp;
        private bool _isTouchingPlayer;
        private bool _station;

        #endregion

        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            _isTouchingPlayer = false;
            _station = true;
        }
        
        private PlayerData GetPlayerData()
        {
            return Resources.Load<SO_PlayerData>("Data/SO_PlayerData").PlayerData;
        }
        
        public void movementcontroller(HorizontalInputParams inputParams)
        {
            _inputSpeed = inputParams.XValue;
            _clamp = inputParams.ClampValues;
        }
        
        private void FixedUpdate()
        {
            if (_isTouchingPlayer)
            {
                if (!_station)
                    Move();
                else
                    StopMove();
            }
        }
        
        private void Move()
        {
            move.velocity = new Vector3(_inputSpeed * _playerData.MovementSide, move.velocity.y, _playerData.MoveSpeed);
            move.position = new Vector3(Mathf.Clamp(move.position.x, _clamp.x, _clamp.y), move.position.y, move.position.z);
        }

        private void StopMove()
        {
            move.velocity = Vector3.zero;
        }

        public void Station(bool variable)
        {
            _station = variable;
        }
        
        public void Play()
        {
            _isTouchingPlayer = true;
            _station = false;
            CoreGameSignals.Instance.onPlayerAnimation?.Invoke("Runner");
        }

        public void Finish()
        {
            _station = true;
        }

        public void Reset()
        {
            _isTouchingPlayer = false;
            _station = true;
        }
    }
}