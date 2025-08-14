using ADK.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.GateSystem.UI
{
    public class GateRaidStateView : MonoBehaviour
    {
        [SerializeField] private Image stateImage;
        [SerializeField] private SlicedFilledImage progressImage;
        [SerializeField] private TMP_Text progressText;

        private GateInfo _gateInfo;

        public void Initialize(GateInfo gateInfo)
        {
            UnregisterEvents();
            _gateInfo = gateInfo;
            RegisterEvents();
            OnGateProgressChanged();
            OnGateProgressChanged();
        }

        private void OnGateStateChanged()
        {
            //TODO: Set stateIcon

            OnGateProgressChanged();
        }

        private void OnGateProgressChanged()
        {
            //TODO: Set progress
        }

        private void RegisterEvents()
        {
            _gateInfo.OnGateStateChanged += OnGateStateChanged;
            _gateInfo.OnGateStateProgressChanged += OnGateProgressChanged;
        }

        private void UnregisterEvents()
        {
            if (_gateInfo == null) return;
            _gateInfo.OnGateStateChanged -= OnGateStateChanged;
            _gateInfo.OnGateStateProgressChanged -= OnGateProgressChanged;
        }
    }
}