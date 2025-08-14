namespace AKD.Common.GameEventSystem
{
    public interface IGenericGameEventListener<T>
    {
        void Invoke(T value);
    }
}
