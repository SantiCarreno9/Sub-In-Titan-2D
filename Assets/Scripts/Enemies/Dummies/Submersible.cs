using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submersible : MonoBehaviour, ISubmarine
{
    [SerializeField] float radius;
    public Transform Transform { get => transform; }
    public void AddAttachedEnemy(IEnemyEffect enemyEffect)
    {
        
    }

    public void Damage(int amount)
    {
        Debug.Log("Damaged");
    }

    public Vector2 GetRelativeAttackPosition()
    {
        return transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
    }

    public void RemoveAttachedEnemy(IEnemyEffect enemyEffect)
    {
        
    }
}
