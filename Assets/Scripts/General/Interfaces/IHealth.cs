public interface IHealth
{
    public int HealthPoints { get; }
    public void Damage(int points);
}