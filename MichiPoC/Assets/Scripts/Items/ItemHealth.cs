public class ItemHealth
{
    public int MaxHealth;
    private int _currentHealth;

    public ItemHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        _currentHealth = MaxHealth;
    }

    public void TakeDamage()
    {
        _currentHealth--;
    }

    public bool HasNoHpLeft()
    {
        return _currentHealth <= 0;
    }
}
