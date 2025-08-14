using AdventurerVillage.RaidSystem;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.PartySystem.UI
{
    public class SelectPartyForRaidCard : PartyCard
    {
        [SerializeField/*, PropertyOrder(-1)*/] private RaidData raidData;
        [SerializeField] private Button selectButton;
        
        private void OnSelectButtonClicked()
        {
            raidData.SetPartyInfo(PartyInfo);
        }
        
        #region MonoBehaviour Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        }

        #endregion
    }
}