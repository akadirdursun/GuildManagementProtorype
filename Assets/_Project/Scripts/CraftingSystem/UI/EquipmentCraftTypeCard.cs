using ADK.Common.ObjectPooling;
using AdventurerVillage.ClassSystem;
using AdventurerVillage.ClassSystem.UI;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.ResourceSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CraftingSystem.UI
{
    public class EquipmentCraftTypeCard : MonoBehaviour
    {
        [SerializeField] private ClassDatabase classDatabase;
        [SerializeField] protected CraftSelectionData craftSelectionData;
        [SerializeField] private ResourceData materialResourceData;
        [SerializeField] private ClassIcon classIconPrefab;
        [SerializeField] private Button selectButton;
        [SerializeField] private Image itemIconImage;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Transform classListParent;
        [SerializeField] private GameObject notEnoughResourcesArea;

        private EquipmentData _equipmentData;
        private IObjectPool<ClassIcon> _classIconPool;

        public void Initialize(EquipmentData equipmentData)
        {
            _equipmentData = equipmentData;
            itemIconImage.sprite = _equipmentData.Icon;
            itemNameText.text = _equipmentData.Name;
            _classIconPool.ReleaseAll();
            var classesToAvailable = classDatabase.GetCombatClassesCanUse(_equipmentData.Id, _equipmentData.EquipmentType);
            foreach (var classData in classesToAvailable)
            {
                _classIconPool.Get().Initialize(classData);
            }

            costText.text = $"<sprite=0>{_equipmentData.CraftCost}";
            OnResourceAmountChanged();
        }

        protected virtual void OnSelectButtonClick()
        {
            craftSelectionData.SelectEquipment(_equipmentData);
        }

        private void OnResourceAmountChanged()
        {
            if (_equipmentData == null) return;
            if (_equipmentData.CraftCost > materialResourceData.Amount)
            {
                ShowNotEnoughResourcesGroup();
                return;
            }

            HideNotEnoughResourcesGroup();
        }

        private void ShowNotEnoughResourcesGroup()
        {
            selectButton.interactable = false;
            notEnoughResourcesArea.SetActive(true);
        }

        private void HideNotEnoughResourcesGroup()
        {
            selectButton.interactable = true;
            notEnoughResourcesArea.SetActive(false);
        }

        #region MonoBehaviour Methods

        private void Awake()
        {
            _classIconPool = new ObjectPool<ClassIcon>(classIconPrefab, 1, OnCreate, OnGet, OnRelease);
        }

        private void OnEnable()
        {
            selectButton.onClick.AddListener(OnSelectButtonClick);
            materialResourceData.OnResourceAmountChanged += OnResourceAmountChanged;
        }

        private void OnDisable()
        {
            selectButton.onClick.RemoveListener(OnSelectButtonClick);
            materialResourceData.OnResourceAmountChanged -= OnResourceAmountChanged;
        }

        #endregion

        #region Pool Methods

        private void OnCreate(ClassIcon card, ObjectPool<ClassIcon> pool)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        private void OnGet(ClassIcon card)
        {
            card.transform.SetParent(classListParent, false);
            card.gameObject.SetActive(true);
        }

        private void OnRelease(ClassIcon card)
        {
            card.transform.SetParent(transform, false);
            card.gameObject.SetActive(false);
        }

        #endregion
    }
}