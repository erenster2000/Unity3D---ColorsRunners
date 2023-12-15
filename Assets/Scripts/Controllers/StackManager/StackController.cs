using System.Collections;
using System.Collections.Generic;
using Command.StackController;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using UnityEngine;

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
        public List<GameObject> _slowStack;
        public ListChangeCommand ListChangeCommand;

        #endregion
        #region Serialized Variables
        
        [SerializeField] private GameObject playerObj;
        [SerializeField] private Transform pool;
        [SerializeField] private GameObject ıdleObj;

        #endregion
        #region Private Variables
        
        private GameObject _platform;
        private GameObject _player;
        private bool _reset;
        private bool _hyperCasual;
        private bool _minigame;
        private bool _trigger;
        
        #endregion
        #endregion

        private void Awake()
        {
            StackData = GetStackData();
            _hyperCasual = true;
            _reset = false;
        }
        
        public void GameChange()
        {
            _hyperCasual = false;
        }

        private void Start()
        {
            _player = FindObjectOfType<Managers.PlayerManager>().gameObject;
            ListChangeCommand = new ListChangeCommand(ref StackListObj, ref PoolListObj, transform, pool,
                _player.transform, ref StackData);
            for (int i = 0; i < 100; i++)
            {
                PoolInstantiate();
            }
            
            for (int i = 0; i < 6; i++)
            {
                 ListChange(PoolListObj[0], "Stack");
            }
            _trigger = false;
        }
        
        public GameObject FirstPlayerObject(){ return StackListObj[0].gameObject;}
        public void MinigameStackAdd(GameObject obj) { _slowStack.Add(obj); MinigameObjList.Remove(obj);}
        public int StackCount(){return StackListObj.Count - 1;}
        private StackData GetStackData() => Resources.Load<SO_StackData>("Data/SO_StackData").StackData;

        public void PositionUpdate()
        {
            int count = StackListObj.Count;
            if (count != 0)
            {
                StackListObj[0].transform.position = _player.transform.position;
            }
            else
            {
                if (_hyperCasual)
                {
                    if (!_minigame)
                    {
                        Fail();
                    }
                }
                else
                {
                    if (!_trigger)
                    {
                        CoreGameSignals.Instance.onStation?.Invoke(false);
                        CoreGameSignals.Instance.onPlayerGameChange?.Invoke();
                        MinigameSignals.Instance.onSetCamera(_player);
                        StackSignals.Instance.onJoystick?.Invoke();
                        _trigger = true;
                    }
                }
            }
        }

        public void Basket()
        {
            MinigameObjList[0].transform.localPosition = _player.transform.GetChild(0).transform.position;
            MinigameObjList[0].SetActive(true);
            MinigameObjList[0].GetComponent<Rigidbody>().useGravity = true;
            MinigameObjList[0].GetComponent<Rigidbody>().AddForce(new Vector3(0,10,3), ForceMode.VelocityChange);
            MinigameObjList.Remove(MinigameObjList[0]);
            MinigameObjList.TrimExcess();
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

        public void Fail()
        {
            if (_slowStack.Count == 0)
            {
                CoreGameSignals.Instance.onStation?.Invoke(true);
                if (!_reset)
                {
                    _reset = true;
                    StackSignals.Instance.onUIReset?.Invoke();
                }
            }
        }

        public void CasualStack()
        {
            if (StackListObj.Count > 0)
            {
                if (StackListObj.Count != 1)
                {
                    Transform lastPosition = StackListObj[1].transform;
                    _player.transform.position = lastPosition.position;
                }
                var obj = StackListObj[0];
                MinigameObjList.Add(obj);
                StackListObj.Remove(obj);
            }
        }

        public void HelicopterPlatformStack()
        {
            _minigame = true;
            if (StackListObj.Count > 0)
            {
                if (StackListObj.Count != 1)
                {
                    Transform lastPosition = StackListObj[1].transform;
                    _player.transform.position = lastPosition.position;
                }
                var obj = StackListObj[0];
                MinigameObjList.Add(obj);
                StackListObj.Remove(obj);

                if (StackListObj.Count == 0)
                {
                    CoreGameSignals.Instance.onStation?.Invoke(true);
                    _player.transform.position = new Vector3(MinigameObjList[0].transform.position.x,_player.transform.position.y,MinigameObjList[0].transform.position.z);
                    DOVirtual.DelayedCall(2, () => StackSignals.Instance.onSetOutlineBorder?.Invoke(true));
                    DOVirtual.DelayedCall(4, () => MinigameSignals.Instance.onPlayHelicopterExecution?.Invoke());
                    DOVirtual.DelayedCall(4.1f, () => Fail());
                    DOVirtual.DelayedCall(5.5f, () => StartCoroutine(SlowlyStackAdd())); // platformun kapanmasına göre bir koşul yaz
                }
            }
        }

        public void MinigameAdd(GameObject obj)
        {
            MinigameObjList.Add(obj);
            StackListObj.Remove(obj);
            StackListObj.TrimExcess();
        }

        public void MinigamePoolAdd(GameObject minigameObj)
        {
            ListChangeCommand.ListChange(minigameObj, "Pool");
            MinigameObjList.Remove(minigameObj);
        }

        public void StackStriking()
        {
            var count = StackListObj.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ListChange(PoolListObj[0], "Stack");
                }
                StackSignals.Instance.onMinigameColor?.Invoke(StackListObj[0]);
            }
        }

        public void SlowStackStriking(int index)
        {
            for (int i = 0; i < index; i++)
            {
                PoolListObj[i].SetActive(true);
                StackSignals.Instance.onMinigameColor?.Invoke(_slowStack[0]);
                PoolListObj[i].GetComponent<Animator>().SetTrigger("StandingToCrouched");// PlayerObjectsController üzerinden yapılacak.
                PoolListObj[i].transform.SetParent(transform);
                PoolListObj[i].transform.position = _slowStack[i].transform.position;
                _slowStack.Add(PoolListObj[i]);
                PoolListObj.Remove(PoolListObj[i]);
            }
        }

        public IEnumerator SlowlyStackAdd()
        {
            var index = _slowStack.Count;
            SlowStackStriking(index);
            if (_slowStack.Count != 0)
            {
                MinigameSignals.Instance.onSetCamera?.Invoke(_player);
                CoreGameSignals.Instance.onStation?.Invoke(false);
            }
            for (int i = 0; i < index + index; i++)
            {
                StackListObj.Add(_slowStack[0]);
                _slowStack[0].GetComponent<Animator>().SetTrigger("Runner"); // PlayerObject üzerinden yapılacak
                _slowStack.Remove(_slowStack[0]);
                yield return new WaitForSeconds(.15f);
            }
        }

        private void PoolInstantiate()
        {
            GameObject player = Instantiate(playerObj);
            PoolListObj.Add(player);
            player.transform.SetParent(pool);
            player.SetActive(false);
        }

        public void ListChange(GameObject obj, string listName)
        {
            ListChangeCommand.ListChange(obj, listName);
        }

        public GameObject TarretSetObj()
        {
            return StackListObj[0];
        }

        public void Reset()
        {
            for (int i = 0; i < 6; i++)
            {
                ListChange(PoolListObj[0], "Stack");
            }
            DOVirtual.DelayedCall(.1f, () => ResetBool());
        }
        private void ResetBool(){ _reset = false;}
    }
}