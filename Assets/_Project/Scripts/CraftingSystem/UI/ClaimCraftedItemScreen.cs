using System.Linq;
using AdventurerVillage.InventorySystem;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.UI.ScreenControlSystems;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class ClaimCraftedItemScreen : UIScreen
    {
        [SerializeField] protected GradeTable gradeTable;
        [SerializeField] private SelectedCraftingData selectedCraftingData;
        [SerializeField] private CraftingInfoSaveData craftingInfoSaveData;
        [SerializeField] private EquipmentDatabase equipmentDatabase;
        [SerializeField] private InventoryStorage inventoryStorage;
        [SerializeField] private CraftingGradeProgress craftingGradeProgress;
        [SerializeField] private CraftedItemStatArea craftedItemStatArea;
        [Header("UI Elements")]
        [SerializeField/*, TabGroup("UI Elements")*/] private Image iconImage;
        [SerializeField/*, TabGroup("UI Elements")*/] private TMP_Text itemTitleText;
        [SerializeField/*, TabGroup("UI Elements")*/] private TMP_Text craftingPointText;
        [SerializeField/*, TabGroup("UI Elements")*/] private TMP_Text buttonText;
        [SerializeField/*, TabGroup("UI Elements")*/] private CanvasGroup itemInfoCanvasGroup;
        [SerializeField/*, TabGroup("UI Elements")*/] private CanvasGroup progressAreaCanvasGroup;
        [SerializeField/*, TabGroup("UI Elements")*/] private GameObject statArea;
        [SerializeField/*, TabGroup("UI Elements")*/] private GameObject failedArea;
        [Header("Waypoints")]
        [SerializeField/*, TabGroup("Waypoints")*/] private Transform craftingPointTextTarget;
        [SerializeField/*, TabGroup("Waypoints")*/] private Transform itemIconTargetPosition;
        [SerializeField/*, TabGroup("Waypoints")*/] private Transform craftingPointStart;
        [Header("Colors")]
        [SerializeField/*, TabGroup("Colors")*/] private Color successCraftColor = Color.white;
        [SerializeField/*, TabGroup("Colors")*/] private Color failedCraftColor = Color.red;

        private bool _isSuccess;

        private void Initialize()
        {
            itemInfoCanvasGroup.alpha = 0f;
            progressAreaCanvasGroup.alpha = 1f;
            iconImage.transform.localPosition = Vector3.zero;
            closeButton.gameObject.SetActive(false);
            var craftingInfo = selectedCraftingData.SelectedCraftingInfo;
            //Create equipment
            var equipmentData = equipmentDatabase.GetEquipmentData(craftingInfo.EquipmentTypeId);
            var grade = gradeTable.GetGradeFromPoint(craftingInfo.EarnedCraftingPoint);
            var equipmentInfo = equipmentData.CraftEquipment(craftingInfo.EarnedCraftingPoint, grade);
            _isSuccess = equipmentInfo.CombatStatEffects.Any();
            statArea.SetActive(_isSuccess);
            failedArea.SetActive(!_isSuccess);
            if (_isSuccess)
            {
                //Add new equipment to inventory
                inventoryStorage.PlayerInventory.AddItem(equipmentInfo);
                craftedItemStatArea.Initialize(equipmentInfo.CombatStatEffects);
            }

            //Clear crafting info from save files
            craftingInfoSaveData.RemoveCompletedCraftingInfo(craftingInfo);
            //Set equipment stat text and image
            itemTitleText.text = equipmentData.Name;
            iconImage.sprite = equipmentData.Icon;
        }

        private void PlaySuccessAnimation()
        {
            craftingPointText.color = successCraftColor;
            buttonText.text = "CLAIM";
            iconImage.color = successCraftColor;
            var craftingInfo = selectedCraftingData.SelectedCraftingInfo;
            craftingPointText.text = $"+{craftingInfo.EarnedCraftingPoint:N0}";
            craftingPointText.transform.position = craftingPointStart.position;
            Tween.PositionY(craftingPointText.transform, craftingPointTextTarget.position.y, .5f, Ease.Linear, startDelay: .5f)
                .OnComplete(() =>
                {
                    var progressAnimationDuration =
                        craftingGradeProgress.Initialize(craftingInfo.EarnedCraftingPoint, OnProgressAnimationCompleted);
                    Tween.Custom(craftingInfo.EarnedCraftingPoint, 0, progressAnimationDuration,
                        (value) => { craftingPointText.text = $"+{value:N0}"; });
                });

            void OnProgressAnimationCompleted()
            {
                progressAreaCanvasGroup.alpha = 0f;
                Tween.Position(iconImage.transform, itemIconTargetPosition.position, .5f, Ease.Linear)
                    .Chain(Tween.Alpha(itemInfoCanvasGroup, 1f, 1f)).OnComplete(() => { closeButton.gameObject.SetActive(true); });
            }
        }

        private void PlayFailedAnimation()
        {
            craftingPointText.color = successCraftColor;
            buttonText.text = "CLOSE";
            iconImage.color = successCraftColor;
            var craftingInfo = selectedCraftingData.SelectedCraftingInfo;
            craftingPointText.text = $"+{craftingInfo.EarnedCraftingPoint:N0}";
            Tween.Color(craftingPointText, failedCraftColor, 1f).Group(Tween.Color(iconImage, failedCraftColor, 1f))
                .OnComplete(OnAnimationCompleted);

            void OnAnimationCompleted()
            {
                progressAreaCanvasGroup.alpha = 0f;
                Tween.Position(iconImage.transform, itemIconTargetPosition.position, .5f, Ease.Linear)
                    .Chain(Tween.Alpha(itemInfoCanvasGroup, 1f, 1f)).OnComplete(() => { closeButton.gameObject.SetActive(true); });
            }
        }

        #region UIScreen Methods

        protected override void OnBeforePopupShow()
        {
            base.OnBeforePopupShow();
            Initialize();
        }

        protected override void OnAfterPopupShow()
        {
            base.OnAfterPopupShow();
            if (_isSuccess)
            {
                PlaySuccessAnimation();
                return;
            }

            PlayFailedAnimation();
        }

        protected override void OnAfterPopupClose()
        {
            base.OnAfterPopupClose();
            selectedCraftingData.Clear();
        }

        #endregion
    }
}