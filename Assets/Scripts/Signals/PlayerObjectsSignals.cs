using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerObjectsSignals : MonoSingleton<PlayerObjectsSignals>
    {
        public Func<float> onDistance = delegate { return new float();};
    }
}