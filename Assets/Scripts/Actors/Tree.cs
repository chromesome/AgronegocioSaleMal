using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Actor, IDestructible
{
    float health = 100f;
    [SerializeField] float maxHealth = 100f;

    public override void SetupActions()
    {
        actions = new List<ActionItem>();
        actions.Add(new ActionItem(2, "Fire", "ActionFire", 0));
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float ReceiveDamage(float damage)
    {
        float damageRemain = 0;
        health -= damage;
        if(health < 0)
        {
            damageRemain = Mathf.Abs(health - damage);
            Destroy(gameObject);
        }
        return damageRemain;
    }
}
