using System;
using UnityEngine;

namespace AdventurerVillage.UI.ScreenControlSystems
{
    [Serializable]
    public class UIScreenAction
    {
        #region Constructor

        public UIScreenAction(UIScreen screen)
        {
            _screen = screen;
        }

        #endregion

        [SerializeField] private UIScreen _screen;
        public string ScreenName => _screen.name;
        public bool UsingUiInputActions => _screen.UseUiInputAction;
        public Type ScreenType => _screen.GetType();
        
        public void Run()
        {
            _screen.Show();
        }

        public void Stop()
        {
            _screen.Close();
        }
        
        public void Visible()
        {
            _screen.MakeVisible();
        }

        public void Invisible()
        {
            _screen.MakeInvisible();
        }
    }
}