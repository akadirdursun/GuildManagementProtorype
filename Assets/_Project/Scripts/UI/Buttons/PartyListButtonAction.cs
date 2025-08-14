using AdventurerVillage.PartySystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;

namespace AdventurerVillage.UI.Buttons
{
    public class PartyListButtonAction : BaseButtonAction
    {
        protected override void OnButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(PartyListScreen));
        }
    }
}