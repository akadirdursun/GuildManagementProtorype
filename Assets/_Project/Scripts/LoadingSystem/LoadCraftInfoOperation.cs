using AdventurerVillage.CraftingSystem;
using AKD.Common.SequenceSystem;

namespace AdventurerVillage.LoadingSystem
{
    public class LoadCraftInfoOperation : BaseOperationBehaviour
    {
        public override void Begin()
        {
            CraftingManager.Instance.Initialize();
            Complete();
        }
    }
}