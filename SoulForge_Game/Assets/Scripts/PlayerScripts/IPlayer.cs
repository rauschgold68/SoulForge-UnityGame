public interface IPlayer
{
    void TakeDamage(int damage);
    void Die();
    void Heal(int healAmount);
    void LifeSteal(int attackDamage, bool lifeStealEnabled, string upgradeStage);
    int GetCurrentHealth();
}