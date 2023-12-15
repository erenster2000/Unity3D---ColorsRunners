using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class ObjectData
    {
        [Range(1, 10)]public float distance = 5;
        public float quantity = .5f;
    }
}