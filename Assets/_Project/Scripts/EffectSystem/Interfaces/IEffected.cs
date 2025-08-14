namespace AdventurerVillage.EffectSystem
{
    public interface IEffected
    {
        public void AddEffect(EffectInfo effectInfo);

        public void RemoveEffect(string effectName);
    }
}