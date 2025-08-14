using PrimeTween;
using UnityEngine;

namespace AdventurerVillage
{
    public class GlobalSettingsManager : MonoBehaviour
    {
        private void Initialize()
        {
            PrimeTweenConfig.warnEndValueEqualsCurrent = false;
#if UNITY_EDITOR || ADK_DEBUG
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }

        #region MonoBehaviour Methods

        private void Awake()
        {
            Initialize();
        }

        #endregion
    }
}