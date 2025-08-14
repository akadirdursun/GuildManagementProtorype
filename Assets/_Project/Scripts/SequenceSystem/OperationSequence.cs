using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AKD.Common.SequenceSystem
{
    [Serializable]
    public class OperationSequence : BaseOperationBehaviour
    {
        #region Fields

        [SerializeField] protected List<BaseOperationBehaviour> operations;
        [Space(10)] [SerializeField] private SequenceType sequenceType;

        private int _currentOperationIndex;

        #endregion

        #region Methods

        public override void Init()
        {
            operations.ForEach(operation => { operation.Init(); });
        }

        public override void Begin()
        {
            AddListeners();
            _currentOperationIndex = 0;
            BeforeBegin();
            switch (sequenceType)
            {
                case SequenceType.Consecutive:
                    var op = operations.First();
                    op.BeforeBegin();
                    op.Begin();
                    break;
                case SequenceType.Simultaneous:
                    operations.ForEach(op => op.Begin());
                    break;
            }
        }

        internal override void OnKill()
        {
            RemoveListeners();
            for (int i = 0; i < operations.Count; i++)
            {
                if (_currentOperationIndex > i) continue;
                operations[i].OnKill();
            }
        }

        private void ForwardSequence()
        {
            if (_currentOperationIndex == operations.Count - 1)
            {
                RemoveListeners();
                Complete();
                return;
            }

            _currentOperationIndex++;

            if (sequenceType == SequenceType.Consecutive)
            {
                operations[_currentOperationIndex].Begin();
            }
        }

        private void AddListeners()
        {
            operations.ForEach(operation => { operation.onComplete.AddListener(ForwardSequence); });
        }

        private void RemoveListeners()
        {
            operations.ForEach(operation => { operation.onComplete.RemoveListener(ForwardSequence); });
        }

        #endregion
    }

    public enum SequenceType
    {
        Consecutive,
        Simultaneous
    }
}