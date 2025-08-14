using System;
using System.Collections.Generic;
using System.Linq;
using ADK.Common;
using AdventurerVillage.InputControl;
using AdventurerVillage.InputControl.Enums;
using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.UI.ScreenControlSystems
{
    public class UIActionRecorder : Singleton<UIActionRecorder>
    {
        [SerializeField] private InputData inputData;
        [SerializeField, ReadOnly] private List<UIScreenAction> actions = new();

        public bool IsAnyScreenOpen => actions.Any();

        public void Record(UIScreenAction screenAction)
        {
            if (!actions.Any(a => a.ScreenType == screenAction.ScreenType))
                actions.Add(screenAction);
            Debug.Log($"ActionRecorder added: {screenAction.ScreenName}");
            screenAction.Run();
            if (screenAction.UsingUiInputActions)
                inputData.ChangeState(InputStates.UIScreen);
        }

        private void RemoveLastRecord()
        {
            if (!actions.Any()) return;
            RemoveAction(actions.Last(), actions.Count - 1);
        }

        public void DoLastScreenInvisible()
        {
            if (!actions.Any()) return;
            actions.Last().Invisible();
        }

        public void DoPreviousScreenVisible()
        {
            if (!actions.Any()) return;
            RemoveLastRecord();
            if (actions.Any())
                actions[^1].Visible();
        }

        public void ClearAllRecords()
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                var action = actions[i];
                action.Stop();
            }

            actions.Clear();
            inputData.ChangeState(InputStates.MapView);
        }

        public void RemoveRecordOf(Type screenType)
        {
            if (!actions.Any()) return;

            var actionIndex = actions.FindIndex(action => action.ScreenType == screenType);
            if (actionIndex == -1) return;
            RemoveAction(actions[actionIndex], actionIndex);
        }

        private void RemoveAction(UIScreenAction screenAction, int index)
        {
            Debug.Log($"ActionRecorder remove: {screenAction.ScreenName}");
            actions.RemoveAt(index);
            screenAction.Stop();
            if (!actions.Any(a => a.UsingUiInputActions))
                inputData.ChangeState(InputStates.MapView);
        }
    }
}