using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.UI.CharacterPanel
{
    public class CircleCharacterAttributeView : MonoBehaviour
    {
        [SerializeField] private GradeTable gradeTable;
        [SerializeField] private Image fillImage;
        [SerializeField] private TMP_Text gradeText;

        private Attribute _attribute;

        public void Initialize(Attribute attribute)
        {
            _attribute = attribute;
            UpdateView();
            _attribute.OnCharacterStatChanged += UpdateView;
        }

        private void UpdateView()
        {
            var level = _attribute.Level;
            var progress = (float)level / Attribute.MaxLevel;
            var color = gradeTable.GetColor(_attribute.Grade);
            fillImage.color = color;
            fillImage.fillAmount = progress;
            gradeText.text = $"{_attribute.Grade}";
        }

        private void Clear()
        {
            if (_attribute == null) return;
            _attribute.OnCharacterStatChanged -= UpdateView;
            _attribute = null;
        }

        #region MonoBehaviour Methods

        private void OnDisable()
        {
            Clear();
        }

        #endregion
    }
}