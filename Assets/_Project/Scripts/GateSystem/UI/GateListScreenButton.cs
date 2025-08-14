using AdventurerVillage.UI.Buttons;
using AdventurerVillage.UI.ScreenControlSystems;

namespace AdventurerVillage.GateSystem.UI
{
    public class GateListScreenButton : BaseButtonAction
    {
        protected override void OnButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(GateListScreen));
        }
    }
}
