using UnityEngine;

public sealed class DealDamageAction : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out HealthComponent healthComponent)) 
            healthComponent.TakeDamage(damage);
    }
}