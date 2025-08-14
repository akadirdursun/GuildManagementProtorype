using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.LoadingSystem
{
    [CreateAssetMenu(fileName = "GameLoadTypeData", menuName = "Adventurer Village/Loading System/Game Load Type Data")]
    public class GameLoadTypeData : ScriptableObject
    {
        [SerializeField, ReadOnly] private GameLoadType loadType;

        public GameLoadType LoadType => loadType;

        public void SetLoadType(GameLoadType type)
        {
            loadType = type;
        }
    }

    public enum GameLoadType
    {
        LoadNewGame,
        LoadPreviousGame
    }
}