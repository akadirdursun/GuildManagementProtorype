using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.GateSystem.UI
{
    public abstract class BaseGateInfoCard : MonoBehaviour
    {
        [SerializeField] protected GateIconView iconView;
        [SerializeField] protected Button selectButton;
        [SerializeField] protected TMP_Text nameText;

        protected GateInfo _gateInfo;

        public void Initialize(GateInfo gateInfo)
        {
            _gateInfo = gateInfo;
            SetView();
        }

        protected virtual void SetView()
        {
            nameText.text = _gateInfo.Name;
            iconView.Initialize(_gateInfo.Icon, _gateInfo.Grade);
        }

        protected abstract void OnSelectButtonClicked();

        #region MonoBehaviour Methods

        protected virtual void OnEnable()
        {
            selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        protected virtual void OnDisable()
        {
            selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        }

        #endregion
    }
}