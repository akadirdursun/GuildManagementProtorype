using AdventurerVillage.CharacterSystem;
using UnityEngine;

namespace AdventurerVillage.NameGenerationSystem
{
    [CreateAssetMenu(fileName = "CharacterNameGenerator", menuName = "Adventurer Village/Name Generation System/Character Name Generator")]
    //TODO: Refactor
    public class CharacterNameGenerator : ScriptableObject
    {
        [SerializeField] private string folderPath;
        [SerializeField] private bool genderBasedNames;
        [SerializeField] private string[] maleNames;
        [SerializeField] private string[] femaleNames;
        [SerializeField] private string[] names;
        [SerializeField] private bool hasSurnames;
        [SerializeField] private string[] surnames;

        private const string MaleNameJsonAsset = "MaleNames";
        private const string FemaleNameJsonAsset = "FemaleNames";
        private const string UnisexNameJsonAsset = "Names";
        private const string SurnameJsonAsset = "Surnames";

        public string GetName(Genders gender)
        {
            string characterName = "";
                switch (gender)
                {
                    case Genders.Male:
                        characterName = maleNames[Random.Range(0, maleNames.Length)];
                        break;
                    case Genders.Female:
                        characterName = maleNames[Random.Range(0, femaleNames.Length)];
                        break;
                }

            if (hasSurnames)
            {
                characterName += $" {surnames[Random.Range(0, surnames.Length)]}";
            }

            return characterName;
        }

        public string GetName()
        {
            string characterName = names[Random.Range(0, names.Length)];
         
            if (hasSurnames)
            {
                characterName += $" {surnames[Random.Range(0, surnames.Length)]}";
            }

            return characterName;
        }
#if UNITY_EDITOR
        [ContextMenu("Read Names")]
        public void ReadNames()
        {
            if (genderBasedNames)
            {
                var maleNameJson = Resources.Load<TextAsset>(folderPath + "/" + MaleNameJsonAsset).text;
                maleNames = JsonUtility.FromJson<NameValues>(maleNameJson).values;
                var femaleNameJson = Resources.Load<TextAsset>(folderPath + "/" + FemaleNameJsonAsset).text;
                femaleNames = JsonUtility.FromJson<NameValues>(femaleNameJson).values;
            }
            else
            {
                var nameJson = Resources.Load<TextAsset>(folderPath + "/" + UnisexNameJsonAsset).text;
                names = JsonUtility.FromJson<NameValues>(nameJson).values;
            }

            if (!hasSurnames) return;
            var surnameJson = Resources.Load<TextAsset>(folderPath + "/" + SurnameJsonAsset).text;
            surnames = JsonUtility.FromJson<NameValues>(surnameJson).values;

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}