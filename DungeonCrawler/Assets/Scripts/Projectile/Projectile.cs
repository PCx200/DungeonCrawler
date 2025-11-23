using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected DamageData damageData;

    public virtual void Initialize(DamageData data)
    {
        damageData = data;
    }

}
