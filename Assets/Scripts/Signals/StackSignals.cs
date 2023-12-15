using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction onPlatformClose = delegate { };
        public UnityAction<bool> onSetOutlineBorder = delegate { };
        public UnityAction<GameObject> onMinigameColor = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction onJoystick = delegate { };
        public UnityAction onUIReset = delegate { };
    }
}