using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Color = Enums.Color;

namespace Controllers.PlayerObjectsManager
{
    public class PlayerObjectsController : MonoBehaviour
    {
        #region Self Variables
        #region public Variables

        #endregion
        #region Serialized Variables
        
        [SerializeField] private List<Material> materials;
        [SerializeField] private Animator animator;

        #endregion
        #region Private Variables
        
        private ObjectData _objectData;
        private ColorData _colorData;
        private GameObject _execution;
        private Transform _oldTransform;

        #endregion
        #endregion

        private void Awake()
        {
            _objectData = GetObjectData();
            _colorData = GetColorData();
            materials = _colorData.Colors;
        }

        private ObjectData GetObjectData(){return Resources.Load<SO_ObjectData>("Data/SO_ObjectData").ObjectData;}
        private ColorData GetColorData(){return Resources.Load<SO_ColorData>("Data/SO_ColorData").ColorData;}
        public void PlayerExecution(GameObject other){ _execution = other; }
        public void Comparison(GameObject door)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].color == door.GetComponent<MeshRenderer>().material.color)
                {
                    Color index = (Color)Enum.Parse(typeof(Color), materials[i].name);
                    ColorChange(index);
                }
            }
        }
        
        public void ColorChange(Color color)
        {
            Material material = materials[(int)color];
            transform.GetChild(0).GetComponent<Renderer>().material = material;
        }

        public void MinigameControl()
        {
            PlayerObjectsSignals.Instance.minigameState?.Invoke("HelicopterMinigame");
            float distance = _objectData.distance;
            float i = _objectData.quantity;
            if (transform.position.x < 0)
            {
                transform.DOMoveX(-1.5f, .5f);
            }
            else
            {
                transform.DOMoveX(1.5f, .8f);
            }
            if (distance >= 7)
            {
                i = -.5f;
            }
            else if (distance <= 2.5f)
            {
                i = .5f;
            }
            distance += i;
            _objectData.distance = distance;
            _objectData.quantity = i;
            transform.DOMoveZ(transform.position.z + distance, 1).OnComplete(() => PlayerAnimation("StandingToCrouched"));
        }

        public void PlayExecution()
        {
            if (transform.GetChild(0).GetComponent<Renderer>().material.color != _execution.GetComponent<Renderer>().material.color ) // renkleri enum ataması yap ve onun üzerinden işlet.
            {
                PlayerAnimation("Dead");
                DOVirtual.DelayedCall(5, ()=>PlayerObjectsSignals.Instance.onListChange?.Invoke(transform.gameObject, "Pool"));
            }
            else
            {
                DOVirtual.DelayedCall(3 , ()=>PlayerObjectsSignals.Instance.onListChange?.Invoke(transform.gameObject, "Stack"));
            }
        }

        public void PlayerAnimation(string animation) // durum makinesi yazılacak.
        {
            if (animation == "Runner")
            {
                animator.SetTrigger("Runner");
            }
            else if (animation == "Idle")
            {
                animator.SetTrigger("Idle");
            }
            else if (animation == "StandingToCrouched")
            {
                animator.SetTrigger("StandingToCrouched");
                _oldTransform = transform;
                transform.DOLocalMoveY(3.35f, .2f);
            }
            else if (animation == "Dead")
            {
                transform.DOMoveY(_oldTransform.position.y + .4f, .2f);
                animator.SetTrigger("Dead");
                transform.DOMoveY(_oldTransform.position.y + .1f, .5f).SetDelay(1);
            }
        }
    }
}