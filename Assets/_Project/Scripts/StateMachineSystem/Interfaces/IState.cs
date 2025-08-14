namespace AdventurerVillage.StateMachineSystem
{
    public interface IState
    {
        void Enter();
        void Execute(float time);
        void Exit();
    }
}