using System;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.CharacterSystem
{
    [Serializable]
    public class CharacterStateController
    {
        [SerializeField, ReadOnly] private CharacterStates currentState = CharacterStates.IdleState;

        public CharacterStates CurrentState => currentState;

        public void ChangeState(CharacterStates newState)
        {
            currentState = newState;
        }
    }
}