using UnityEngine;

public interface ISubmarine
{
    public Vector2 GetAttackPosition();
    public void Damage(int amount);
    public void Push(Vector2 force);
    public void AddAttachedEnemy(IEnemyEffect enemyEffect);
    public void RemoveAttachedEnemy(IEnemyEffect enemyEffect);
}
