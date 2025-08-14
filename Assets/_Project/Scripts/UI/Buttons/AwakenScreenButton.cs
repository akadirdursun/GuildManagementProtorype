using AdventurerVillage.AwakenSystems.UI;
using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class AwakenScreenButton : BaseButtonAction
    {
        protected override void OnButtonClicked()
        {
            UIScreenController.Instance.ShowScreen(typeof(CharacterAwakenScreen));
        }
    }
}