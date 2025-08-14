using AdventurerVillage.SaveSystem;

namespace AdventurerVillage.CustomScriptableObjectSystem
{
    public abstract class SavableCustomScriptableObject : CustomScriptableObject, ISavable
    {
        public abstract void Save();

        public abstract void Load();
    }
}