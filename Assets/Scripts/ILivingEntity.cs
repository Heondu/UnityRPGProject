public interface ILivingEntity
{
    void TakeDamage(int damage);

    public void Restore(int value);

    Status GetStatus(string name);
}
