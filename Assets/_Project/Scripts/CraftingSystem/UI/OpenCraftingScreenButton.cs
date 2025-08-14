using AdventurerVillage.UI.Buttons;
using AdventurerVillage.UI.ScreenControlSystems;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class OpenCraftingScreenButton : BaseButtonAction
    {
        protected override void OnButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(CraftingScreen));
        }
    }
}