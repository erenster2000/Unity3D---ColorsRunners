using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class MinigameSignals : MonoSingleton<MinigameSignals>
    {
        public UnityAction onPlayHelicopterExecution = delegate { };
        public UnityAction<GameObject> onSetCamera = delegate { };
        public UnityAction onSlowlyStackAdd = delegate { };
        public Func<int> onStackCount = delegate { return new int();};
        public Func<GameObject> onTarretSetObj = delegate { return new GameObject();};
        public UnityAction onSlowMove = delegate { };
    }
}