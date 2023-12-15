using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.MinigameManager
{
    public class TarretMinigameController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variable

        public List<GameObject> BulletList;
        public List<ParticleSystem> ExplotionDotList;

        #endregion
        #region Serialized Variables
        
        [SerializeField] private GameObject rightTurret;
        [SerializeField] private GameObject leftTurret;
        [SerializeField] private GameObject fakeBullet;
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject rightBarrel;
        [SerializeField] private GameObject leftBarrel;
        [SerializeField] private ParticleSystem explotionParticleSystem;
        [SerializeField] private GameObject bulletPool;
        [SerializeField] private GameObject explotionPool;
        [SerializeField] private GameObject rightFakeTarget;
        [SerializeField] private GameObject leftFakeTarget;
        
        #endregion
        #region Private variables

        private int _stackCount;
        private GameObject _platform;
        private ParticleSystem _explotionDot;
        private bool _limit;
        private int _index;
        private GameObject _targetObj;
        
        #endregion
        #endregion

        private void Start()
        {
            for (int i = 0; i < 30; i++)
            {
                InstantiateBullet();
                InstantiateExplotion();
            }
        }

        private void FixedUpdate()
        {
            if (_limit != true)
            {
                FakeTargetLook();
            }
        }

        private void FakeTargetLook()
        {
            rightTurret.transform.DOLookAt(rightFakeTarget.transform.position, .3f);
            leftTurret.transform.DOLookAt(leftFakeTarget.transform.position, .3f);
        }

        private void InstantiateExplotion()
        {
            ParticleSystem insExplotion = Instantiate(explotionParticleSystem);
            ExplotionDotList.Add(insExplotion);
            insExplotion.transform.SetParent(explotionPool.transform);
        }

        private void InstantiateBullet()
        {
            GameObject insBullet = Instantiate(fakeBullet);
            BulletList.Add(insBullet);
            insBullet.transform.SetParent(bulletPool.transform);
            insBullet.SetActive(false);
        }

        public void Platform(GameObject platform)
        {
            _platform = platform;
        }

        public void TargetStartLook()
        {
            _index++;
            switch (_index)
            {
                case 1:
                    _limit = true;
                    break;
                case 2:
                    _limit = false;
                    break;
            }
            StartCoroutine(TargetLook());
            StartCoroutine(TargetFire());
        }

        private void SetAGoal()
        {
            if (MinigameSignals.Instance.onStackCount?.Invoke() >= 0)
            {
                _targetObj = MinigameSignals.Instance.onTarretSetObj?.Invoke();
            }
            else
            {
                _targetObj = null;
            }
        }

        public IEnumerator TargetLook()
        {
            while (true)
            {
                if (_limit == true)
                {
                    SetAGoal();
                    if (_targetObj != null)
                    {
                        if (_platform.GetComponent<Renderer>().material.color != _targetObj.transform.GetChild(0).GetComponent<Renderer>().material.color)
                        {
                            rightTurret.transform.DOLookAt(_targetObj.transform.position, .4f);
                            leftTurret.transform.DOLookAt(_targetObj.transform.position, .4f);
                        }
                        else
                        {
                            FakeTargetLook();
                        }
                    }
                    else
                    {
                        _limit = false;
                        FakeTargetLook();
                    }
                    yield return new WaitForFixedUpdate();
                }
                else yield break;
            }
        }

        public IEnumerator TargetFire()
        {
            int i = 0;
            while (true)
            {
                yield return new WaitForSeconds(.4f);
                if (_limit == true)
                {
                    if (_platform.GetComponent<Renderer>().material.color != _targetObj.transform.GetChild(0).GetComponent<Renderer>().material.color)
                    {
                        var insRightBullet = BulletList[i].gameObject; insRightBullet.transform.position = rightBarrel.transform.position; insRightBullet.transform.rotation = rightTurret.transform.rotation;
                        insRightBullet.SetActive(true);
                        var insLeftBullet = BulletList[i + 10].gameObject; insLeftBullet.transform.position = leftBarrel.transform.position; insLeftBullet.transform.rotation = leftTurret.transform.rotation;
                        insLeftBullet.SetActive(true);
                        //insRightBullet.GetComponent<Rigidbody>().AddForce(insRightBullet.transform.forward * 4000);
                        insRightBullet.transform.DOMove(_targetObj.transform.position, .1f);
                        ExplotionDotList[i].transform.position = insRightBullet.transform.position; ExplotionDotList[i].Play();
                        rightTurret.transform.GetChild(0).transform.DOLocalMoveZ(-.5f, .2f).SetLoops(2,LoopType.Yoyo);
                        //insLeftBullet.GetComponent<Rigidbody>().AddForce(insLeftBullet.transform.forward * 4000);
                        insLeftBullet.transform.DOMove(_targetObj.transform.position, .1f);
                        ExplotionDotList[i + 10].transform.position = insLeftBullet.transform.position; ExplotionDotList[i + 10].Play();
                        leftTurret.transform.GetChild(0).transform.DOLocalMoveZ(-.5f, .2f).SetLoops(2,LoopType.Yoyo);
                        bullet.transform.position = _targetObj.transform.position;
                        bullet.SetActive(true);
                        i++;
                    }
                }
                else yield break;
            }
        }
        
        public void Reset()
        {
            _index = 0;
        }
    }
}