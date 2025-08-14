using AdventurerVillage.BuildingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.HexagonGridSystem.UI
{
    public class HexCellCanvasController : MonoBehaviour
    {
        [SerializeField] private BuilderCostData builderCostData;
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField/*, TabGroup("UI Elements")*/] private TMP_Text claimableLandCostText;
        [SerializeField/*, TabGroup("UI Elements")*/] private Image iconImage;
        [SerializeField/*, TabGroup("Sprites")*/] private Sprite claimedLandSprite;
        
        private HexCellCanvasStates _hexCellCanvasStates;

        public void ChangeState(HexCellCanvasStates newState)
        {
            _hexCellCanvasStates = newState;

            HideAllElements();
            switch (_hexCellCanvasStates)
            {
                case HexCellCanvasStates.EmptyState:
                    break;
                case HexCellCanvasStates.ClaimableLandState:
                    ShowCostText();
                    break;
                case HexCellCanvasStates.LandClaimedState:
                    iconImage.sprite = claimedLandSprite;
                    iconImage.gameObject.SetActive(true);
                    break;
                case HexCellCanvasStates.BuildingPlacedState:
                    iconImage.gameObject.SetActive(false);
                    break;
            }
        }

        private void ShowCostText()
        {
            UpdateCostText();
            buildingSaveData.OnNewLandClaimed += UpdateCostText;
            claimableLandCostText.gameObject.SetActive(true);
            
        }

        private void UpdateCostText()
        {
            var builderCost = builderCostData.GetCurrentBuilderCost();
            claimableLandCostText.text = $"<sprite=0> {builderCost}";
        }

        private void HideCostText()
        {
            buildingSaveData.OnNewLandClaimed -= UpdateCostText;
            claimableLandCostText.gameObject.SetActive(false);
        }

        private void HideAllElements()
        {
            HideCostText();
            iconImage.gameObject.SetActive(false);
        }

        #region MonoBehaviour Methods

        private void Awake()
        {
            ChangeState(HexCellCanvasStates.EmptyState);
        }

        private void OnDestroy()
        {
            HideAllElements();
        }

        #endregion
    }
}