using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_ColorData", menuName = "Data/SO_ColorData", order = 0)]
    public class SO_ColorData : ScriptableObject
    {
        public ColorData ColorData;
    }
}