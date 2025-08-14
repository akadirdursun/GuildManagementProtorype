using ADK.Common.ObjectPooling;
using AdventurerVillage.ClassSystem;
using AdventurerVillage.GuildSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class ClassSelectionPanel : MonoBehaviour
    {
        [SerializeField] private SelectedCharacterData selectedCharacterData;
        [SerializeField] private ClassDatabase classDatabase;
        [SerializeField] private PlayerGuildData playerGuildData;
        [SerializeField] private ClassButton classButtonPrefab;
        [SerializeField] private Transform classScrollContent;
        [SerializeField] private Button selectClassButton;
        [SerializeField] private TMP_Text classInfoText;

        private IObjectPool<ClassButton> _classButtonPool;
        private BaseClass _selectedClass;

        public void Initialize()
        {
            ShowClasses();
            classInfoText.text = "";
            selectClassButton.gameObject.SetActive(false);
        }

        private void ShowClasses()
        {
            _classButtonPool.ReleaseAll();
            var classes = classDatabase.CombatClasses;
            foreach (var classData in classes)
            {
                _classButtonPool.Get().Initialize(classData, OnClassButtonClick);
            }
        }

        private void OnClassButtonClick(BaseClass selectedClass)
        {
            _selectedClass = selectedClass;
            classInfoText.text = $"{selectedClass.ClassName}";
            selectClassButton.gameObject.SetActive(true);
        }

        private void OnSelectClassButtonClick()
        {
            var characterInfo = selectedCharacterData.SelectedCharacterInfo;
            characterInfo.SetCharacterClass(_selectedClass.Id);
        }

        #region MonoBehaviour Methods

        private void OnEnable()
        {
            selectClassButton.onClick.AddListener(OnSelectClassButtonClick);
        }

        private void Awake()
        {
            _classButtonPool = new ObjectPool<ClassButton>(classButtonPrefab, 4, OnCreate, OnGet, OnRelease);
        }

        private void OnDisable()
        {
            selectClassButton.onClick.RemoveListener(OnSelectClassButtonClick);
        }

        #endregion

        #region Pool

        private void OnCreate(ClassButton button, ObjectPool<ClassButton> pool)
        {
            button.transform.SetParent(transform, false);
            button.gameObject.SetActive(false);
        }

        private void OnGet(ClassButton button)
        {
            button.transform.SetParent(classScrollContent, false);
            button.gameObject.SetActive(true);
        }

        private void OnRelease(ClassButton button)
        {
            button.transform.SetParent(transform, false);
            button.gameObject.SetActive(false);
        }

        #endregion
    }
}