using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Joystick _joystick;

        #endregion
        #region Private Variables
        
        [Header("Data")] private InputData _inputData;
        private Vector3 _joystickPos;
        private Vector2? _mousePosition;
        private Vector3 _moveVector;
        private float _currentVelocity;
        private bool _isTouchingPlayer = true;
        private bool _hyperCasual;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGameChange += OnGameChange;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGameChange -= OnGameChange;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        
        private void Awake()
        {
            _inputData = GetInputData();
            _hyperCasual = true;
        }

        private InputData GetInputData()
        {
            return Resources.Load<SO_InputData>("Data/SO_InputData").InputData;
        }

        private void OnGameChange()
        {
            _hyperCasual = false;
        }
        
        private void Update()
        {
            if (_hyperCasual)
            {
                HyperCasualInput();
            }
            else
            {
                CasualInput();
            }
        }

        private void HyperCasualInput()
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement()) _mousePosition = Input.mousePosition;

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
                if (_isTouchingPlayer)
                    if (_mousePosition != null)
                    {
                        var mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

                        if (mouseDeltaPos.x > _inputData.HorizontalInputSpeed)
                            _moveVector.x = _inputData.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        else if (mouseDeltaPos.x < -_inputData.HorizontalInputSpeed)
                            _moveVector.x = -_inputData.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                        else
                            _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                                _inputData.ClampSpeed);

                        _mousePosition = Input.mousePosition;

                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
                        {
                            XValue = _moveVector.x,
                            ClampValues = new Vector2(_inputData.ClampSides.x, _inputData.ClampSides.y)
                        });
                    }
        }

        private void CasualInput()
        {
            _joystickPos = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            InputSignals.Instance.onCasualMovement?.Invoke(new JoystickInputParams
            {
                JoystickMove = _joystickPos
            });
        }
        
        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}