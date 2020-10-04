using System;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Actor, IHarmful, IDestructible
{
    float health = 100f;
    [SerializeField] float minHealth = 50f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float selfDamage = 5f; // Damage per second done to self
    [SerializeField] float maxDamage = 10f;  // Damage per second
    [SerializeField] float mitigateDamage = 10f;
    [SerializeField] int mitigateCost = 10;
    [SerializeField] float secondsDamage;
    [SerializeField] float secondsSpread;

    [SerializeField] Fire offspring;

    private void Awake()
    {
        health = UnityEngine.Random.Range(minHealth, maxHealth);
        SetupActions();
    }

    private void Start()
    {
        InvokeRepeating("MakeDamage", 0.0f, secondsDamage);
        InvokeRepeating("Spread", secondsSpread, secondsSpread);
    }

    public override void SetupActions()
    {
        actions = new List<ActionItem>();
        actions.Add(new ActionItem(5, "Mitigar", "ActionMitigate", mitigateCost));
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
        Extinguish(damage);

        return 0;
    }

    private void ReduceSize()
    {
        float newScale = Mathf.Clamp(health / maxHealth, 0.2f, 1f);
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    public void Extinguish(float damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            Tile tile = this.GetComponentInParent<Tile>();
            tile.Fire = null;
            Destroy(gameObject);
            GameManager.instance.GetComponent<AudioManager>().CheckForNewFires();
        }
        else
        {
            ReduceSize();
        }
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
            if(!neighbour.IsOnFire())
            {
                neighbour.TrySetFire(offspring);
            }
        }
    }

    public void Mitigate()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.money -= mitigateCost;
        gameManager.moneyText.text = gameManager.money.ToString();
        ReceiveDamage(mitigateDamage);
        // HACK 
        GameManager.instance.GetComponent<AudioManager>().CheckForNewFires();
    }
    protected override void SortingLayer()
    {
        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        if (sprRenderer != null)
        {
            sprRenderer.sortingOrder = Y == 0 ? Y + 2 : Y + 5;
        }
    }
}