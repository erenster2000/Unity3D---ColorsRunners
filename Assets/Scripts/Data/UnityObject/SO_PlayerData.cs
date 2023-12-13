using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_PlayerData", menuName = "Data/SO_PlayerData", order = 0)]
    public class SO_PlayerData : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}