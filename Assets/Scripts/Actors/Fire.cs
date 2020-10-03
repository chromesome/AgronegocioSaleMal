using System.Collections.Generic;
using UnityEngine;

public class Fire : Actor, IHarmful, IDestructible
{
    float health = 100f;
    [SerializeField] float minHealth = 50f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float selfDamage = 5f; // Damage per second done to self
    [SerializeField] float maxDamage = 10f;  // Damage per second

    private void Awake()
    {
        health = Random.Range(minHealth, maxHealth);
    }

    private void Start()
    {
        InvokeRepeating("MakeDamage", 0.0f, 1.0f);
        InvokeRepeating("Spread", 5.0f, 5.0f);
    }

    public override void SetupActions()
    {
        actions = new List<Actions>();
        actions.Add(Actions.Mitigate);
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void Consume()
    {
        ReceiveDamage(selfDamage);
    }

    public float ReceiveDamage(float damage)
    {
        this.health -= damage;
        if(health < 1)
        {
            Extinguish();
        }

        return 0;
    }

    public void Extinguish()
    {
        Destroy(gameObject);
    }

    public void MakeDamage()
    {
        Tile tile = this.GetComponentInParent<Tile>();
        float damage = Mathf.Clamp(maxDamage * health / maxHealth, 1, maxDamage);
        tile.ReceiveDamage(damage);

        // Do half damage to all neighbours
        foreach (Tile neighbour in tile.neighbours)
        {
            if(!neighbour.IsOnFire())
            {
                neighbour.ReceiveDamage(Mathf.Clamp(damage * 0.5f, 1, maxDamage));
            }
        }
    }

    public void Spread()
    {
        Tile tile = this.GetComponentInParent<Tile>();
        foreach (Tile neighbour in tile.neighbours)
        {
            neighbour.TrySetFire();
            //TODO: Quien instancia el fuego?
        }
    }
}