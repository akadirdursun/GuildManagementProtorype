using AdventurerVillage.UI.ScreenControlSystems;
using UnityEngine;

namespace AdventurerVillage.UI.Test
{
    public class ShowScreenButtonScreen: MonoBehaviour
    {
        [SerializeField] private ShowScreenButton showScreenButtonPrefab;
        [SerializeField] private RectTransform contentHolder;

        public void Initialize(UIScreen[] screens)
        {
            foreach (var screen in screens)
            {
                var button = Instantiate(showScreenButtonPrefab, contentHolder);
                button.Initialize(screen);
            }
        }
    }
}