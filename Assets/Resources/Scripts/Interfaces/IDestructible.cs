public interface IDestructible
{
    float GetMaxHealth();
    float GetCurrentHealth();
    float ReceiveDamage(float damage);
}
