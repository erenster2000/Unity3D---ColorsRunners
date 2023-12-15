using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_StackData", menuName = "Data/SO_StackData", order = 0)]
    public class SO_StackData : ScriptableObject
    {
        public StackData StackData;
    }
}