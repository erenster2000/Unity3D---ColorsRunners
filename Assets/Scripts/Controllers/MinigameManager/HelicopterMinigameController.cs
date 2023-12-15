using System;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers.MinigameManager
{
    public class HelicopterMinigameController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        #endregion
        #region Private Variables

        private int _stackCount;
        private int _platform;

        #endregion
        #endregion

        private int StackCount() { _stackCount = (int)MinigameSignals.Instance.onStackCount?.Invoke(); return _stackCount;}
        
        public void Close(GameObject playerObj)
        {
            if (StackCount() <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (transform.GetChild(i).GetComponent<Renderer>().material.color != playerObj.transform.GetChild(0).GetComponent<Renderer>().material.color)
                    {
                        transform.GetChild(i).transform.DOScaleZ(0, 1).SetDelay(1.5f);
                        _platform = i;
                    }
                }
            }
        }

        public void Reset()
        {
            var transformLocalScale = transform.GetChild(_platform).transform.localScale;
            transformLocalScale.z = 1;
            transform.GetChild(_platform).transform.localScale = transformLocalScale;
        }
    }
}