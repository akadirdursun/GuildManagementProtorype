using System;
using System.Collections.Generic;
using System.Linq;
using AdventurerVillage.TimeSystem;

namespace AdventurerVillage.StateMachineSystem
{
    [Serializable/*, ReadOnly*/]
    public abstract class StateMachine
    {
        #region Constructors

        protected StateMachine()
        {
            TimeController.TimeTick += OnTimePassed;
        }

        #endregion
        
        protected Queue<IState> StateOrder;
        protected IState CurrentState;

        protected void ChangeState(IState newState)
        {
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Next()
        {
            if (!StateOrder.Any())
            {
                TimeController.TimeTick -= OnTimePassed;
                Complete();
                return;
            }
            
            var nextState = StateOrder.Dequeue();
            ChangeState(nextState);
        }
        
        private void OnTimePassed(float time)
        {
            if (CurrentState == null) return;
            CurrentState.Execute(time);
        }

        protected abstract void Start();
        protected abstract void Complete();
    }
}