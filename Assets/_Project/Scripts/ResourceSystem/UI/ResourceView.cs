using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.ResourceSystem.UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private ResourceData resourceData;
        [SerializeField, Space] private Image image;
        [SerializeField] private TMP_Text amountText;

        private void UpdateAmount()
        {
            amountText.text = $"{resourceData.Amount}";
        }

        #region MonoBehaviour Methods

        private void Awake()
        {
            image.sprite = resourceData.ResourceIcon;
#if UNITY_EDITOR || ADK_DEBUG
            var button = gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => resourceData.AddAmount(100));
#endif
        }

        private void OnEnable()
        {
            resourceData.OnResourceAmountChanged += UpdateAmount;
        }

        private void Start()
        {
            UpdateAmount();
        }

        private void OnDisable()
        {
            resourceData.OnResourceAmountChanged -= UpdateAmount;
        }

        #endregion
    }
}