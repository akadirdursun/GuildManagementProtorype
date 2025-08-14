using AdventurerVillage.HexagonGridSystem.Pathfinding;
using AdventurerVillage.StateMachineSystem;

namespace AdventurerVillage.RaidSystem.StateMachineSystem
{
    public class MoveBackToCityState : BaseRaidState
    {
        #region Constructor

        public MoveBackToCityState(RaidInfo raidInfo, StateMachine stateMachine) : base(raidInfo, stateMachine)
        {
            
        }

        #endregion
        

        private HexNode[] _path;
        private int _currentIndex = 0;

        public override void Enter()
        {
            _path = HexPathFinder.Instance.FindPath(RaidInfo.CurrentCoordinate, GameConfig.CityCoordinates)
                .ToArray();
            if (RaidInfo.TimeLeftToMove == 0)
                RaidInfo.TimeLeftToMove = _path[_currentIndex].TerrainDifficulty;
        }

        public override void Execute(float time)
        {
            RaidInfo.TimeLeftToMove -= time;
            if (RaidInfo.TimeLeftToMove <= 0)
                MoveToNextHex();
        }

        public override void Exit()
        {
            RaidInfo.TimeLeftToMove = 0f;
        }

        private void MoveToNextHex()
        {
            if (_currentIndex == _path.Length - 1)
            {
                StateMachine.Next();
                return;
            }
            var nextNode = _path[++_currentIndex];
            RaidInfo.MoveTo(nextNode.Coordinates);
        }
    }
}