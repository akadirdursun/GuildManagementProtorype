using UnityEngine;

namespace AdventurerVillage.PartySystem
{
    [CreateAssetMenu(fileName = "SelectedPartyData", menuName = "Adventurer Village/Party System/Selected Party Data")]
    public class SelectedPartyData : ScriptableObject
    {
        [SerializeField/*, ReadOnly*/] private PartyInfo selectedParty;
        public PartyInfo SelectedParty => selectedParty;
        
        public void SelectParty(PartyInfo party)
        {
            selectedParty = party;
        }
    }
}