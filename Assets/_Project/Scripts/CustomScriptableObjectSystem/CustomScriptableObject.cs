using UnityEngine;

namespace AdventurerVillage.CustomScriptableObjectSystem
{
    public abstract class CustomScriptableObject : ScriptableObject
    {
        public abstract void OnGameStarted();
    }
}