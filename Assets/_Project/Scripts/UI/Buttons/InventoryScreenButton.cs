using AdventurerVillage.InventorySystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;

namespace AdventurerVillage.UI.Buttons
{
    public class InventoryScreenButton : BaseButtonAction
    {
        protected override void OnButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(InventoryUIScreen));
        }
    }
}