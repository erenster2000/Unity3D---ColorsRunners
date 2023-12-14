using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class MinigameSignals : MonoSingleton<MinigameSignals>
    {
        public UnityAction onPlayExecution = delegate { };
    }
}