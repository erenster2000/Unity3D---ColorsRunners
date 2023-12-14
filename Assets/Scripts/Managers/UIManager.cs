using Controllers.UIManager;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uıPanelController;

        #endregion

        #endregion

        public void Play()
        {
            uıPanelController.OnClosePanel(UIPanel.PlayButton);
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
    }
}