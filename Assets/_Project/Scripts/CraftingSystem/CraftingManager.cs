using ADK.Common;
using AdventurerVillage.BuildingSystem;
using AdventurerVillage.CharacterSystem;
using AdventurerVillage.HexagonGridSystem;
using AdventurerVillage.NotificationSystem;
using UnityEngine;

namespace AdventurerVillage.CraftingSystem
{
    public class CraftingManager : Singleton<CraftingManager>
    {
        [SerializeField] private CraftSelectionData craftSelectionData;
        [SerializeField] private CraftingInfoSaveData craftingInfoSaveData;
        [SerializeField] private HexMapData hexMapData;

        public void Initialize()
        {
            var ongoingCraftingInfos = craftingInfoSaveData.OngoingCraftingInfos;
            foreach (var ongoingCraftingInfo in ongoingCraftingInfos)
            {
                StartCraftingAction(ongoingCraftingInfo);
            }

            var completedCraftingInfos = craftingInfoSaveData.CompletedCraftingInfos;
            foreach (var completedCraftingInfo in completedCraftingInfos)
            {
                CraftingNotificationManager.Instance.SendNotification(completedCraftingInfo);
            }
        }

        public void StartNewEquipmentCrafting()
        {
            if (!craftSelectionData.IsCraftPossible()) return;
            var craftingInfo = craftSelectionData.CreateCraftingInfo();
            craftSelectionData.SelectedCraftsman.CharacterStateController.ChangeState(CharacterStates.OnCraftingState);
            craftingInfoSaveData.AddCraftingInfo(craftingInfo);
            StartCraftingAction(craftingInfo);
        }

        private void StartCraftingAction(CraftingInfo craftingInfo)
        {
            hexMapData.TryGetCell(craftingInfo.BuildingCoordinates, out var cell);
            cell.HexLandManager.StartNewAction(new EquipmentCraftingAction(craftingInfo));
        }
    }
}