using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_ObjectData", menuName = "Data/SO_ObjectData", order = 0)]
    public class SO_ObjectData : ScriptableObject
    {
        public ObjectData ObjectData;
    }
}