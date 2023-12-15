using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerObjectsSignals : MonoSingleton<PlayerObjectsSignals>
    {
        public Func<float> onDistance = delegate { return new float();};
        public Func<GameObject> onFirstPlayerObject = delegate { return new GameObject();};
        public UnityAction<string> minigameState = delegate { };
        public UnityAction<GameObject, string> onListChange = delegate { };
        public UnityAction<GameObject> onMinigamePoolAdd = delegate { };
        public UnityAction<GameObject> onSlowlyStack = delegate { };
        public UnityAction<GameObject> onMinigameAdd = delegate { };
        public UnityAction onCasualStack = delegate { };
        public UnityAction<bool> onIdleObjScale = delegate { };
        public UnityAction onBasket = delegate { };
    }
}