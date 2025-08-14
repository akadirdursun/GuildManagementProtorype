using AdventurerVillage.CharacterSystem.UI;
using AdventurerVillage.UI.ScreenControlSystems;

namespace AdventurerVillage.UI.Buttons
{
    public class CharacterListButton : BaseButtonAction
    {
        protected override void OnButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(CharacterListScreen));
        }
    }
}
