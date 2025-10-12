using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    protected float health;
    protected float healthMax;
    protected float speed;
    protected float knockBack;
    public virtual void TakeDamage(float damage){
        health -= damage;
        if (health > 0) {
            Die();
        }
    }
    protected abstract void Die();
    public float GetKnockBack() {
        return knockBack; 
    }

}
