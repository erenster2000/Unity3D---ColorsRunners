using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_InputData", menuName = "Data/SO_InputData", order = 0)]
    public class SO_InputData : ScriptableObject
    {
        public InputData InputData;
    }
}