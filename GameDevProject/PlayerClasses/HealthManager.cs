using System;

namespace GameDevProject.Entities
{
    public class HealthManager
    {
        public int Health { get; private set; }
        public int MaxHealth { get; }

        public bool IsDead { get; set; } = false;

        public event Action OnDeath;

        public HealthManager(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                IsDead = true;
                OnDeath?.Invoke();
            }
        }
    }
}
