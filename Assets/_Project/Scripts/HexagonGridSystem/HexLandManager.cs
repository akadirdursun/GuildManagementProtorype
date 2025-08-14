using AdventurerVillage.BuildingSystem;
using AdventurerVillage.BuildingSystem.UI;
using AdventurerVillage.HexagonGridSystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public class HexLandManager : MonoBehaviour
    {
        [SerializeField] private BuildingSaveData buildingSaveData;
        [SerializeField] private HexCell hexCell;
        [SerializeField] private HexCellCanvasController canvasController;
        [SerializeField] private GameObject removableEnvironment;
        [SerializeField] private GameObject fence;

        private HexLandStates _currentState = HexLandStates.Empty;
        private BaseBuildingController _buildingController;

        public BuildingStates CurrentBuildingState => _buildingController.BuildingState;

        public void PlaceBuilding(BaseBuildingController buildingController)
        {
            fence.SetActive(false);
            removableEnvironment.SetActive(false);
            _buildingController = buildingController;
            var buildingTransform = _buildingController.transform;
            buildingTransform.SetParent(transform);
            buildingTransform.localPosition = Vector3.zero;
            _currentState = HexLandStates.BuildingPlaced;
            canvasController.ChangeState(HexCellCanvasStates.BuildingPlacedState);
        }

        private void OnLandClaimed()
        {
            canvasController.ChangeState(HexCellCanvasStates.LandClaimedState);
            removableEnvironment.SetActive(false);
            fence.SetActive(true);
            _currentState = HexLandStates.Claimed;
        }

        private void OnLandClaimed(HexCoordinates coordinates)
        {
            if (coordinates != hexCell.Coordinates) return;
            OnLandClaimed();
        }

        private void OnNewCoordinateAddedAsClaimable(HexCoordinates coordinates)
        {
            if (_currentState != HexLandStates.Empty || coordinates != hexCell.Coordinates) return;
            _currentState = HexLandStates.Claimable;
            canvasController.ChangeState(HexCellCanvasStates.ClaimableLandState);
        }

        public void StartNewAction(BaseBuildingAction buildingAction)
        {
            _buildingController.StartNewAction(buildingAction);
        }

        public void OnSelect()
        {
            switch (_currentState)
            {
                case HexLandStates.Empty:
                    break;
                case HexLandStates.Claimable:
                    UIScreenController.Instance.ShowScreen(typeof(ClaimableLandScreen));
                    break;
                case HexLandStates.Claimed:
                    UIScreenController.Instance.ShowScreen(typeof(BuildingSelectionScreen));
                    break;
                case HexLandStates.BuildingPlaced:
                    _buildingController.OnSelect();
                    break;
            }
        }

        public void OnDeselect()
        {
            switch (_currentState)
            {
                case HexLandStates.Empty:
                    break;
                case HexLandStates.Claimable:
                    UIActionRecorder.Instance.RemoveRecordOf(typeof(ClaimableLandScreen));
                    break;
                case HexLandStates.Claimed:
                    break;
                case HexLandStates.BuildingPlaced:
                    break;
            }
        }

        #region MonoBehaviourMethods

        private void Awake()
        {
            buildingSaveData.OnNewCoordinateAddedAsClaimable += OnNewCoordinateAddedAsClaimable;
            buildingSaveData.SetCoordinateAsClaimed += OnLandClaimed;
        }

        private void OnDisable()
        {
            buildingSaveData.OnNewCoordinateAddedAsClaimable -= OnNewCoordinateAddedAsClaimable;
            buildingSaveData.SetCoordinateAsClaimed -= OnLandClaimed;
        }

        #endregion
    }
}