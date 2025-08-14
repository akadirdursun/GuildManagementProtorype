using System.Linq;
using AdventurerVillage.BuildingSystem.UI;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.CraftingSystem;
using AdventurerVillage.NotificationSystem;
using AdventurerVillage.TimeSystem;
using UnityEngine;
using CharacterInfo = AdventurerVillage.CharacterSystem.CharacterInfo;

namespace AdventurerVillage.BuildingSystem
{
    public class WorkshopBuildingController : BaseBuildingController
    {
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private CraftingInfoSaveData craftingInfoSaveData;
        [SerializeField] private EquipmentCraftConfig equipmentCraftConfig;
        [SerializeField] private EquipmentWorkshopWorldCanvas equipmentWorkshopWorldCanvas;
        private EquipmentCraftingAction _equipmentCraftingAction;
        private CharacterInfo _craftsmanInfo;
        private int _remainingTickToAction;

        public override void StartNewAction(BaseBuildingAction baseBuildingAction)
        {
            if (baseBuildingAction.GetType() != typeof(EquipmentCraftingAction))
            {
                Debug.LogError("WorkshopBuildingController needs WorkshopBuildingAction");
                return;
            }

            _equipmentCraftingAction = baseBuildingAction as EquipmentCraftingAction;
            if (_equipmentCraftingAction == null) return;
            base.StartNewAction(baseBuildingAction);
            var craftingInfo = _equipmentCraftingAction.CraftingInfo;
            _craftsmanInfo = characterDatabase.AllCharacters.First(character => character.Name == craftingInfo.CraftsmanName);
            _remainingTickToAction = equipmentCraftConfig.ActionPerTick;
            equipmentWorkshopWorldCanvas.Initialize(craftingInfo);
            equipmentWorkshopWorldCanvas.Show();
            DateData.OnTickPasses += OnTimeTickPassed;
        }

        private void OnTimeTickPassed()
        {
            var craftingInfo = _equipmentCraftingAction.CraftingInfo;
            craftingInfo.OnTimeTickPassed();
            _remainingTickToAction--;
            if (_remainingTickToAction == 0)
            {
                _remainingTickToAction = equipmentCraftConfig.ActionPerTick;
                if (IsCraftingPointEarned(out var craftingPointValue))
                    craftingInfo.IncreaseCraftingPoint(craftingPointValue);
            }

            if (craftingInfo.RemainingTickCount <= 0)
                EndAction();
        }

        protected override void EndAction()
        {
            base.EndAction();
            var lastCraftingPointMultiplier =
                (float)(equipmentCraftConfig.ActionPerTick - _remainingTickToAction) / equipmentCraftConfig.ActionPerTick;
            var craftingInfo = _equipmentCraftingAction.CraftingInfo;
            if (IsCraftingPointEarned(out var craftingPointValue))
                craftingInfo.IncreaseCraftingPoint(craftingPointValue * lastCraftingPointMultiplier);
            DateData.OnTickPasses -= OnTimeTickPassed;
            craftingInfoSaveData.OnCraftingCompleted(craftingInfo);
            _craftsmanInfo.CharacterStateController.ChangeState(CharacterStates.IdleState);
            CraftingNotificationManager.Instance.SendNotification(craftingInfo);
            equipmentWorkshopWorldCanvas.Hide();
            Clear();
        }

        private bool IsCraftingPointEarned(out float craftingPointValue)
        {
            var craftingDifficulty = Random.Range(1f, 100f);
            var randomProductivity = Random.Range(1f, _equipmentCraftingAction.CraftingInfo.TotalProductivity);
            if (randomProductivity < craftingDifficulty)
            {
                craftingPointValue = 0;
                return false;
            }

            var randomStrengthPoint = Random.Range(0, _craftsmanInfo.Stats.Strength.Level + 1);
            var isCritical = randomStrengthPoint > craftingDifficulty;
            var quality = _equipmentCraftingAction.CraftingInfo.CraftingPointPerAction;
            craftingPointValue = isCritical ? quality * 2f : quality;

            return true;
        }

        public override void OnSelect()
        {
            Debug.LogError("On Workshop Building Selected");
        }

        private void Clear()
        {
            _equipmentCraftingAction = null;
            _craftsmanInfo = null;
            _remainingTickToAction = 0;
        }

        #region MonoBehaviour Methods

        private void OnDestroy()
        {
            DateData.OnTickPasses -= OnTimeTickPassed;
        }

        #endregion
    }
}