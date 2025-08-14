using AdventurerVillage.StatSystem.UI;
using AdventurerVillage.UI.CharacterPanel;
using UnityEngine;

namespace AdventurerVillage.CombatSystem.UI
{
    public class CharacterCombatReportView : MonoBehaviour
    {
        [SerializeField/*, TabGroup("Vital View")*/] private VitalView healthVitalView;
        [SerializeField/*, TabGroup("Vital View")*/] private VitalView staminaVitalView;
        [SerializeField/*, TabGroup("Vital View")*/] private VitalView manaVitalView;
        [Space] [SerializeField] private PortraitView portraitView;
        [SerializeField] private GameObject deadPanel;
        public void Initialize(CharacterCombatConfig config)
        {
            var characterStats = config.CharacterInfo.Stats;
            deadPanel.SetActive(!config.CharacterInfo.IsAlive);
            healthVitalView.Initialize(characterStats.Health);
            staminaVitalView.Initialize(characterStats.Stamina);
            manaVitalView.Initialize(characterStats.Mana);
        }
    }
}