using UnityEngine;

namespace AdventurerVillage.SaveSystem
{
    public abstract class SavableScriptableObject: ScriptableObject, ISavable
    {
        public abstract void Save();

        public abstract void Load();

        public abstract void Reset();
    }
}