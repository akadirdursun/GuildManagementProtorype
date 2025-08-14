using AdventurerVillage.StatSystem.UI;
using AdventurerVillage.UI.CharacterPanel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class CraftsmanCard : MonoBehaviour
    {
        [SerializeField] private EquipmentCraftConfig equipmentCraftConfig;
        [SerializeField] protected CraftSelectionData craftSelectionData;
        [SerializeField, Space] private TMP_Text nameText;
        [SerializeField] private PortraitView portraitView;
        [SerializeField/*, TabGroup("Stats")*/] private BaseStatValueView craftQuality;
        [SerializeField/*, TabGroup("Stats")*/] private BaseStatValueView craftProductivity;
        [SerializeField/*, TabGroup("Vitals")*/] private VitalView staminaView;
        [SerializeField] private GameObject notEnoughStaminaWarning;
        [SerializeField] private Button selectButton;

        private CharacterInfo _characterInfo;

        public void Initialize(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;
            nameText.text = characterInfo.Name;
            portraitView.Initialize(characterInfo);
            //Stats
            craftQuality.Initialize(_characterInfo.Stats.CraftQuality);
            craftProductivity.Initialize(_characterInfo.Stats.CraftProductivity);
            //Vitals
            staminaView.Initialize(_characterInfo.Stats.Stamina);
            var hasEnoughStamina = _characterInfo.Stats.Stamina.HasEnoughValue(craftSelectionData.BaseStaminaCost);
            notEnoughStaminaWarning.SetActive(!hasEnoughStamina);
            selectButton.interactable = hasEnoughStamina;
        }

        protected virtual void OnSelectButtonClick()
        {
            craftSelectionData.SelectCraftsman(_characterInfo);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            selectButton.onClick.AddListener(OnSelectButtonClick);
        }

        private void OnDisable()
        {
            selectButton.onClick.RemoveListener(OnSelectButtonClick);
        }

        #endregion
    }
}