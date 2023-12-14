using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction onPlatformClose = delegate { };
    }
}