using UnityEngine;

namespace AdventurerVillage.BuildingSystem
{
    public abstract class BaseBuildingController : MonoBehaviour
    {
        protected BuildingInfo BuildingInfo;
        public BuildingStates BuildingState => BuildingInfo.CurrentState;

        public void Initialize(BuildingInfo buildingInfo)
        {
            BuildingInfo = buildingInfo;
        }

        public virtual void StartNewAction(BaseBuildingAction baseBuildingAction)
        {
            BuildingInfo.CurrentState = BuildingStates.InAction;
        }

        protected virtual void EndAction()
        {
            BuildingInfo.CurrentState = BuildingStates.Idle;
        }

        public abstract void OnSelect();
    }
}