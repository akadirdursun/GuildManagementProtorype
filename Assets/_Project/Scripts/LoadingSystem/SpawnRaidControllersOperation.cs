using AdventurerVillage.RaidSystem;
using AKD.Common.SequenceSystem;

namespace AdventurerVillage.LoadingSystem
{
    public class SpawnRaidControllersOperation : BaseOperationBehaviour
    {
        public override void Begin()
        {
            RaidControllerSpawner.Instance.SpawnSavedRaidControllers();
            Complete();
        }
    }
}
