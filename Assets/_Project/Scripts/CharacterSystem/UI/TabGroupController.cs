using System;
using UnityEngine;
using UnityEngine.UI;

namespace AdventurerVillage.CharacterSystem.UI
{
    public class TabGroupController : MonoBehaviour
    {
        [SerializeField] private TabGroupInfo[] tabGroups;

        private TabGroupInfo _activeTabGroup;

        public void ActivateTab(int index)
        {
            CloseActiveTab();
            ChangeActiveTabGroup(tabGroups[index]);
        }

        private void ChangeActiveTabGroup(TabGroupInfo newTabGroup)
        {
            if (_activeTabGroup == newTabGroup) return;
            if (_activeTabGroup != null)
                _activeTabGroup.tabScreen.SetActive(false);
            _activeTabGroup = newTabGroup;
            _activeTabGroup.tabScreen.SetActive(true);
        }

        private void RegisterTabButtons()
        {
            foreach (var tabGroup in tabGroups)
            {
                tabGroup.tabButton.onClick.AddListener(() => ChangeActiveTabGroup(tabGroup));
            }
        }

        private void UnregisterTabButtons()
        {
            foreach (var tabGroup in tabGroups)
            {
                tabGroup.tabButton.onClick.RemoveAllListeners();
            }
        }

        private void CloseActiveTab()
        {
            if (_activeTabGroup == null) return;
            _activeTabGroup.tabScreen.SetActive(false);
            _activeTabGroup = null;
        }

        private void CloseAllTabs()
        {
            foreach (var tabGroup in tabGroups)
            {
                tabGroup.tabScreen.SetActive(false);
            }

            _activeTabGroup = null;
        }

        #region MonoBehaviourMethods

        private void OnEnable()
        {
            RegisterTabButtons();
        }

        private void Start()
        {
            CloseAllTabs();
        }

        private void OnDisable()
        {
            UnregisterTabButtons();
        }

        #endregion

        #region Structs

        [Serializable]
        private class TabGroupInfo
        {
            public Button tabButton;
            public GameObject tabScreen;
        }

        #endregion
    }
}