using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers.MinigameManager
{
    public class MinigameController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject minigamePlatform;
        [SerializeField] private GameObject door;

        #endregion

        #region Private variables

        private GameObject _player;

        #endregion

        #endregion

        public void Close()
        {
            for (int i = 0; i < 2; i++)
            {
                if (minigamePlatform.transform.GetChild(i).GetComponent<Renderer>().material.name != door.GetComponent<Renderer>().material.name)
                {
                    minigamePlatform.transform.GetChild(i).transform.DOScaleZ(0, 1).OnComplete(()=> MinigameSignals.Instance.onPlayExecution?.Invoke());
                    break;
                }
            }
        }
    }
}