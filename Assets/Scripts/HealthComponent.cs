using System;
using UnityEngine;

public sealed class HealthComponent : MonoBehaviour
{
    public event Action OnDeath;
    public event Action OnTakeDamage;
    public bool IsAlive() => health > 0;
    public bool IsHealthFull() => health == maxHitPoints;
    public int GetCurrentHitPoints() => health;

    [SerializeField] private int maxHitPoints;
    [SerializeField] private int health;
        
    public void TakeDamage(int damage)
    {
        health = Math.Max(0, health - damage);
        if (health <= 0)
            OnDeath?.Invoke();
        else
            OnTakeDamage?.Invoke();
    }
        
}