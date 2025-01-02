using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Entities
{
    public class HealthManager
    {
        public int Health { get; private set; }
        public int MaxHealth { get; }

        public bool IsDead { get; set; } = false;

        public event Action OnDeath;

        private float _hitCooldownTimer;
        private readonly float _hitCooldownDuration;

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

            _hitCooldownTimer = _hitCooldownDuration;
        }
    }
}
