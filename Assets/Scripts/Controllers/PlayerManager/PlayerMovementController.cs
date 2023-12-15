using System;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using Unity.Mathematics;
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
        private Vector3 _joystickInput;
        private Vector3 _resetPos;
        private bool _isTouchingPlayer;
        private bool _station;
        private bool _minigameHelicopter;
        private bool _hyperCasual;
        private bool _situation;
        private GameObject idleObj;

        #endregion
        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            _isTouchingPlayer = false;
            _station = true;
            _hyperCasual = true;
        }

        private void Start()
        {
            idleObj = transform.parent.parent.GetChild(0).GetChild(0).gameObject;
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<SO_PlayerData>("Data/SO_PlayerData").PlayerData;
        }
        public void HyperCasualMovementController(HorizontalInputParams inputParams)
        {
            _inputSpeed = inputParams.XValue;
            _clamp = inputParams.ClampValues;
        }

        public void CasualMovementController(JoystickInputParams joystickInputParams)
        {
            _joystickInput = joystickInputParams.JoystickMove;
        }

        public void GameChange()
        {
            _hyperCasual = false;
            idleObj.transform.SetParent(transform);
        }

        private void FixedUpdate()
        {
            if (_isTouchingPlayer)
            {
                if (_hyperCasual)
                {
                    if (!_station)
                        HyperCaualMove();
                    else
                        StopMove();
                }
                else
                {
                    if (!_station)
                    {
                        CasualMove();
                    }
                    else
                    {
                        StopMove();
                    }
                }
            }
        }

        public void IdleObjScale(bool state)
        {
            if (state == false)
            {
                idleObj.transform.localScale -= new Vector3(.1f, .1f, .1f);
            }
            else
            {
                idleObj.transform.localScale += new Vector3(.1f, .1f, .1f);
            }
        }
        
        private void HyperCaualMove()
        {
            move.velocity = new Vector3(_inputSpeed * _playerData.MovementSide, move.velocity.y, _playerData.MoveSpeed);
            move.position = new Vector3(Mathf.Clamp(move.position.x, _clamp.x, _clamp.y), move.position.y, move.position.z);
        }

        private void CasualMove()
        {
            move.velocity = new Vector3(_joystickInput.x * _playerData.MovementSide, move.velocity.y, _joystickInput.z * _playerData.MovementSide);
            Vector3 direction = Vector3.forward * _joystickInput.z + Vector3.right * _joystickInput.x;
            if (direction != new Vector3(0,0,0))
            {
                idleObj.transform.localRotation = Quaternion.Slerp(idleObj.transform.rotation, Quaternion.LookRotation(direction), 4 * Time.deltaTime);
            }
            if (_joystickInput.x != 0 || _joystickInput.z != 0)
            {
                CasualCaracterAnimation("Runner");
            }
            else
            {
                CasualCaracterAnimation("Idle");
            }
        }

        private void CasualCaracterAnimation(string animation)
        {
            if (animation == "Runner")
            {
                if (!_situation)
                {
                    idleObj.transform.GetComponent<Animator>().SetTrigger("Runner");
                    _situation = true;
                }
            }
            else
            {
                if (_situation)
                {
                    idleObj.transform.GetComponent<Animator>().SetTrigger("Idle");
                    _situation = false;
                }
            }
        }

        private void StopMove()
        {
            move.velocity = Vector3.zero;
        }

        public void Station(bool variable)
        {
            _station = variable;
        }

        public void SlowMove(){
            
            switch (_playerData.MoveSpeed)
            {
                case 10:
                    _playerData.MoveSpeed = 3;
                    break;
                case 3:
                    PlayerSignals.Instance.onStackStriking?.Invoke();
                    _playerData.MoveSpeed = 10;
                    break;
            }
        }
        
        public void Play()
        {
            _resetPos = transform.position;
            _isTouchingPlayer = true;
            _station = false;
            PlayerSignals.Instance.onPlayerAnimation?.Invoke("Runner");
        }

        public void Finish()
        {
            _station = true;
        }

        public void Reset()
        {
            transform.position = _resetPos;
            _isTouchingPlayer = false;
            _station = true;
            _hyperCasual = true;
            _playerData.MoveSpeed = 10;
            MinigameSignals.Instance.onSetCamera?.Invoke(transform.gameObject);
        }
    }
}