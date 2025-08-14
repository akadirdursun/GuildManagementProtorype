using AdventurerVillage.GuildSystem;
using AdventurerVillage.SceneLoadSystem;
using AKD.Common.GameEventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI
{
    public class GuildInfoSelectController : MonoBehaviour
    {
        [SerializeField] private PlayerGuildData playerGuildData;
        [SerializeField] private TMP_InputField guildNameInputField;
        [SerializeField] private WarningTextAnimation waringTextAnimation;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button getBackButton;
        [SerializeField] private GameEvent onGuildInfoSelectionOver;
        //TODO: Create Logo Selection Area

        private void OnConfirmButtonClicked()
        {
            var guildName = guildNameInputField.text;
            if (string.IsNullOrEmpty(guildName))
            {
                waringTextAnimation.PlayAnimation();
                return;
            }
            playerGuildData.CreateGuild(guildName);
            onGuildInfoSelectionOver.Invoke();
        }

        private void OnGetBackButtonClicked()
        {
            SceneLoader.Instance.ReturnToMainMenu();
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            getBackButton.onClick.AddListener(OnGetBackButtonClicked);
        }

        private void Start()
        {
            guildNameInputField.Select();
        }

        private void OnDisable()
        {
            confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
            getBackButton.onClick.RemoveListener(OnGetBackButtonClicked);
        }

        #endregion
    }
}