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

        #region Private Variables

        [Header("Data")] private InputData _inputData;
        
        private Vector2? _mousePosition;
        private Vector3 _moveVector;
        private float _currentVelocity;
        private bool _isTouchingPlayer = true;

        #endregion

        #endregion
        
        private void Awake()
        {
            _inputData = GetInputData();
        }

        private InputData GetInputData()
        {
            return Resources.Load<SO_InputData>("Data/SO_InputData").InputData;
        }
        
        private void Update()
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