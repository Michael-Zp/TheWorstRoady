public class ItemHealth
{
    public int CurrentHealth;

    public ItemHealth(int maxHealth)
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        CurrentHealth--;
    }

    public bool HasNoHpLeft()
    {
        return CurrentHealth <= 0;
    }
}
