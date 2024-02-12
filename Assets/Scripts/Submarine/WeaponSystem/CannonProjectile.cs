using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float explosionRadius;
    [SerializeField] float range;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask physicsLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float destroyTime;
    [SerializeField] GameObject GFX;
    bool hasExploded;
    Vector3 launchPoint;
    bool shot;
    public void Shoot(Vector2 direction)
    {
        launchPoint = transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * direction);
        rb.velocity = direction;
        rb.isKinematic = false;
        shot = true;
        explosionEffect.transform.localScale *= explosionRadius;
    }
    private void Update()
    {
        if (!shot)
        {
            return;
        }
        if (hasExploded)
        {
            return;
        }
        if((transform.position - launchPoint).sqrMagnitude >= range * range)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasExploded)
        {
            return;
        }

        if (((1 << other.gameObject.layer) & physicsLayer) > 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;
        explosionEffect.SetActive(true);
        rb.isKinematic = true;
        GFX.SetActive(false);
        var enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<IEnemy>().Damage(damage);
        }

        Destroy(gameObject, destroyTime);
    }
}
