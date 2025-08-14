using AdventurerVillage.ResourceSystem;
using AKD.Common.SequenceSystem;
using UnityEngine;

namespace AdventurerVillage.LoadingSystem
{
    public class LoadResourcesOperation : BaseOperationBehaviour
    {
        [SerializeField] private ResourceDatabase resourceDatabase;

        public override void Begin()
        {
            resourceDatabase.Initialize();
            Complete();
        }
    }
}