using System.Collections;
using System.Collections.Generic;
using Command.StackController;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers.StackManager
{
    public class StackController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables

        [Header("Data")] public StackData StackData;
        public List<GameObject> StackListObj;
        public List<GameObject> PoolListObj;
        public List<GameObject> MinigameObjList;
        public ListChangeCommand ListChangeCommand;

        #endregion
        #region Serialized Variables

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerObj;
        [SerializeField] private Transform pool;
        [SerializeField] private GameObject MinigamePlatform;

        #endregion
        #region Private Variables

        #endregion
        #endregion

        private void Awake()
        {
            StackData = GetStackData();
            ListChangeCommand = new ListChangeCommand(ref StackListObj, ref PoolListObj, transform, pool,
                player.transform, ref StackData);
        }

        private void Start()
        {
            for (int i = 0; i < 20; i++)
            {
                PoolInstantiate();
            }
            
            for (int i = 0; i < 20; i++)
            {
                 ListChange(PoolListObj[0], 2);
            }
        }

        private StackData GetStackData() => Resources.Load<SO_StackData>("Data/SO_StackData").StackData;

        public void PositionUpdate()
        {
            int count = StackListObj.Count;
            if (count != 0)
            {
                StackListObj[0].transform.position = player.transform.position;
            }
        }

        public void MoveStack()
        {
            int Count = StackListObj.Count;
            for (int i = 1; i <= Count - 1; i++)
            {
                Vector3 stackPos = StackListObj[i - 1].transform.localPosition;
                float lerpObjx = Mathf.Lerp(StackListObj[i].transform.localPosition.x, stackPos.x, StackData.LerpDelay);
                float lerpobjz = Mathf.Lerp(StackListObj[i].transform.localPosition.z - StackData.StackBetween, stackPos.z, StackData.LerpDelay);
                float lerpobjy = Mathf.Lerp(StackListObj[i].transform.localPosition.y, stackPos.y, StackData.LerpDelay);
                StackListObj[i].transform.localPosition = new Vector3(lerpObjx, lerpobjy, lerpobjz);
            }
        }

        public void HelicopterPlatformStack()
        {
            if (StackListObj.Count > 0)
            {
                if (StackListObj.Count != 1)
                {
                    Transform lastPosition = StackListObj[1].transform;
                    player.transform.position = lastPosition.position;
                }
                var obj = StackListObj[0];
                MinigameObjList.Add(obj);
                StackListObj.Remove(obj);

                if (StackListObj.Count == 0)
                {
                    CoreGameSignals.Instance.onFinish?.Invoke();
                    DOVirtual.DelayedCall(1,()=>StackSignals.Instance.onPlatformClose?.Invoke());
                }
            }
        }

        private void PoolInstantiate()
        {
            GameObject player = Instantiate(playerObj);
            PoolListObj.Add(player);
            player.transform.SetParent(pool);
            player.SetActive(false);
        }

        public void ListChange(GameObject obj, int list)
        {
            ListChangeCommand.ListChange(obj, list);
        }
    }
}