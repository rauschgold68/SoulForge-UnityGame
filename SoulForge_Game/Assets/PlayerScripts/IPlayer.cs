public interface IPlayer
{
    void TakeDamage(int damage);
    void Die();
    void Heal(int healAmount);
    int lifeSteal(int damage);
}