using Sirenix.OdinInspector;
using UnityEngine;

public sealed class Explosion : MonoBehaviour
{
    [SerializeField] private float powerExplosion;
    [SerializeField] private float radiusExplosion;
    [SerializeField] private GameObject centerExplosion;
    [SerializeField] private int maxCollidersExplosion;
    [SerializeField] private Color colorGizmo;

    //TODO: метод для теста.
    //TODO: так же в финальное if можно добавить Else что если Rigidbody нету то его можно добавить через AddComponent
    [Button]
    void ExplosionDamage()
    {
        Collider[] hitColliders = new Collider[maxCollidersExplosion];
        int numColliders = Physics.OverlapSphereNonAlloc(centerExplosion.transform.position, radiusExplosion, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            var distance = Vector3.Distance(centerExplosion.transform.position, hitColliders[i].transform.position);
            if (distance < radiusExplosion)
            {
                var direction = hitColliders[i].transform.position - centerExplosion.transform.position;
                if (hitColliders[i].TryGetComponent(out Rigidbody rigidBody))
                {
                    rigidBody.AddForce(direction.normalized * powerExplosion * (radiusExplosion - distance));
                }
            }
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = colorGizmo;
        Gizmos.DrawSphere(centerExplosion.transform.position, radiusExplosion);
    }
#endif
    
    
    //TODO: чистый метод для использования 
    /*void ExplosionDamage(GameObject center,float power, float radius, int maxColliders)
    {
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(center.transform.position, radius, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            var distance = Vector3.Distance(center.transform.position, hitColliders[i].transform.position);
            if (distance < radius)
            {
                var direction = hitColliders[i].transform.position - center.transform.position;
                if (hitColliders[i].TryGetComponent(out Rigidbody rigidBody))
                {
                    rigidBody.AddForce(direction.normalized * powerExplosion * (radius - distance));
                }
            }
        }
    }*/
}
