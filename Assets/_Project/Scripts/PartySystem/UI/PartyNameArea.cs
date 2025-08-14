using TMPro;
using UnityEngine;

namespace AdventurerVillage.PartySystem.UI
{
    public class PartyNameArea : MonoBehaviour
    {
        [SerializeField] private SelectedPartyData selectedPartyData;
        [SerializeField] private TMP_Text partyNameText;

        public void Initialize()
        {
            partyNameText.text = selectedPartyData.SelectedParty.PartyName;
        }
    }
}