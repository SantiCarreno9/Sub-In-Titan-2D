using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOECharge : MonoBehaviour
{
    [SerializeField] float chargeRadius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] int damage;
    public void UseCharge()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, chargeRadius, enemyLayer);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<IEnemy>().Damage(damage);
        }
    }
}
