using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Actor, IDestructible
{
    float health = 100f;
    int tier;
    [SerializeField] float maxHealth = 100f;

    [SerializeField] List<Sprite> factoryStateSprites;

    private void Start()
    {
        
    }

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
        else
        {
            RefreshSprite();
        }
        return damageRemain;
    }

    private void RefreshSprite()
    {
        int currentTier = GetCurrentTier();

        if (tier != currentTier)
        {
            tier = currentTier;
            if(currentTier > 0)
            {
                currentTier -= 1;   // 0 base
            }

            this.GetComponent<SpriteRenderer>().sprite = factoryStateSprites[currentTier];
        }
    }

    private int GetCurrentTier()
    {
        // Magic number = cantidad de sprites
        return Mathf.RoundToInt(health / 10f);
    }
}
