using System;

public interface IHealthScript
{
    int health { get; set; }
    float healthMax { get; set; }

    event EventHandler DieEvent;

    void AddMaxHealth(int value);
    void ApplyDamage(int damage);
    void HealthRegeneration(int regeneration);
    void Start();
}