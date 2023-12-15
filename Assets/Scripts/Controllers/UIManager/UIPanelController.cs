using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers.UIManager
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;

        #endregion

        #endregion

        public void OnOpenPanel(UIPanel panelParam )
        {
            panels[(int) panelParam].SetActive(true);
        }

        public void OnClosePanel(UIPanel panelParam)
        {
            panels[(int) panelParam].SetActive(false);
        }
    }
}